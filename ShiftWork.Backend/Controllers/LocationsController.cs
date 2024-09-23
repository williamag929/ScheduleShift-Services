using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftWork.Backend.Controllers
{
    [Authorize]
    [Route("api/{companyId}/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public LocationController(ILocationService locationService, IMapper mapper, IMemoryCache memoryCache)
        {
            _locationService = locationService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        // GET: api/{companyId}/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations(string companyId)
        {
            var cacheKey = $"Locations_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Location> locations))
            {
                locations = await _locationService.GetAll(companyId);
                if (locations == null || !locations.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, locations, cacheEntryOptions);
            }

            return Ok(locations);
        }

        // GET: api/{companyId}/Location/{locationId}
        [HttpGet("{locationId}")]
        public async Task<ActionResult<Location>> GetLocation(string companyId, int locationId)
        {
            var cacheKey = $"Location_{companyId}_{locationId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Location location))
            {
                location = await _locationService.Get(companyId, locationId );

                if (location == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, location, cacheEntryOptions);
            }

            return Ok(location);
        }

        // PUT: api/{companyId}/Location
        [HttpPut]
        public async Task<IActionResult> PutLocation(string companyId, [FromBody] LocationDto locationDto)
        {
            if (locationDto.LocationId == null)
            {
                return BadRequest("LocationId is required");
            }

            var location = _mapper.Map<Location>(locationDto);
            location.CompanyId = companyId;
            location.Updated = DateTime.UtcNow;

            var updatedLocation = await _locationService.Update(location);
            if (updatedLocation == null)
            {
                return NotFound();
            }

            var cacheKey = $"Location_{companyId}_{locationDto.LocationId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedLocation);
        }

        // POST: api/{companyId}/Location
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(string companyId, [FromBody] LocationDto locationDto)
        {
            var cacheKey = $"Locations_{companyId}";

            var location = _mapper.Map<Location>(locationDto);
            location.CompanyId = companyId;
            location.Created = DateTime.UtcNow;
            location.Updated = DateTime.UtcNow;
            location.IsActive = true;

            var createdLocation = await _locationService.Add(location);
            if (createdLocation == null)
            {
                return BadRequest("Failed to create location");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetLocation), new { companyId, locationId = createdLocation.LocationId }, createdLocation);
        }

        // DELETE: api/{companyId}/Location/{locationId}
        [HttpDelete("{locationId}")]
        public async Task<IActionResult> DeleteLocation(string companyId, int locationId)
        {
            var location = await _locationService.Get(companyId,  locationId);


            //var location = locations.First();
            var isDeleted = await _locationService.Delete(location.LocationId);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete location");
            }

            var cacheKey = $"Location_{companyId}_{locationId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
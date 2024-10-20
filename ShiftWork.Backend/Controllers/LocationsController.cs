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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public LocationsController(ILocationService locationService, IMapper mapper, IMemoryCache memoryCache)
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
            if (!_memoryCache.TryGetValue(cacheKey, out Location _location))
            {
                _location = await _locationService.Get(companyId, locationId );

                if (_location == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, _location, cacheEntryOptions);
            }

            return Ok(_location);
        }

        // PUT: api/{companyId}/Location
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, [FromBody] LocationDto locationDto)
        {
            if (locationDto.LocationId == null)
            {
                return BadRequest("LocationId is required");
            }

            var locationModel = _mapper.Map<Location>(locationDto);

            if (id != locationModel.LocationId)
            {
                return BadRequest();
            }

            locationModel.Updated = DateTime.UtcNow;

            var updatedLocation = await _locationService.Update(locationModel);
            if (updatedLocation == null)
            {
                return NotFound();
            }

            var cacheKey = $"Location_{locationModel.CompanyId}_{locationDto.LocationId}";
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

            var cacheKey = $"Locations_{companyId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
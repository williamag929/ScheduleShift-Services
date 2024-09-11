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
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public AreasController(IAreaService areaService, IMapper mapper, IMemoryCache memoryCache)
        {
            _areaService = areaService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        // GET: api/{companyId}/Areas
        [HttpGet("{companyId}/Areas")]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas(string companyId)
        {
            var cacheKey = $"Areas_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Area> areas))
            {
                areas = await _areaService.Get(companyId, new int[] { });
                if (areas == null || !areas.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, areas, cacheEntryOptions);
            }

            return Ok(areas);
        }

        // GET: api/{companyId}/Areas/{areaId}
        [HttpGet("{companyId}/Areas/{areaId}")]
        public async Task<ActionResult<Area>> GetArea(string companyId, int areaId)
        {
            var cacheKey = $"Area_{companyId}_{areaId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Area area))
            {
                var areas = await _areaService.Get(companyId, new[] { areaId });
                if (areas == null || !areas.Any())
                {
                    return NotFound();
                }
                area = areas.FirstOrDefault(x => x.AreaId == areaId);
                if (area == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, area, cacheEntryOptions);
            }

            return Ok(area);
        }

        // PUT: api/{companyId}/Areas
        [HttpPut("{companyId}/Areas")]
        public async Task<IActionResult> PutArea(string companyId, [FromBody] AreaDto areaDto)
        {
            if (areaDto.AreaId == null)
            {
                return BadRequest("AreaId is required");
            }

            var area = _mapper.Map<Area>(areaDto);
            area.CompanyId = companyId;
            area.Updated = DateTime.UtcNow;

            var updatedArea = await _areaService.Update(area);
            if (updatedArea == null)
            {
                return NotFound();
            }

            var cacheKey = $"Area_{companyId}_{areaDto.AreaId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedArea);
        }

        // POST: api/{companyId}/Areas
        [HttpPost("{companyId}/Areas")]
        public async Task<ActionResult<Area>> PostArea(string companyId, [FromBody] AreaDto areaDto)
        {
            var cacheKey = $"Areas_{companyId}";

            var area = _mapper.Map<Area>(areaDto);
            area.CompanyId = companyId;
            area.Created = DateTime.UtcNow;
            area.Updated = DateTime.UtcNow;
            area.IsActive = true;

            var createdArea = await _areaService.Add(area);
            if (createdArea == null)
            {
                return BadRequest("Failed to create area");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetArea), new { companyId, areaId = createdArea.AreaId }, createdArea);
        }

        // DELETE: api/{companyId}/Areas/{areaId}
        [HttpDelete("{companyId}/Areas/{areaId}")]
        public async Task<IActionResult> DeleteArea(string companyId, int areaId)
        {
            var areas = await _areaService.Get(companyId, new[] { areaId });
            if (areas == null || !areas.Any())
            {
                return NotFound();
            }

            var area = areas.First();
            var isDeleted = await _areaService.Delete(area);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete area");
            }

            var cacheKey = $"Area_{companyId}_{areaId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
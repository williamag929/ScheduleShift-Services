using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftWork.Backend.Controllers
{
    [Authorize]
    [Route("api/{companyId}/[controller]")]
    [ApiController]
    public class ScheduleShiftController : ControllerBase
    {
        private readonly IScheduleShiftService _scheduleShiftService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ScheduleShiftController(IScheduleShiftService scheduleShiftService, IMapper mapper, IMemoryCache memoryCache)
        {
            _scheduleShiftService = scheduleShiftService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleShift>>> GetScheduleShifts(string companyId)
        {
            var cacheKey = $"ScheduleShifts_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ScheduleShift> scheduleShifts))
            {
                scheduleShifts = await _scheduleShiftService.GetAll(companyId);
                if (scheduleShifts == null || !scheduleShifts.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, scheduleShifts, cacheEntryOptions);
            }

            return Ok(scheduleShifts);
        }

        [HttpGet("{shiftId}")]
        public async Task<ActionResult<ScheduleShift>> GetScheduleShift(string companyId, int shiftId)
        {
            var cacheKey = $"ScheduleShift_{companyId}_{shiftId}";
            if (!_memoryCache.TryGetValue(cacheKey, out ScheduleShift scheduleShift))
            {
                scheduleShift = await _scheduleShiftService.Get(companyId, shiftId);

                if (scheduleShift == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, scheduleShift, cacheEntryOptions);
            }

            return Ok(scheduleShift);
        }

        [HttpPut]
        public async Task<IActionResult> PutScheduleShift(string companyId, [FromBody] ScheduleShiftDTO scheduleShiftDto)
        {
            if (scheduleShiftDto.ScheduleShiftId == null)
            {
                return BadRequest("ShiftId is required");
            }

            var scheduleShift = _mapper.Map<ScheduleShift>(scheduleShiftDto);
            scheduleShift.CompanyId = companyId;
            scheduleShift.Updated = DateTime.UtcNow;

            var updatedScheduleShift = await _scheduleShiftService.Update(scheduleShift);
            if (updatedScheduleShift == null)
            {
                return NotFound();
            }

            var cacheKey = $"ScheduleShift_{companyId}_{scheduleShiftDto.ScheduleShiftId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedScheduleShift);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleShift>> PostScheduleShift(string companyId, [FromBody] ScheduleShiftDTO scheduleShiftDto)
        {
            var cacheKey = $"ScheduleShifts_{companyId}";

            var scheduleShift = _mapper.Map<ScheduleShift>(scheduleShiftDto);
            scheduleShift.CompanyId = companyId;
            scheduleShift.Created = DateTime.UtcNow;
            scheduleShift.Updated = DateTime.UtcNow;
            scheduleShift.IsActive = true;

            var createdScheduleShift = await _scheduleShiftService.Add(scheduleShift);
            if (createdScheduleShift == null)
            {
                return BadRequest("Failed to create schedule shift");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetScheduleShift), new { companyId, shiftId = createdScheduleShift.ScheduleShiftId }, createdScheduleShift);
        }

        [HttpDelete("{shiftId}")]
        public async Task<IActionResult> DeleteScheduleShift(string companyId, int shiftId)
        {
            var scheduleShift = await _scheduleShiftService.Get(companyId, shiftId);

            var isDeleted = await _scheduleShiftService.Delete(scheduleShift.ScheduleShiftId);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete schedule shift");
            }

            var cacheKey = $"ScheduleShift_{companyId}_{shiftId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ScheduleController(IScheduleService scheduleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules(string companyId)
        {
            var cacheKey = $"Schedules_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Schedule> schedules))
            {
                schedules = await _scheduleService.GetAll(companyId);
                if (schedules == null || !schedules.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, schedules, cacheEntryOptions);
            }

            return Ok(schedules);
        }

        [HttpGet("{scheduleId}")]
        public async Task<ActionResult<Schedule>> GetSchedule(string companyId, int scheduleId)
        {
            var cacheKey = $"Schedule_{companyId}_{scheduleId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Schedule schedule))
            {
                schedule = await _scheduleService.Get(companyId, scheduleId);

                if (schedule == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, schedule, cacheEntryOptions);
            }

            return Ok(schedule);
        }

        [HttpPut]
        public async Task<IActionResult> PutSchedule(string companyId, [FromBody] ScheduleDto scheduleDto)
        {
            if (scheduleDto.ScheduleId == null)
            {
                return BadRequest("ScheduleId is required");
            }

            var schedule = _mapper.Map<Schedule>(scheduleDto);
            schedule.CompanyId = companyId;
            schedule.Updated = DateTime.UtcNow;

            var updatedSchedule = await _scheduleService.Update(schedule);
            if (updatedSchedule == null)
            {
                return NotFound();
            }

            var cacheKey = $"Schedule_{companyId}_{scheduleDto.ScheduleId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedSchedule);
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(string companyId, [FromBody] ScheduleDto scheduleDto)
        {
            var cacheKey = $"Schedules_{companyId}";

            var schedule = _mapper.Map<Schedule>(scheduleDto);
            schedule.CompanyId = companyId;
            schedule.Created = DateTime.UtcNow;
            schedule.Updated = DateTime.UtcNow;
            schedule.IsActive = true;

            var createdSchedule = await _scheduleService.Add(schedule);
            if (createdSchedule == null)
            {
                return BadRequest("Failed to create schedule");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetSchedule), new { companyId, scheduleId = createdSchedule.ScheduleId }, createdSchedule);
        }

        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule(string companyId, int scheduleId)
        {
            var schedule = await _scheduleService.Get(companyId, scheduleId);

            var isDeleted = await _scheduleService.Delete(schedule.ScheduleId);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete schedule");
            }

            var cacheKey = $"Schedule_{companyId}_{scheduleId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }

    }
}

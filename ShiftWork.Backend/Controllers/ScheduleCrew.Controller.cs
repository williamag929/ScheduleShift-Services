using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {

        private readonly ShiftWorkContext _context;

        public SchedulesController(ShiftWorkContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Person.Where(u => u.IsActive == true)
            .Select(u => new User
            {
                Id = u.PersonId,
                Name = u.FullName,
                Role = "Admin",
            }).ToListAsync();

            /**
            return await _context.Users.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Role = u.Role
            }).ToListAsync();
            **/
        }

        // GET: api/shifts?start=YYYY-MM-DD&end=YYYY-MM-DD
        [HttpGet("shifts")]
        public async Task<ActionResult<IEnumerable<Shift>>> GetShifts([FromQuery] string start, [FromQuery] string end)
        {
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            return await _context.ScheduleShifts
                .Where(s => s.StartTime >= startDate && s.EndTime <= endDate)
                .Select(s => new Shift
                {
                    Id = s.ScheduleShiftId,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    UserId = s.PersonId, 
                    RequiredRole = "Admin"
                })
                .ToListAsync();
        }

        // GET: api/availabilities?start=YYYY-MM-DD&end=YYYY-MM-DD
        [HttpGet("availabilities")]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilities([FromQuery] string start, [FromQuery] string end)
        {

            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);

            var users = await _context.Person.Where(u => u.IsActive == true)
            .Select(u => new User
            {
                Id = u.PersonId,
                Name = u.FullName,
                Role = "Admin",
            }).ToListAsync();


            var userIds = users.Select(u => u.Id).ToList();

            var shifts = await _context.ScheduleShifts
            .Where(s => userIds.Contains(s.PersonId) &&
            s.StartTime >= startDate && s.EndTime <= endDate)
            .Select(a => new Availability
            {
                UserId = a.PersonId,
                StartTime = a.StartTime,
                EndTime = a.EndTime
            }).ToListAsync();

            var busyShifts = shifts.Select(a => a.UserId).ToList();
            var availableUsers = users.Where(u => !busyShifts.Contains(u.Id)).ToList();

            return Ok(new
            {
                AvailableUsers = availableUsers,
                BusyShifts = shifts
            });          

            /**
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            return await _context.Availabilities
                .Where(a => a.StartTime >= startDate && a.EndTime <= endDate)
                .Select(a => new Availability
                {
                    UserId = a.UserId,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                })
                .ToListAsync();

                **/
        }

        // POST: api/schedules/bulk
        [HttpPost("schedules/bulk")]
        public async Task<ActionResult> CreateSchedules([FromBody] List<Schedule> schedules)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Schedules.AddRange(schedules);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        // POST: api/schedules/generate
        [HttpPost("generate")]
        public async Task<ActionResult> GenerateSchedule([FromBody] ScheduleRequest request)
        {
            using var client = new HttpClient();
            var pythonServiceUrl = "http://localhost:8000/generate-schedule";
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(request),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync(pythonServiceUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var result = await response.Content.ReadFromJsonAsync<ScheduleResponse>();
            return Ok(result);
        }
    }

    public class ScheduleRequest
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class ScheduleResponse
    {
        public string Message { get; set; }
        public List<Schedule> Schedule { get; set; }
    }
}
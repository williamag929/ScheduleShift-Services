using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ShiftWorkContext _context;

        public ScheduleService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all schedule shifts
        public async Task<List<Schedule>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Schedules
                .Where(s => s.CompanyId == companyId && !s.IsDeleted)
                .ToListAsync();
        }

        // Get a schedule shift by Id
        public async Task<Schedule> Get(string companyId, int ScheduleId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Schedules
                .FirstOrDefaultAsync(s => s.CompanyId == companyId && s.ScheduleId == ScheduleId && !s.IsDeleted);
        }

        // Add a new schedule shift
        public async Task<Schedule> Add(Schedule Schedule)
        {
            if (Schedule == null)
            {
                throw new ArgumentNullException(nameof(Schedule));
            }

            await _context.Schedules.AddAsync(Schedule);
            await _context.SaveChangesAsync();
            return Schedule;
        }

        // Update an existing schedule shift
        public async Task<Schedule> Update(Schedule Schedule)
        {
            if (Schedule == null)
            {
                throw new ArgumentNullException(nameof(Schedule));
            }

            var existingSchedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.ScheduleId == Schedule.ScheduleId);

            if (existingSchedule == null)
            {
                throw new InvalidOperationException("Schedule shift not found");
            }

            //existingSchedule.Subject = Schedule.Subject;
            //existingSchedule.Description = Schedule.Description;
            existingSchedule.StartTime = Schedule.StartTime;
            existingSchedule.EndTime = Schedule.EndTime;
            existingSchedule.PersonId = Schedule.PersonId;
            existingSchedule.ScheduleId = Schedule.ScheduleId;
            existingSchedule.AreaId = Schedule.AreaId;
            existingSchedule.LocationId = Schedule.LocationId;
            //existingSchedule.GeoLocationStart = Schedule.GeoLocationStart;
            //existingSchedule.GeoLocationEnd = Schedule.GeoLocationEnd;
            existingSchedule.IsActive = Schedule.IsActive;
            existingSchedule.IsDeleted = Schedule.IsDeleted;
            existingSchedule.IsApproved = Schedule.IsApproved;
            existingSchedule.Updated = DateTime.UtcNow;
            //existingSchedule.AvatarImageIn = Schedule.AvatarImageIn;
            //existingSchedule.AvatarImageOut = Schedule.AvatarImageOut;

            _context.Schedules.Update(Schedule);
            await _context.SaveChangesAsync();
            return existingSchedule;
        }

        // Delete a schedule shift by Id
        public async Task<bool> Delete(int ScheduleId)
        {
            var Schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.ScheduleId == ScheduleId);

            if (Schedule == null)
            {
                throw new InvalidOperationException("Schedule shift not found");
            }

            Schedule.IsDeleted = true;
            Schedule.Deleted = DateTime.UtcNow;

            _context.Schedules.Update(Schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IScheduleService
    {
        Task<List<Schedule>> GetAll(string companyId);
        Task<Schedule> Get(string companyId, int ScheduleId);
        Task<Schedule> Add(Schedule Schedule);
        Task<Schedule> Update(Schedule Schedule);
        Task<bool> Delete(int ScheduleId);
    }
}
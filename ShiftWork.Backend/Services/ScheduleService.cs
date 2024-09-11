using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class ScheduleShiftService : IScheduleShiftService
    {
        private readonly ShiftWorkContext _context;

        public ScheduleShiftService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all schedule shifts
        public async Task<List<ScheduleShift>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.ScheduleShifts
                .Where(s => s.CompanyId == companyId && !s.IsDeleted)
                .ToListAsync();
        }

        // Get a schedule shift by Id
        public async Task<ScheduleShift> Get(string companyId, int scheduleShiftId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.ScheduleShifts
                .FirstOrDefaultAsync(s => s.CompanyId == companyId && s.ScheduleShiftId == scheduleShiftId && !s.IsDeleted);
        }

        // Add a new schedule shift
        public async Task<ScheduleShift> Add(ScheduleShift scheduleShift)
        {
            if (scheduleShift == null)
            {
                throw new ArgumentNullException(nameof(scheduleShift));
            }

            await _context.ScheduleShifts.AddAsync(scheduleShift);
            await _context.SaveChangesAsync();
            return scheduleShift;
        }

        // Update an existing schedule shift
        public async Task<ScheduleShift> Update(ScheduleShift scheduleShift)
        {
            if (scheduleShift == null)
            {
                throw new ArgumentNullException(nameof(scheduleShift));
            }

            var existingScheduleShift = await _context.ScheduleShifts
                .FirstOrDefaultAsync(s => s.ScheduleShiftId == scheduleShift.ScheduleShiftId);

            if (existingScheduleShift == null)
            {
                throw new InvalidOperationException("Schedule shift not found");
            }

            existingScheduleShift.Subject = scheduleShift.Subject;
            existingScheduleShift.Description = scheduleShift.Description;
            existingScheduleShift.StartTime = scheduleShift.StartTime;
            existingScheduleShift.EndTime = scheduleShift.EndTime;
            existingScheduleShift.PersonId = scheduleShift.PersonId;
            existingScheduleShift.ScheduleId = scheduleShift.ScheduleId;
            existingScheduleShift.AreaId = scheduleShift.AreaId;
            existingScheduleShift.LocationId = scheduleShift.LocationId;
            existingScheduleShift.GeoLocationStart = scheduleShift.GeoLocationStart;
            existingScheduleShift.GeoLocationEnd = scheduleShift.GeoLocationEnd;
            existingScheduleShift.IsActive = scheduleShift.IsActive;
            existingScheduleShift.IsDeleted = scheduleShift.IsDeleted;
            existingScheduleShift.IsApproved = scheduleShift.IsApproved;
            existingScheduleShift.Updated = DateTime.UtcNow;
            existingScheduleShift.AvatarImageIn = scheduleShift.AvatarImageIn;
            existingScheduleShift.AvatarImageOut = scheduleShift.AvatarImageOut;

            _context.ScheduleShifts.Update(existingScheduleShift);
            await _context.SaveChangesAsync();
            return existingScheduleShift;
        }

        // Delete a schedule shift by Id
        public async Task<bool> Delete(int scheduleShiftId)
        {
            var scheduleShift = await _context.ScheduleShifts
                .FirstOrDefaultAsync(s => s.ScheduleShiftId == scheduleShiftId);

            if (scheduleShift == null)
            {
                throw new InvalidOperationException("Schedule shift not found");
            }

            scheduleShift.IsDeleted = true;
            scheduleShift.Deleted = DateTime.UtcNow;

            _context.ScheduleShifts.Update(scheduleShift);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IScheduleShiftService
    {
        Task<List<ScheduleShift>> GetAll(string companyId);
        Task<ScheduleShift> Get(string companyId, int scheduleShiftId);
        Task<ScheduleShift> Add(ScheduleShift scheduleShift);
        Task<ScheduleShift> Update(ScheduleShift scheduleShift);
        Task<bool> Delete(int scheduleShiftId);
    }
}
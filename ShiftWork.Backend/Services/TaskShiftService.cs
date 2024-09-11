using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class TaskShiftService : ITaskShiftService
    {
        private readonly ShiftWorkContext _context;

        public TaskShiftService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all task shifts
        public async Task<IEnumerable<TaskShift>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.TaskShifts
                .Where(ts => ts.CompanyId == companyId && !ts.IsDeleted)
                .ToListAsync();
        }

        // Get a task shift by Id
        public async Task<TaskShift> Get(string companyId, int taskShiftId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.TaskShifts
                .FirstOrDefaultAsync(ts => ts.CompanyId == companyId && ts.TaskShiftId == taskShiftId && !ts.IsDeleted);
        }

        // Add a new task shift
        public async Task<TaskShift> Add(TaskShift taskShift)
        {
            if (taskShift == null)
            {
                throw new ArgumentNullException(nameof(taskShift));
            }

            await _context.TaskShifts.AddAsync(taskShift);
            await _context.SaveChangesAsync();
            return taskShift;
        }

        // Update an existing task shift
        public async Task<TaskShift> Update(TaskShift taskShift)
        {
            if (taskShift == null)
            {
                throw new ArgumentNullException(nameof(taskShift));
            }

            var existingTaskShift = await _context.TaskShifts
                .FirstOrDefaultAsync(ts => ts.TaskShiftId == taskShift.TaskShiftId && !ts.IsDeleted);

            if (existingTaskShift == null)
            {
                throw new InvalidOperationException("TaskShift not found");
            }

            existingTaskShift.TaskShiftName = taskShift.TaskShiftName;
            existingTaskShift.Comment = taskShift.Comment;
            existingTaskShift.isSchedule = taskShift.isSchedule;
            existingTaskShift.TaskDate = taskShift.TaskDate;
            existingTaskShift.StartTime = taskShift.StartTime;
            existingTaskShift.EndTime = taskShift.EndTime;
            existingTaskShift.LocationId = taskShift.LocationId;
            existingTaskShift.AreaId = taskShift.AreaId;
            existingTaskShift.PersonId = taskShift.PersonId;
            existingTaskShift.IsActive = taskShift.IsActive;
            existingTaskShift.Updated = DateTime.UtcNow;

            _context.TaskShifts.Update(existingTaskShift);
            await _context.SaveChangesAsync();
            return existingTaskShift;
        }

        // Delete a task shift by Id
        public async Task<bool> Delete(int taskShiftId)
        {
            var taskShift = await _context.TaskShifts
                .FirstOrDefaultAsync(ts => ts.TaskShiftId == taskShiftId && !ts.IsDeleted);

            if (taskShift == null)
            {
                throw new InvalidOperationException("TaskShift not found");
            }

            taskShift.IsDeleted = true;
            taskShift.Deleted = DateTime.UtcNow;

            _context.TaskShifts.Update(taskShift);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface ITaskShiftService
    {
        Task<IEnumerable<TaskShift>> GetAll(string companyId);
        Task<TaskShift> Get(string companyId, int taskShiftId);
        Task<TaskShift> Add(TaskShift taskShift);
        Task<TaskShift> Update(TaskShift taskShift);
        Task<bool> Delete(int taskShiftId);
    }
}
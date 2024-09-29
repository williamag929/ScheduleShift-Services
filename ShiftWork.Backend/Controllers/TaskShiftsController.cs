using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;

namespace ShiftWork.Backend.Controllers
{
    [Authorize]
    [Route("api/{companyId}/[controller]")]
    [ApiController]
    public class TaskShiftsController : ControllerBase
    {
        private readonly ITaskShiftService _taskShiftService;
        private readonly ShiftWorkContext _context;
        private readonly IMapper _mapper;

        public TaskShiftsController(ShiftWorkContext context, IMapper mapper, ITaskShiftService taskShiftService)
        {
            _context = context;
            _mapper = mapper;
            _taskShiftService = taskShiftService;
        }

        // GET: api/TaskShifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskShift>>> GetTaskShift(string companyId)
        {
           var tasks = await _taskShiftService.GetAll(companyId);
          if (_context.TaskShifts == null)
          {
              return NotFound();
          }
            return tasks.ToList();  
        }

        // GET: api/TaskShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskShift>> GetTaskShift(string companyId, int id)
        {
          if (_context.TaskShifts == null)
          {
              return NotFound();
          }
            var taskShift = await _taskShiftService.Get(companyId, id );

            if (taskShift == null)
            {
                return NotFound();
            }

            return taskShift;
        }

        // PUT: api/TaskShifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskShift(int id, TaskShiftDto taskShiftDto)
        {

            var taskShift = _mapper.Map<TaskShift>(taskShiftDto);

            if (id != taskShift.TaskShiftId)
            {
                return BadRequest();
            }

            var updatedtaskShift = await _taskShiftService.Update(taskShift);
            //_context.Entry(taskShift).State = EntityState.Modified;

           if (updatedtaskShift == null)
            {
                return NotFound();
            }

             return Ok(updatedtaskShift);
        }

        // POST: api/TaskShifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskShift>> PostTaskShift(TaskShiftDto taskShiftDto)
        {
            if (_context.TaskShifts == null)
            {
                return Problem("Entity set 'ShiftWorkContext.TaskShift'  is null.");
            }

            var taskShift = _mapper.Map<TaskShift>(taskShiftDto);

            taskShift.Created = DateTime.Now;
            taskShift.Updated = DateTime.Now;
            taskShift.Deleted = DateTime.Now;
            
            var createdTask = await _taskShiftService.Add(taskShift);
            //_context.TaskShifts.Add(taskShift);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskShift", new { id = createdTask.TaskShiftId }, createdTask);
        }

        // DELETE: api/TaskShifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskShift(int id)
        {
            if (_context.TaskShifts == null)
            {
                return NotFound();
            }
            var taskShift = await _context.TaskShifts.FindAsync(id);
            if (taskShift == null)
            {
                return NotFound();
            }

            var isDeleted = await _taskShiftService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest("Failed to delete location");
            }

            return NoContent();
        }

        private bool TaskShiftExists(int id)
        {
            return (_context.TaskShifts?.Any(e => e.TaskShiftId == id)).GetValueOrDefault();
        }
    }
}

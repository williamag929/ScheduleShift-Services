using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskShiftsController : ControllerBase
    {
        private readonly ShiftWorkContext _context;
        private readonly IMapper _mapper;

        public TaskShiftsController(ShiftWorkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TaskShifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskShift>>> GetTaskShift()
        {
          if (_context.TaskShift == null)
          {
              return NotFound();
          }
            return await _context.TaskShift.ToListAsync();
        }

        // GET: api/TaskShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskShift>> GetTaskShift(int id)
        {
          if (_context.TaskShift == null)
          {
              return NotFound();
          }
            var taskShift = await _context.TaskShift.FindAsync(id);

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

            _context.Entry(taskShift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskShiftExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskShifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskShift>> PostTaskShift(TaskShiftDto taskShiftDto)
        {
            if (_context.TaskShift == null)
            {
                return Problem("Entity set 'ShiftWorkContext.TaskShift'  is null.");
            }

            var taskShift = _mapper.Map<TaskShift>(taskShiftDto);

            taskShift.CreatedDate = DateTime.Now;
            _context.TaskShift.Add(taskShift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskShift", new { id = taskShift.TaskShiftId }, taskShift);
        }

        // DELETE: api/TaskShifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskShift(int id)
        {
            if (_context.TaskShift == null)
            {
                return NotFound();
            }
            var taskShift = await _context.TaskShift.FindAsync(id);
            if (taskShift == null)
            {
                return NotFound();
            }

            _context.TaskShift.Remove(taskShift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskShiftExists(int id)
        {
            return (_context.TaskShift?.Any(e => e.TaskShiftId == id)).GetValueOrDefault();
        }
    }
}

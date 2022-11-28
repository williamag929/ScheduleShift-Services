using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleShiftsController : ControllerBase
    {
        private readonly ShiftWorkContext _context;
        private readonly IMapper _mapper;

        public ScheduleShiftsController(ShiftWorkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ScheduleShifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleShift>>> GetScheduleShift()
        {
          if (_context.ScheduleShift == null)
          {
              return NotFound();
          }
            return await _context.ScheduleShift.ToListAsync();
        }

        // GET: api/ScheduleShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleShift>> GetScheduleShift(int id)
        {
          if (_context.ScheduleShift == null)
          {
              return NotFound();
          }
            var scheduleShift = await _context.ScheduleShift.FindAsync(id);

            if (scheduleShift == null)
            {
                return NotFound();
            }

            return scheduleShift;
        }

        // PUT: api/ScheduleShifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduleShift(int id, ScheduleShift scheduleShift)
        {
            if (id != scheduleShift.ScheduleShiftId)
            {
                return BadRequest();
            }

            _context.Entry(scheduleShift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleShiftExists(id))
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

        // POST: api/ScheduleShifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScheduleShift>> PostScheduleShift(ScheduleShift scheduleShift)
        {
          if (_context.ScheduleShift == null)
          {
              return Problem("Entity set 'ShiftWorkContext.ScheduleShift'  is null.");
          }
            _context.ScheduleShift.Add(scheduleShift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduleShift", new { id = scheduleShift.ScheduleShiftId }, scheduleShift);
        }

        // DELETE: api/ScheduleShifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduleShift(int id)
        {
            if (_context.ScheduleShift == null)
            {
                return NotFound();
            }
            var scheduleShift = await _context.ScheduleShift.FindAsync(id);
            if (scheduleShift == null)
            {
                return NotFound();
            }

            _context.ScheduleShift.Remove(scheduleShift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleShiftExists(int id)
        {
            return (_context.ScheduleShift?.Any(e => e.ScheduleShiftId == id)).GetValueOrDefault();
        }
    }
}

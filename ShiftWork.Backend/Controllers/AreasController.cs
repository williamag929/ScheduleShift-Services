using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly ShiftWorkContext _context;
        private readonly IMapper _mapper;

        public AreasController(ShiftWorkContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetArea([FromQuery] string companyId)
        {
          if (_context.Areas == null)
          {
              return NotFound();
          }
            return await _context.Areas.Where(c=>c.CompanyId == companyId).ToListAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
          if (_context.Areas == null)
          {
              return NotFound();
          }
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, AreaDto areaDto)
        {
            var area = _mapper.Map<Area>(areaDto);
            area.Updated = DateTime.UtcNow;
            if (id != area.AreaId)
            {
                return BadRequest();
            }
            _context.Entry(area).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(AreaDto areaDto)
        {
            if (_context.Areas == null)
            {
                return Problem("Entity set 'ShiftWorkContext.Area'  is null.");
            }
            var area = _mapper.Map<Area>(areaDto);
            area.Created = DateTime.UtcNow;
            area.Updated = DateTime.UtcNow;
            area.IsActive = true;
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetArea", new { id = area.AreaId }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (_context.Areas == null)
            {
                return NotFound();
            }
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AreaExists(int id)
        {
            return (_context.Areas?.Any(e => e.AreaId == id)).GetValueOrDefault();
        }
    }
}

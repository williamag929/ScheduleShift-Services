
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class AreaServices : IAreaService
    {
        private readonly ShiftWorkContext _context;

        public AreaServices(ShiftWorkContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Area>> Get(string companyId, int[] ids)
        {
            var areas = _context.Areas.Where(x => x.CompanyId == companyId).AsQueryable();

            if (ids != null && ids.Any())
                areas = areas.Where(x => ids.Contains(x.AreaId));
            return await areas.ToListAsync();
        }

        public async Task<Area> Add(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();

            return area;
        }

        public async Task<Area> Update(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }
            var areaForChanges = await _context.Areas.SingleAsync(x => x.AreaId == area.AreaId);
            if (areaForChanges == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            areaForChanges.AreaName = area.AreaName;
            areaForChanges.LocationId = area.LocationId;
            areaForChanges.IsActive = area.IsActive;
            areaForChanges.IsDeleted = area.IsDeleted;
            areaForChanges.IsActive = area.IsActive;
            areaForChanges.Deleted = area.Deleted;
            areaForChanges.Updated = DateTime.UtcNow;

            _context.Areas.Update(areaForChanges);
            await _context.SaveChangesAsync();
            return areaForChanges;
        }

        public async Task<bool> Delete(Area area)
        {
            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<IEnumerable<Area>> Get(int Id, int[] ids)
        {
            throw new NotImplementedException();
        }
    }


    public interface IAreaService
    {
        Task<IEnumerable<Area>> Get(string Id, int[] ids);

        Task<Area> Add(Area area);

        Task<Area> Update(Area area);

        Task<bool> Delete(Area area);
    }
}
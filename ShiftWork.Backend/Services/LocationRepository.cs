using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftWork.Backend.Services
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly DbContext _context;
        private readonly DbSet<Location> _dbSet;

        public LocationRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Location>();
        }

        public async Task<List<Location>> GetAll(string id, int[] ids)
        {
            return _dbSet.Where(c=>c.CompanyId == id).ToList();
        }

        public async Task<Location> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Location> Add(Location entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Location> Update(Location entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Location entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
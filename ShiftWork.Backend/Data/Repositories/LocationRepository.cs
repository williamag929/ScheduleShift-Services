using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftWork.Backend.Services
{
    public class LocationRepository : ILocationRepository<Location>
    {
        private readonly ShiftWorkContext _context;
        private readonly DbSet<Location> _dbSet;

        public LocationRepository(ShiftWorkContext context)
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

    public interface ILocationRepository<T>
    {
        Task<List<T>> GetAll(string companyId, int[] ids);
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
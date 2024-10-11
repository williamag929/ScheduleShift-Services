using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{

    public class CompanyRepository : ICompanyRepository<Company>
    {
        private readonly DbContext _context;
        private readonly DbSet<Company> _dbSet;

        public CompanyRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Company>();
        }
        public List<Company> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<Company> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Company> Add(Company entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<Company> Update(Company entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Company entity)
        {
            _dbSet.Remove(entity);
        }
    }


    public interface ICompanyRepository<T>
    {
        List<T> GetAll();
        Task<T> GetById(string id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{

    public class UserCompanyRepository : ICompanyRepository<Company>
    {
        private readonly ShiftWorkContext _context;
        private readonly DbSet<UserCompany> _dbSet;

        public UserCompanyRepository(ShiftWorkContext context)
        {
            _context = context;
            _dbSet = _context.Set<UserCompany>();
        }
        public List<UserCompany> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<UserCompany> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<UserCompany> Add(UserCompany entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<UserCompany> Update(UserCompany entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(UserCompany entity)
        {
            _dbSet.Remove(entity);
        }

        List<Company> ICompanyRepository<Company>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Company> ICompanyRepository<Company>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> Add(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task<Company> Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Company entity)
        {
            throw new NotImplementedException();
        }
    }


    public interface IUserCompanyRepository<T>
    {
        List<T> GetAll();
        Task<T> GetById(string id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using ShiftWork.Backend.Models;
using Microsoft.Extensions.Logging;
using ShiftWork.Backend.Data;


namespace ShiftWork.Backend.Services
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly ShiftWorkContext _context;
        private readonly ILogger<UserCompanyService> _logger;
        private readonly IUserCompanyRepository<UserCompany> _UserCompanyRepository;
        //private readonly ShiftWorkContext _context;

        public UserCompanyService(ShiftWorkContext context,ILogger<UserCompanyService> logger,
        IUserCompanyRepository<UserCompany> repository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
            _UserCompanyRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            //_UserCompanyRepository = new UserCompanyRepository(_context);
        }

        public async Task<IEnumerable<UserCompany>> GetAll()
        {
            return _UserCompanyRepository.GetAll();
        }

        public async Task<UserCompany> GetUserCompanyById(string UserCompanyId)
        {
            return await _UserCompanyRepository.GetById(UserCompanyId);
        }

        public async Task<UserCompany> AddUserCompany(UserCompany UserCompany)
        {
            return await _UserCompanyRepository.Add(UserCompany);
        }

        public async Task<UserCompany> UpdateUserCompany(UserCompany UserCompany)
        {
            return await _UserCompanyRepository.Update(UserCompany);
        }

        public async Task<bool> DeleteUserCompany(string UserCompanyId)
        {
            try
            {
                UserCompany entity = await _UserCompanyRepository.GetById(UserCompanyId);
                if (entity != null){
                    await _UserCompanyRepository.Delete(entity);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public Task<UserCompany> GetUserCompany(string UserCompanyId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserCompanyService
    {
        Task<IEnumerable<UserCompany>> GetAll();
        Task<UserCompany> GetUserCompany(string UserCompanyId);
        Task<UserCompany> AddUserCompany(UserCompany UserCompany);
        Task<UserCompany> UpdateUserCompany(UserCompany UserCompany);
        Task<bool> DeleteUserCompany(string UserCompanyId);
    }

}
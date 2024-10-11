using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using ShiftWork.Backend.Models;
using Microsoft.Extensions.Logging;
using ShiftWork.Backend.Data;


namespace ShiftWork.Backend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ShiftWorkContext _context;
        private readonly ILogger<CompanyService> _logger;
        private readonly ICompanyRepository<Company> _companyRepository;
        //private readonly ShiftWorkContext _context;

        public CompanyService(ShiftWorkContext context,ILogger<CompanyService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
            _companyRepository = new CompanyRepository(_context);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return _companyRepository.GetAll();
        }

        public async Task<Company> GetCompanyById(string companyId)
        {
            return await _companyRepository.GetById(companyId);
        }

        public async Task<Company> AddCompany(Company company)
        {
            return await _companyRepository.Add(company);
        }

        public async Task<Company> UpdateCompany(Company company)
        {
            return await _companyRepository.Update(company);
        }

        public async Task<bool> DeleteCompany(string companyId)
        {
            try
            {
                Company entity = await _companyRepository.GetById(companyId);
                if (entity != null){
                    await _companyRepository.Delete(entity);
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

        public Task<Company> GetCompany(string companyId)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompany(string companyId);
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(Company company);
        Task<bool> DeleteCompany(string companyId);
    }

}
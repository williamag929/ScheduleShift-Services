using System;
using System.Collections.Generic;


namespace ShiftWork.Backend.Services
{
public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _companyRepository;
    //private readonly ShiftWorkContext _context;

    public CompanyService(IRepository<Company> companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public List<Company> GetAllCompanies()
    {
        return _companyRepository.GetAll();
    }

    public Company GetCompanyById(int companyId)
    {
        return _companyRepository.GetById(companyId);
    }

    public void AddCompany(Company company)
    {
        _companyRepository.Add(company);
    }

    public void UpdateCompany(Company company)
    {
        _companyRepository.Update(company);
    }

    public void DeleteCompany(int companyId)
    {
        _companyRepository.Delete(companyId);
    }
}

    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> Get(string companyId);
        Task<Company> Add(Company company);
        Task<Company> Update(Company company);
        Task<bool> Delete(int companyId);
    }

}
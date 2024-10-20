using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftWork.Backend.Services;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(string id)
        {
            var company = await _companyService.GetCompany(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            if (company == null)
            {
                return BadRequest();
            }

            await _companyService.AddCompany(company);
            return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(string id, Company company)
        {
            if (id != company.CompanyId.ToString())
            {
                return BadRequest();
            }

            var entity = await _companyService.UpdateCompany(company);

            if (entity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(string id)
        {
            var deleted = await _companyService.DeleteCompany(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
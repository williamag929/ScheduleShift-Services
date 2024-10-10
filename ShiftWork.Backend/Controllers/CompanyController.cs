using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftWork.Backend.Services;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
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
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

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

            await _companyService.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            var updated = await _companyService.UpdateCompanyAsync(company);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var deleted = await _companyService.DeleteCompanyAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
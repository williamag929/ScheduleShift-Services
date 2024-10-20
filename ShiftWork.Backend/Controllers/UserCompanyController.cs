using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftWork.Backend.Services;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService _UserCompanyService;

        public UserCompanyController(IUserCompanyService UserCompanyService)
        {
            _UserCompanyService = UserCompanyService;
        }

        // GET: api/UserCompany
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompany>>> GetCompanies()
        {
            var companies = await _UserCompanyService.GetAll();
            return Ok(companies);
        }

        // GET: api/UserCompany/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCompany>> GetUserCompany(string id)
        {
            var UserCompany = await _UserCompanyService.GetUserCompany(id);

            if (UserCompany == null)
            {
                return NotFound();
            }

            return Ok(UserCompany);
        }

        // POST: api/UserCompany
        [HttpPost]
        public async Task<ActionResult<UserCompany>> CreateUserCompany(UserCompany UserCompany)
        {
            if (UserCompany == null)
            {
                return BadRequest();
            }

            await _UserCompanyService.AddUserCompany(UserCompany);
            return CreatedAtAction(nameof(GetUserCompany), new { id = UserCompany.UserCompanyId }, UserCompany);
        }

        // PUT: api/UserCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserCompany(string id, UserCompany UserCompany)
        {
            if (id != UserCompany.UserCompanyId.ToString())
            {
                return BadRequest();
            }

            var entity = await _UserCompanyService.UpdateUserCompany(UserCompany);

            if (entity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/UserCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCompany(string id)
        {
            var deleted = await _UserCompanyService.DeleteUserCompany(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
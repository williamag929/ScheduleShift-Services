using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using ShiftWork.Backend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ShiftWork.Backend.Controllers
{
    [Authorize]
    [Route("api/{companyId}/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public RolesController(IRoleService roleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _roleService = roleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        // GET: api/{companyId}/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(string companyId)
        {
            var cacheKey = $"Roles_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Role> roles))
            {
                roles = await _roleService.GetAll(companyId);
                if (roles == null || !roles.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, roles, cacheEntryOptions);
            }

            return Ok(roles);
        }

        // GET: api/{companyId}/Roles/{roleId}
        [HttpGet("{roleId}")]
        public async Task<ActionResult<Role>> GetRole(string companyId, int roleId)
        {
            var cacheKey = $"Role_{companyId}_{roleId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Role role))
            {
                 role = await _roleService.Get(companyId,  roleId );
                if (role == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, role, cacheEntryOptions);
            }

            return Ok(role);
        }

        // PUT: api/{companyId}/Roles
        [HttpPut]
        public async Task<IActionResult> PutRole(string companyId, [FromBody] RoleDto roleDto)
        {
            if (roleDto.RoleId == null)
            {
                return BadRequest("RoleId is required");
            }

            var role = _mapper.Map<Role>(roleDto);
            role.CompanyId = companyId;
            //role.Update = DateTime.UtcNow;

            var updatedRole = await _roleService.Update(role);
            if (updatedRole == null)
            {
                return NotFound();
            }

            var cacheKey = $"Role_{companyId}_{roleDto.RoleId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedRole);
        }

        // POST: api/{companyId}/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(string companyId, [FromBody] RoleDto roleDto)
        {
            var cacheKey = $"Roles_{companyId}";

            var role = _mapper.Map<Role>(roleDto);
            role.CompanyId = companyId;
            //role.Created = DateTime.UtcNow;
            //role.Updated = DateTime.UtcNow;
            //role.IsActive = true;

            var createdRole = await _roleService.Add(role);
            if (createdRole == null)
            {
                return BadRequest("Failed to create role");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetRole), new { companyId, roleId = createdRole.RoleId }, createdRole);
        }

        // DELETE: api/{companyId}/Roles/{roleId}
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string companyId, int roleId)
        {
            var role = await _roleService.Get(companyId,  roleId );
            if (role == null)
            {
                return NotFound();
            }

            var isDeleted = await _roleService.Delete(role.RoleId);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete role");
            }

            var cacheKey = $"Role_{companyId}_{roleId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class RoleService : IRoleService
    {
        private readonly ShiftWorkContext _context;

        public RoleService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all roles
        public async Task<IEnumerable<Role>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Roles
                .Where(r => r.CompanyId == companyId)
                .ToListAsync();
        }

        // Get a role by Id
        public async Task<Role> Get(string companyId, int roleId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Roles.FirstOrDefaultAsync(r => r.CompanyId == companyId && r.RoleId == roleId);
        }

        // Add a new role
        public async Task<Role> Add(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        // Update an existing role
        public async Task<Role> Update(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var existingRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == role.RoleId);

            if (existingRole == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            existingRole.RoleName = role.RoleName;
            //existingRole.Updated = DateTime.UtcNow;

            _context.Roles.Update(existingRole);
            await _context.SaveChangesAsync();

            return existingRole;
        }

        // Delete a role by Id
        public async Task<bool> Delete(int roleId)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == roleId);

            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll(string companyId);
        Task<Role> Get(string companyId, int roleId);
        Task<Role> Add(Role role);
        Task<Role> Update(Role role);
        Task<bool> Delete(int roleId);
    }    
}
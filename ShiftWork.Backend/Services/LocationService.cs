using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class LocationService : ILocationService
    {
        private readonly ShiftWorkContext _context;

        public LocationService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Get all locations
        public async Task<IEnumerable<Location>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Locations
                .Where(c => c.CompanyId == companyId)
                .ToListAsync();
        }

        // Get a location by Id
        public async Task<Location> Get(string companyId, int locationId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.Locations.FirstOrDefaultAsync(c => c.CompanyId == companyId && c.LocationId == locationId);
        }

        // Add a new location
        public async Task<Location> Add(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return location;
        }

        // Update an existing location
        public async Task<Location> Update(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            var existingLocation = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == location.LocationId);

            if (existingLocation == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            existingLocation.LocationName = location.LocationName;
            existingLocation.LocationAddress = location.LocationAddress;
            existingLocation.Updated = DateTime.UtcNow;

            _context.Locations.Update(existingLocation);
            await _context.SaveChangesAsync();

            return existingLocation;
        }

        // Delete a location by Id
        public async Task<bool> Delete(int locationId)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == locationId);

            if (location == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAll(string companyId);
        Task<Location> Get(string companyId, int locationId);
        Task<Location> Add(Location location);
        Task<Location> Update(Location location);
        Task<bool> Delete(int locationId);
    }
}
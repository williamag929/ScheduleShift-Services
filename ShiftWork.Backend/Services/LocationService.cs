using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class LocationService : ILocationService
    {
        private readonly ShiftWorkContext _context;
        private readonly ILogger<CompanyService> _logger;
        private readonly IRepository<Location> _repository;        

        public LocationService(ShiftWorkContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = new LocationRepository(_context);
        }

        // Get all locations
        public async Task<IEnumerable<Location>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }
            var locations = await _repository.GetAll(companyId, null);
            return locations.ToList();
        }

        // Get a location by Id
        public async Task<Location> Get(string companyId, int locationId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(locationId));
            }

            var entity = await _repository.GetById(locationId);
            if (entity == null)
                throw new ArgumentException("Not Found", nameof(locationId));
            
            if (entity.CompanyId != companyId)
                throw new ArgumentException("Company not match", nameof(locationId));            
            return entity;
        }

        // Add a new location
        public async Task<Location> Add(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

           var result = await _repository.Add(location);
            return result;
        }

        // Update an existing location
        public async Task<Location> Update(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            var existingLocation = await _repository.GetById(location.LocationId);

            if (existingLocation == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            var result = await _repository.Update(location);
            await _context.SaveChangesAsync();

            return result;
        }

        // Delete a location by Id
        public async Task<bool> Delete(int locationId)
        {
            var existingLocation = await _repository.GetById(locationId);

            if (existingLocation == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            await _repository.Delete(existingLocation);
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
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
        private readonly ILocationRepository<Location> _repository;        

        public LocationService(ShiftWorkContext context, ILocationRepository<Location> repository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
        public async Task<Location> Add(Location _location)
        {
            if (_location == null)
            {
                throw new ArgumentNullException(nameof(_location));
            }

           var result = await _repository.Add(_location);
            return result;
        }

        // Update an existing location
        public async Task<Location> Update(Location _location)
        {
            if (_location == null)
            {
                throw new ArgumentNullException(nameof(_location));
            }

            var existingLocation = await _repository.GetById(_location.LocationId);

            if (existingLocation == null)
            {
                throw new InvalidOperationException("Location not found");
            }

            existingLocation.LocationName = _location.LocationName;
            existingLocation.CityCode = _location.CityCode;
            existingLocation.CountryCode = _location.CountryCode;
            existingLocation.RatioMax = _location.RatioMax; 
            existingLocation.Ration = _location.Ration;
            existingLocation.Deleted = _location.Deleted;
            existingLocation.GeoLocation = _location.GeoLocation;
            existingLocation.Notification = _location.Notification;
            existingLocation.IsActive = _location.IsActive;
            existingLocation.IsDeleted = _location.IsDeleted;   
            existingLocation.Latitude = _location.Latitude;
            existingLocation.Longitude = _location.Longitude;
            existingLocation.LocationAddress = _location.LocationAddress;
            existingLocation.LocationConfig = _location.LocationConfig;
            existingLocation.StateCode = _location.StateCode;
            existingLocation.TimeZoneId = _location.TimeZoneId;
            existingLocation.ValidateOnSite = _location.ValidateOnSite;
            existingLocation.ValidateRatio = _location.ValidateRatio;

            await _repository.Update(existingLocation);

            return existingLocation;
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
        Task<Location> Add(Location _location);
        Task<Location> Update(Location _location);
        Task<bool> Delete(int locationId);
    }
}
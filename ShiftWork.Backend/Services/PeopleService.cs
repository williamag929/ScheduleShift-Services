using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ShiftWork.Backend.Data;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly ShiftWorkContext _context;
        private readonly IDistributedCache _cache;

        public PeopleService(ShiftWorkContext context, IDistributedCache cache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException();
        }

        // Get all people
        public async Task<IEnumerable<Person>> GetAll(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }
            var cacheKey = $"people_{companyId}";
            var cacheOptions = new DistributedCacheEntryOptions()
               .SetAbsoluteExpiration(TimeSpan.FromMinutes(20))
               .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            var people = await _cache.GetOrSetAsync(cacheKey,
                async () =>
                {
                    return await _context.People
                .Where(p => p.CompanyId == companyId)
                .ToListAsync();
                },
                cacheOptions)!;

            return people;
        }

        // Get a person by Id
        public async Task<Person> Get(string companyId, int personId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company ID cannot be null or empty", nameof(companyId));
            }

            return await _context.People.FirstOrDefaultAsync(p => p.CompanyId == companyId && p.PersonId == personId);
        }

        // Add a new person
        public async Task<Person> Add(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();

            var cacheKey = $"people_{person.CompanyId}";
            _cache.Remove(cacheKey);

            return person;
        }

        // Update an existing person
        public async Task<Person> Update(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var existingPerson = await _context.People
                .FirstOrDefaultAsync(p => p.PersonId == person.PersonId);

            if (existingPerson == null)
            {
                throw new InvalidOperationException("Person not found");
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.Email = person.Email;
            existingPerson.Updated = DateTime.UtcNow;

            _context.People.Update(existingPerson);
            await _context.SaveChangesAsync();

            return existingPerson;
        }

        // Delete a person by Id
        public async Task<bool> Delete(int personId)
        {
            var person = await _context.People
                .FirstOrDefaultAsync(p => p.PersonId == personId);

            if (person == null)
            {
                throw new InvalidOperationException("Person not found");
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            var cacheKey = $"people_{person.CompanyId}";
            _cache.Remove(cacheKey);            

            return true;
        }
    }

    public interface IPeopleService
    {
        Task<IEnumerable<Person>> GetAll(string companyId);
        Task<Person> Get(string companyId, int personId);
        Task<Person> Add(Person person);
        Task<Person> Update(Person person);
        Task<bool> Delete(int personId);
    }
}
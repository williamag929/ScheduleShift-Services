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

namespace ShiftWork.Backend.Controllers
{
    [Authorize]
    [Route("api/{companyId}/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _PersonService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public PersonController(IPersonService PersonService, IMapper mapper, IMemoryCache memoryCache)
        {
            _PersonService = PersonService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        // GET: api/{companyId}/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson(string companyId)
        {
            var cacheKey = $"Person_{companyId}";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Person> Person))
            {
                Person = await _PersonService.GetAll(companyId);
                if (Person == null || !Person.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, Person, cacheEntryOptions);
            }

            return Ok(Person);
        }

        // GET: api/{companyId}/Person/{personId}
        [HttpGet("{personId}")]
        public async Task<ActionResult<Person>> GetPerson(string companyId, int personId)
        {
            var cacheKey = $"Person_{companyId}_{personId}";
            if (!_memoryCache.TryGetValue(cacheKey, out Person person))
            {
                 person = await _PersonService.Get(companyId, personId );
                if (person == null)
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _memoryCache.Set(cacheKey, person, cacheEntryOptions);
            }

            return Ok(person);
        }

        // PUT: api/{companyId}/Person
        [HttpPut]
        public async Task<IActionResult> PutPerson(string companyId, [FromBody] PersonDto personDto)
        {
            if (personDto.PersonId == null)
            {
                return BadRequest("PersonId is required");
            }

            var person = _mapper.Map<Person>(personDto);
            person.CompanyId = companyId;
            person.Updated = DateTime.UtcNow;

            var updatedPerson = await _PersonService.Update(person);
            if (updatedPerson == null)
            {
                return NotFound();
            }

            var cacheKey = $"Person_{companyId}_{personDto.PersonId}";
            _memoryCache.Remove(cacheKey);

            return Ok(updatedPerson);
        }

        // POST: api/{companyId}/Person
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(string companyId, [FromBody] PersonDto personDto)
        {
            var cacheKey = $"Person_{companyId}";

            var person = _mapper.Map<Person>(personDto);
            person.CompanyId = companyId;
            person.Created = DateTime.UtcNow;
            person.Updated = DateTime.UtcNow;
            person.IsActive = true;

            var createdPerson = await _PersonService.Add(person);
            if (createdPerson == null)
            {
                return BadRequest("Failed to create person");
            }
            _memoryCache.Remove(cacheKey);

            return CreatedAtAction(nameof(GetPerson), new { companyId, personId = createdPerson.PersonId }, createdPerson);
        }

        // DELETE: api/{companyId}/Person/{personId}
        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePerson(string companyId, int personId)
        {
            var person  = await _PersonService.Get(companyId, personId);
            if (person == null )
            {
                return NotFound();
            }

            var isDeleted = await _PersonService.Delete(person.PersonId);
            if (!isDeleted)
            {
                return BadRequest("Failed to delete person");
            }

            var cacheKey = $"Person_{companyId}_{personId}";
            _memoryCache.Remove(cacheKey);

            return NoContent();
        }
    }
}
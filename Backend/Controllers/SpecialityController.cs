using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public SpecialityController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialities()
        {
            if(_dataContext.Specialities == null)
            {
                return NotFound();
            }
            return await _dataContext.Specialities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Speciality>> GetSpeciality(int id)
        {
            if(_dataContext.Specialities == null)
            {
                return NotFound();
            }
            var speciality = await _dataContext.Specialities.FindAsync(id);
            if(speciality == null)
            {
                return NotFound();
            }
            return speciality;
        } 
        [HttpPost]
        public async Task<ActionResult<Speciality>> AddSpeciality(Speciality speciality)
        {
            _dataContext.Specialities.Add(speciality);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSpeciality), new { id = speciality.Id }, speciality);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Speciality>> UpdateSpecialiy(int id, Speciality speciality)
        {
            if(id != speciality.Id)
            {
                return BadRequest();
            }
            _dataContext.Update(speciality);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!SpecialityExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool SpecialityExists(int id)
        {
            return (_dataContext.Specialities?.Any(speciality => speciality.Id == id)).GetValueOrDefault();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Speciality>> DeleteSpeciality(int id)
        {
            if(_dataContext.Specialities == null)
            {
                return NotFound();
            }
            var speciality = await _dataContext.Specialities.FindAsync(id);
            if(speciality is null)
            {
                return NotFound();
            }
            _dataContext.Specialities.Remove(speciality);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
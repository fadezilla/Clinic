using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ClinicController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
        {
            if(_dataContext.Clinics == null)
            {
                return NotFound();
            }
            return await _dataContext.Clinics.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Clinic>> GetClinic(int id)
        {
            if(_dataContext.Clinics == null)
            {
                return NotFound();
            }
            var clinic = await _dataContext.Clinics.FindAsync(id);
            if(clinic is null)
            {
                return NotFound();
            }
            return clinic;
        }
        [HttpPost]
        public async Task<ActionResult<Clinic>> AddClinic(Clinic clinic)
        {
            _dataContext.Clinics.Add(clinic);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClinic), new { id = clinic.Id }, clinic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Clinic>> UpdateClinic(int id, Clinic clinic)
        {
            if(id != clinic.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(clinic);
            try{
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ClinicExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool ClinicExists(int id)
        {
            return (_dataContext.Clinics?.Any(clinic => clinic.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Clinic>> DeleteClinic(int id)
        {
            if(_dataContext.Clinics == null)
            {
                return NotFound();
            }
            var clinic = await _dataContext.Clinics.FindAsync(id);
            if(clinic is null)
            {
                return NotFound();
            }
            _dataContext.Clinics.Remove(clinic);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController: ControllerBase
    {
        private readonly DataContext _dataContext;
        public PatientController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            if(_dataContext.Patients == null)
            {
                return NotFound();
            }
            return await _dataContext.Patients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            if(_dataContext.Patients == null)
            {
                return NotFound();
            }
            var patient = await _dataContext.Patients.FindAsync(id);
            if(patient == null)
            {
                return NotFound();
            }
            return patient;
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _dataContext.Patients.Add(patient);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int Id, Patient patient)
        {
            if(Id != patient.Id)
            {
                return BadRequest();
            }
            _dataContext.Update(patient);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!PatientExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool PatientExists(int Id)
        {
            return (_dataContext.Patients?.Any(patient => patient.Id == Id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            if(_dataContext.Patients == null)
            {
                return NotFound();
            }
            var patient = await _dataContext.Patients.FindAsync(id);
            if(patient is null)
            {
                return NotFound();
            }
            _dataContext.Patients.Remove(patient);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
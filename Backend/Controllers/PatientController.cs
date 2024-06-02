using Backend.Models;
using Backend.Data;
using Backend.ControllerTools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        public async Task<ActionResult<IEnumerable<PatientTool>>> GetPatients()
        {
            var patient = await _dataContext.Patients
                .Include(p => p.Appointments)
                .Select(p => new PatientTool
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    Email = p.Email,
                    Appointments = p.Appointments.Select(a => new AppointmentDetails
                    {
                        Id = a.Id,
                        Category = a.Category,
                        Date = a.Date,
                    }).ToList()
                }).ToListAsync();
                
                if(patient == null)
                {
                    return NotFound();
                }
                return patient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientTool>> GetPatient(int id)
        {
            var patient = await _dataContext.Patients
                .Where(p => p.Id == id)
                .Include(p => p.Appointments)
                .Select(p => new PatientTool
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    Email = p.Email,
                    Appointments = p.Appointments.Select(a => new AppointmentDetails
                    {
                        Id = a.Id,
                        Category = a.Category,
                        Date = a.Date,
                    }).ToList()
                }).FirstOrDefaultAsync();

                if(patient == null)
                {
                    return NotFound();
                }

                return patient;
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient(Patient patient)
        {
            var existingPatient = await _dataContext.Patients.FirstOrDefaultAsync(p => p.Email == patient.Email);

            if(existingPatient != null)
            {
                return BadRequest("Patient with this email already exists.");
            }
            
            _dataContext.Patients.Add(patient);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _dataContext.Update(patient);

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool PatientExists(int id)
        {
            return (_dataContext.Patients?.Any(patient => patient.Id == id)).GetValueOrDefault();
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

            if(patient.Appointments != null){
                _dataContext.Appointments.RemoveRange(patient.Appointments);
            }

            _dataContext.Patients.Remove(patient);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
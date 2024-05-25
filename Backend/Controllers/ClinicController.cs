using Backend.Data;
using Backend.Models;
using Backend.ControllerTools;
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
        public async Task<ActionResult<IEnumerable<ClinicTool>>> GetClinics()
        {
            var clinics = await _dataContext.Clinics
                .Include(c => c.Doctors)
                    .ThenInclude(d => d.Speciality)
                .Include(c => c.Appointments)
                .Select(c => new ClinicTool
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    Doctors = c.Doctors.Select(d => new DoctorDetails
                    {
                        Id = d.Id,
                        Name = d.FirstName + " " + d.LastName,
                    }).ToList(),
                    Appointments = c.Appointments.Select(a => new AppointmentDetails
                    {
                        Id = a.Id,
                        Category = a.Category,
                        Date = a.Date
                    }).ToList()
                }).ToListAsync();
            if (clinics == null)
            {
                return NotFound();
            }
            return clinics;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicTool>> GetClinic(int id)
        {
            var clinic = await _dataContext.Clinics
                .Where(c => c.Id == id)
                .Include(c => c.Doctors)
                    .ThenInclude(d => d.Speciality)
                .Include(c => c.Appointments)
                .Select(c => new ClinicTool
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    Doctors = c.Doctors.Select(d => new DoctorDetails
                    {
                        Id = d.Id,
                        Name = d.FirstName + " " + d.LastName,
                    }).ToList(),
                    Appointments = c.Appointments.Select(a => new AppointmentDetails
                    {
                        Id = a.Id,
                        Category = a.Category,
                        Date = a.Date
                    }).ToList()
                }).FirstOrDefaultAsync();

            if(clinic == null)
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
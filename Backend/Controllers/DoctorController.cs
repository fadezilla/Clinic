using Backend.Data;
using Backend.Models;
using Backend.ControllerTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DoctorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorTool>>> GetDoctors()
        {
            var doctors = await _dataContext.Doctors
                .Include(d => d.Speciality)
                .Include(d => d.Clinic)
                .Select(d => new DoctorTool
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    SpecialityId = d.Speciality.Id,
                    SpecialityName = d.Speciality.Name,
                    ClinicId = d.Clinic.Id,
                    ClinicName = d.Clinic.Name
                }).ToListAsync();

            return doctors;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorTool>> GetDoctor(int id)
        {
            var doctor = await _dataContext.Doctors
                .Where(d => d.Id == id)
                .Include(d => d.Speciality)
                .Include(d => d.Clinic)
                .Select(d => new DoctorTool
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    SpecialityId = d.Speciality.Id,
                    SpecialityName = d.Speciality.Name,
                    ClinicId = d.Clinic.Id,
                    ClinicName = d.Clinic.Name
                }).FirstOrDefaultAsync();

                if(doctor == null)
                {
                    return NotFound();
                }
                return doctor;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorSearchResult>>> SearchDoctor(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var doctors = await _dataContext.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Speciality)
                .Where(d => d.FirstName.Contains(query) || d.LastName.Contains(query))
                .Select(d => new DoctorSearchResult
                {
                    FullName = d.FirstName + " " + d.LastName,
                    ClinicName = d.Clinic.Name,
                    SpecialityName = d.Speciality.Name
                }).ToListAsync();

            if (doctors == null || doctors.Count == 0)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _dataContext.Update(doctor);

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool DoctorExists(int id)
        {
            return (_dataContext.Doctors?.Any(doctor => doctor.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            if(_dataContext.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _dataContext.Doctors.FindAsync(id);
            if(doctor == null)
            {
                return NotFound();
            }
            _dataContext.Doctors.Remove(doctor);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
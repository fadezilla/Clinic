using Backend.Data;
using Backend.Models;
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
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            if(_dataContext.Doctors == null)
            {
                return NotFound();
            }
            return await _dataContext.Doctors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            if(_dataContext.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _dataContext.Doctors.FindAsync(id);
            if(doctor is null)
            {
                return NotFound();
            }
            return doctor;
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
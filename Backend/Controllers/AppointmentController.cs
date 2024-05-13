using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public AppointmentsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public class AppointmentTool
        {
            public int Id { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
            public string Patient { get; set; }
            public int PatientId { get; set; }
            public string Clinic { get; set; }
            public int ClinicId { get; set; }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentTool>>> GetAppointments()
        {
            var appointment = await _dataContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Clinic)
                .Select(a => new AppointmentTool
                {
                    Id = a.Id,
                    Category = a.Category,
                    Date = a.Date,
                    PatientId = a.PatientId,
                    ClinicId = a.ClinicId,
                    Patient = a.Patient.FirstName + " " + a.Patient.LastName,
                    Clinic = a.Clinic.Name
                }).ToListAsync();

            if(appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentTool>> GetAppointment(int id)
        {
            var appointment = await _dataContext.Appointments
                .Where(a => a.Id == id)
                .Include(a => a.Patient)
                .Include(a => a.Clinic)
                .Select(a => new AppointmentTool
                {
                    Id = a.Id,
                    Category = a.Category,
                    Date = a.Date,
                    PatientId = a.PatientId,
                    ClinicId = a.ClinicId,
                    Patient = a.Patient.FirstName + " " + a.Patient.LastName,
                    Clinic = a.Clinic.Name
                }).FirstOrDefaultAsync();

            if(appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }
        [HttpPost]
        public async Task<ActionResult<Appointment>> AddAppointment(Appointment appointment)
        {
            _dataContext.Appointments.Add(appointment);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(int id, Appointment appointment)
        {
            if(id != appointment.Id)
            {
                return BadRequest();
            }
            _dataContext.Update(appointment);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!AppointmentExists(id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool AppointmentExists(int id)
        {
            return (_dataContext.Appointments?.Any(appointment => appointment.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            if(_dataContext.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _dataContext.Appointments.FindAsync(id);
            if(appointment is null)
            {
                return NotFound();
            }
            _dataContext.Appointments.Remove(appointment);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
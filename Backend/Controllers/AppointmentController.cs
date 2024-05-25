using Backend.Data;
using Backend.Models;
using Backend.ControllerTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        private bool ValidateSSN(long ssn)
        {
            var ssnString = ssn.ToString();
            if (ssnString.Length != 11) return false;

            if (!Regex.IsMatch(ssnString, @"^\d{11}$")) return false;

            string datePart = ssnString.Substring(0, 6);
            if (!DateTime.TryParseExact(datePart, "ddMMyy", null, System.Globalization.DateTimeStyles.None, out _)) return false;

            return true;
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

            if (appointment == null)
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

            if (appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> AddAppointment(BookingTool dto)
        {
            if (!ValidateSSN(dto.SocialSecurityNumber))
            {
                return BadRequest("Invalid Social Security Number. It must be 11 digits long and the first 6 digits must be a valid date in the format ddMMyy.");
            }

            if (!DateTime.TryParseExact(dto.Date, "dd.MM.yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime appointmentStartTime))
            {
                return BadRequest("Invalid date format. Please use 'dd.MM.yyyy HH:mm'.");
            }

            var patient = await _dataContext.Patients.FirstOrDefaultAsync(p => p.Email == dto.Patient.Email);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName,
                    Birthdate = dto.Patient.Birthdate,
                    Email = dto.Patient.Email
                };
                _dataContext.Patients.Add(patient);
                await _dataContext.SaveChangesAsync();
            }

            var appointmentEndTime = appointmentStartTime.AddHours(1);

            var conflictingAppointment = await _dataContext.Appointments
                .AnyAsync(a => (a.ClinicId == dto.ClinicId || a.PatientId == patient.Id) && a.Date >= appointmentStartTime && a.Date < appointmentEndTime);

            if (conflictingAppointment)
            {
                return BadRequest("There is already an appointment at this time either for the same patient or at the same clinic.");
            }

            var appointment = new Appointment
            {
                Category = dto.Category,
                Date = appointmentStartTime,
                PatientId = patient.Id,
                ClinicId = dto.ClinicId
            };
            _dataContext.Appointments.Add(appointment);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointment(int id, BookingTool dto)
        {
            var appointment = await _dataContext.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if (!ValidateSSN(dto.SocialSecurityNumber))
            {
                return BadRequest("Invalid Social Security Number. It must be 11 digits long and the first 6 digits must be a valid date in the format ddMMyy.");
            }

            if (!DateTime.TryParseExact(dto.Date, "dd.MM.yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime appointmentStartTime))
            {
                return BadRequest("Invalid date format. Please use 'dd.MM.yyyy HH:mm'.");
            }

            var patient = await _dataContext.Patients.FirstOrDefaultAsync(p => p.Email == dto.Patient.Email);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName,
                    Birthdate = dto.Patient.Birthdate,
                    Email = dto.Patient.Email
                };
                _dataContext.Patients.Add(patient);
                await _dataContext.SaveChangesAsync();
            }

            var appointmentEndTime = appointmentStartTime.AddHours(1);

            var conflictingAppointment = await _dataContext.Appointments
                .AnyAsync(a => a.Id != id && (a.ClinicId == dto.ClinicId || a.PatientId == patient.Id) && a.Date >= appointmentStartTime && a.Date < appointmentEndTime);

            if (conflictingAppointment)
            {
                return BadRequest("There is already an appointment at this time either for the same patient or at the same clinic.");
            }

            appointment.Category = dto.Category;
            appointment.Date = appointmentStartTime;
            appointment.PatientId = patient.Id;
            appointment.ClinicId = dto.ClinicId;

            _dataContext.Appointments.Update(appointment);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
            {
                if (_dataContext.Appointments == null)
                {
                    return NotFound();
                }
                var appointment = await _dataContext.Appointments.FindAsync(id);
                if (appointment is null)
                {
                    return NotFound();
                }
                _dataContext.Appointments.Remove(appointment);
                await _dataContext.SaveChangesAsync();
                return NoContent();
            }
    }
}
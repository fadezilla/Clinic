namespace Backend.ControllerTools
{
    public class DoctorDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DoctorSearchResult
    {
        public string FullName { get; set; }
        public string ClinicName { get; set; }
        public string SpecialityName { get; set; }
    }

    public class AppointmentDetails
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
    public class ClinicTool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        public List<DoctorDetails> Doctors { get; set; } = new List<DoctorDetails>();
        public List<AppointmentDetails> Appointments { get; set; } = new List<AppointmentDetails>();
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
    public class BookingTool
    {
        public string Category { get; set; }
        public string Date { get; set; }
        public long SocialSecurityNumber { get; set; }
        public int ClinicId { get; set; }
        public PatientTool Patient { get; set; }

    }
    public class DoctorTool
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpecialityName { get; set; }
        public int SpecialityId { get; set; }
        public string ClinicName { get; set; }
        public int ClinicId { get; set; }
    }
    public class PatientTool
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public List<AppointmentDetails> Appointments { get; set; } = new List<AppointmentDetails>();
    }

    public class SpecialityTool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorDetails> Doctors { get; set; } = new List<DoctorDetails>();
        
    }
}
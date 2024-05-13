namespace Backend.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Speciality? Speciality { get; set; }
        public int SpecialityId { get; set; }

        public Clinic? Clinic { get; set; }
        public int ClinicId { get; set; }
    }
}
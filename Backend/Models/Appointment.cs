namespace Backend.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }
        public virtual Patient Patient{ get; set; }
        public int PatientId { get; set; }
        public virtual Clinic Clinic { get; set; }
        public int ClinicId { get; set; }
    }
}
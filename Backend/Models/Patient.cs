namespace Backend.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
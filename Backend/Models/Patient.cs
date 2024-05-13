namespace Backend.Models
{
    public class Patient
    {
        public Patient(int Id, string FirstName, string LastName, string Birthdate, string Email, string SocialSecurityNumber)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Birthdate = Birthdate;
            this.Email = Email;
            this.SocialSecurityNumber = SocialSecurityNumber;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public string SocialSecurityNumber { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
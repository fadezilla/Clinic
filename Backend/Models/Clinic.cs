namespace Backend.Models
{
    public class Clinic
    {
        public Clinic(int Id, string Name, string Address, int PhoneNumber)
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
            this.PhoneNumber = PhoneNumber;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
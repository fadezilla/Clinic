namespace Backend.Models
{
    public class Clinic
    {
        public Clinic(int Id, string Name, string Address)
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
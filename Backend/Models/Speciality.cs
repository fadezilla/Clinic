namespace Backend.Models
{
    public class Speciality
    {
        public Speciality(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
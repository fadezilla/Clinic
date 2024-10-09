using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialityId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Clinic)
                .WithMany(c => c.Doctors)
                .HasForeignKey(d => d.ClinicId);
            
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);
                
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Clinic)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClinicId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Speciality>()
                .HasMany(s => s.Doctors)
                .WithOne(d => d.Speciality)
                .HasForeignKey(d => d.SpecialityId);

            modelBuilder.Entity<Clinic>()           
                .HasMany(c => c.Doctors)
                .WithOne(d => d.Clinic)
                .HasForeignKey(d => d.ClinicId);

            modelBuilder.Entity<Clinic>()
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicId);

            //Dummy data, Delete this if you dont want any data in the Database

            modelBuilder.Entity<Speciality>().HasData(
                new Speciality(1, "General Practitioner"),
                new Speciality(2, "Dermatologist"),
                new Speciality(3, "Cardiologist")
            );

            modelBuilder.Entity<Clinic>().HasData(
                new Clinic(1, "Vegen", "Street1", 123456789),
                new Clinic(2, "Medic", "Street2", 987654321),
                new Clinic(3, "Health", "Street3", 456789123)
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "John", LastName = "Doe", SpecialityId = 1, ClinicId = 1 },
                new Doctor { Id = 2, FirstName = "Jane", LastName = "Smith", SpecialityId = 2, ClinicId = 2 }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FirstName = "Alice", LastName = "Doe", Birthdate = "1990-01-01", Email = "Alice@hotmail.com"},
                new Patient { Id = 2, FirstName = "Bob", LastName = "Smith", Birthdate = "1995-05-05", Email = "Bob@hotmail.com"}
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, Category = "Checkup", Date = "2021-06-01 14:00", Duration = 60, PatientId = 1, ClinicId = 1 },
                new Appointment { Id = 2, Category = "Consultation", Date = "2021-06-02 15:00", Duration = 60, PatientId = 2, ClinicId = 2 }
            );
        }
    }
}
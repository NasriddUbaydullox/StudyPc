using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.DataAccess
{
	public class Context : DbContext
	{
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Users { get; set; }
		public DbSet<PatientBlank> PatientBlanks { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Role> Roles { get; set; }
		public Context(DbContextOptions<Context> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Appointment>(builder =>
			{
				builder.HasKey(r => r.Id);

				builder.HasOne(r => r.Patient)
					.WithMany(r => r.Appointments);


			});

			modelBuilder.Entity<Doctor>(builder =>
			{
				builder.HasKey(r => r.Id);


			});

			modelBuilder.Entity<Patient>(builder =>
			{
				builder.HasKey(r => r.Id);
			});

			modelBuilder.Entity<PatientBlank>(builder =>
			{
				builder.HasKey(r => r.Id);

				builder.HasOne(r => r.patient)
					.WithOne(r => r.PatientBlank)
					.HasForeignKey<PatientBlank>(r => r.PatientId);
			});

			modelBuilder.Entity<Speciality>(builder =>
			{
				builder.HasKey(r => r.Id);
			});





			modelBuilder.Entity<User>(builder =>
			{
				builder.HasOne(r => r.Role)
					.WithMany(r => r.Users)
					.HasForeignKey(r => r.RoleId);


				modelBuilder.Entity<Role>(builder =>
			{
				builder.HasMany(r => r.Permissions)
					.WithMany();

				builder.HasData(new Role[]
				{
					new Role()
					{
						RoleId = 1,
						Name = "super_admin",
						IsActive = true,
					},
					new Role()
					{
						RoleId = 2,
						Name = "user",
						IsActive = true,
					}
				});
			});
			});
		}
	}
}

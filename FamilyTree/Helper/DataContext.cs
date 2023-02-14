using FamilyTree.Entity;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Helper
{
    public partial class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration) => Configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("Default"));
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<PersonFamily> PersonFamily { get; set; }
        public DbSet<PersonSpouse> PersonSpouse { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Location> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Job>(x => { x.Property(y => y.Title).HasMaxLength(300); });
            builder.Entity<Location>(x => { x.Property(y => y.Title).HasMaxLength(300); });

            builder.Entity<Person>(x =>
            {
                x.Property(y => y.FirstName).HasMaxLength(300);
                x.Property(y => y.LastName).HasMaxLength(300);
                x.Property(y => y.BirthDate).HasColumnType("datetime");
                x.Property(y => y.DeathDate).HasColumnType("datetime");
                x.Property(y => y.Phone).HasMaxLength(50);
                x.Property(y => y.Note).HasMaxLength(4000);

                x.HasOne(y => y.Job).WithMany(y => y.Person).HasForeignKey(y => y.JobId).HasConstraintName("FK_Person_Job");
                x.HasOne(y => y.Location).WithMany(y => y.Person).HasForeignKey(y => y.LocationId).HasConstraintName("FK_Person_Location");
            });

            builder.Entity<PersonFamily>(x =>
            {
                x.HasKey(y => y.PersonId);

                x.HasOne(y => y.Person).WithOne(y => y.PersonFamily).HasForeignKey<PersonFamily>(y => y.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PersonFamily_Person");
                x.HasOne(y => y.Father).WithMany(y => y.FatherOfPersons).HasForeignKey(y => y.FatherId).HasConstraintName("FK_PersonFamily_Father");
                x.HasOne(y => y.Mother).WithMany(y => y.MotherOfPersons).HasForeignKey(y => y.MotherId).HasConstraintName("FK_PersonFamily_Mother");
            });

            builder.Entity<PersonSpouse>(x =>
            {
                x.HasOne(y => y.Person).WithMany(y => y.PersonSpouse)
                .HasForeignKey(y => y.PersonId)
                .HasConstraintName("FK_PersonSpouse_Person");

                x.HasOne(y => y.Spouse).WithMany(y => y.SpousePerson)
                    .HasForeignKey(y => y.SpouseId)
                    .HasConstraintName("FK_PersonSpouse_Spouse");
            });

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

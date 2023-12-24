using DataAcceseLayer.Entities;
using DataAcceseLayer.Entities.Resumes;
using DataAcceseLayer.Entities.Vacancies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace DataAcceseLayer.DbContext;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Certificate> GetCertificates { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Apply> Applies { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Certificate>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(555);

        modelBuilder.Entity<Certificate>()
            .Property(c => c.Url)
            .IsRequired()
            .HasMaxLength(555);

        modelBuilder.Entity<Certificate>()
            .HasOne(c => c.User)
            .WithMany(u => u.Certificates)
            .HasForeignKey(c => c.UserId);



        modelBuilder.Entity<Skill>()
            .Property(s => s.Name)
           .IsRequired()
           .HasMaxLength(100); 

        modelBuilder.Entity<Skill>()
            .HasOne(s => s.User)
            .WithMany(u => u.Skills)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<Education>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<Education>()
            .Property(e => e.Specialty)
            .HasMaxLength(500);

        modelBuilder.Entity<Education>()
            .HasOne(e => e.User)
            .WithMany(u => u.Educations)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Apply>()
            .HasKey(a => new { a.JobId, a.UserId });

        modelBuilder.Entity<Apply>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applies)
            .HasForeignKey(a => a.JobId);

        modelBuilder.Entity<Apply>()
            .HasOne(a => a.User)
            .WithMany(u => u.Applies)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Specify ON DELETE NO ACTION

        modelBuilder.Entity<Job>()
            .Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<Job>()
            .Property(j => j.Location)
            .HasMaxLength(500);

        modelBuilder.Entity<Job>()
            .Property(j => j.Description)
            .HasMaxLength(2000);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.User)
            .WithMany(u => u.Jobs)
            .HasForeignKey(j => j.UserId);



        base.OnModelCreating(modelBuilder);
    }
}

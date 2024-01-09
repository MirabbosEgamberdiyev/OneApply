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

        modelBuilder.Entity<Language>()
            .Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Language>()
            .HasOne(l => l.User)
            .WithMany(u => u.Languages)
            .HasForeignKey(l => l.UserId);

        modelBuilder.Entity<Link>()
            .Property(l => l.Url)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<Link>()
            .HasOne(l => l.User)
            .WithMany(u => u.Links)
            .HasForeignKey(l => l.UserId);
        modelBuilder.Entity<Project>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<Project>()
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(2000);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Skill>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Skill>()
            .HasOne(s => s.User)
            .WithMany(u => u.Skills)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<WorkExperience>()
            .Property(w => w.CompanyName)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<WorkExperience>()
            .Property(w => w.CompanyUrl)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<WorkExperience>()
            .Property(w => w.Position)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder.Entity<WorkExperience>()
            .Property(w => w.Description)
            .IsRequired()
            .HasMaxLength(2000);

        modelBuilder.Entity<WorkExperience>()
            .HasOne(w => w.User)
            .WithMany(u => u.WorkExperiences)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<Apply>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applies)
            .HasForeignKey(a => a.JobId);

        modelBuilder.Entity<Apply>()
            .HasOne(a => a.User)
            .WithMany(u => u.Applies)
            .HasForeignKey(a => a.UserId);

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

        modelBuilder.Entity<Apply>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applies)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);


        #region User o'chsa hamma malumot o'chishi
        modelBuilder.Entity<User>()
            .HasMany(u => u.WorkExperiences)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Certificates)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Educations)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Languages)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Links)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Projects)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Skills)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Applies)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Jobs)
            .WithOne(j => j.User)
            .HasForeignKey(j => j.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}

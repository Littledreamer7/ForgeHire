using ForgeHire.Models;
using ForgeHire.Models.Company_Model;
using ForgeHire.Models.Job_Application_Model;
using ForgeHire.Models.Job_Model;
using ForgeHire.Models.User_Model;
using Microsoft.EntityFrameworkCore;


namespace ForgeHire.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // OTP Authentication
        public DbSet<OtpRecord> OtpRecords { get; set; }
        public DbSet<OtpBlock> OtpBlocks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Candidate
        public DbSet<Candidate> Candidates { get; set; }

        // Company
        public DbSet<Company_Info> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<User> Users { get; set; }

        // Jobs
        public DbSet<Job> Jobs { get; set; }
   
        // Job Applications
        public DbSet<JobApplication> JobApplications { get; set; }

        // Roles
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Candidate Mobile Unique
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.MobileNumber)
                .IsUnique();

            // Company Mobile Unique
            modelBuilder.Entity<Models.Company_Model.Company_Info>()
                .HasIndex(c => c.MobileNumber)
                .IsUnique();

            modelBuilder.Entity<JobApplication>()
               .HasOne(x => x.Job)
               .WithMany()
               .HasForeignKey(x => x.JobId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.Company)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

namespace Nakshatra.HostedServices.Services.Contexts;

public class ProfileContext : DbContext
{
    public ProfileContext()
    {
    }

    public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .Property(p => p.ProfileId);

        modelBuilder.Entity<Profile>()
          .HasPartitionKey(nameof(Profile.ProfileId))
          .OwnsOne(p => p.PersonalDetails)
          .HasKey(p => p.PersonalId);

        modelBuilder.Entity<Profile>()
            .HasPartitionKey(nameof(Profile.ProfileId))
            .OwnsMany(p => p.AddressDetails)
            .HasKey(p => p.AddressId);

        modelBuilder.Entity<Profile>()
            .HasPartitionKey(nameof(Profile.ProfileId))
            .OwnsMany(p => p.BlogDetails)
            .HasKey(p => p.BlogId);


        modelBuilder.Entity<Profile>()
           .HasPartitionKey(nameof(Profile.ProfileId))
           .OwnsMany(p => p.CompanyDetails)
           .HasKey(p => p.CompanyId);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.ProfileId))
         .OwnsMany(p => p.EducationDetails)
         .HasKey(p => p.EducationId);

        modelBuilder.Entity<Profile>()
          .HasPartitionKey(nameof(Profile.ProfileId))
          .OwnsMany(p => p.CertificationDetails)
          .HasKey(p => p.CertificateId);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.ProfileId))
         .OwnsMany(p => p.ExperienceDetails)
         .HasKey(p => p.ExperienceId);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.ProfileId))
         .OwnsMany(p => p.SkillDetails)
         .HasKey(p => p.SkillId);

        modelBuilder.Entity<Profile>()
        .HasPartitionKey(nameof(Profile.ProfileId))
        .OwnsMany(p => p.ProjectDetails)
        .HasKey(p => p.ProjectId);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.ProfileId))
         .OwnsMany(p => p.WorkFlowDetails)
         .HasKey(p => p.WorkFlowId);

        modelBuilder.Entity<Profile>()
        .HasPartitionKey(nameof(Profile.ProfileId))
        .OwnsMany(p => p.SocialDetails)
        .HasKey(p => p.SocialId);
    }
}

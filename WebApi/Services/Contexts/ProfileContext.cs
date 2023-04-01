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
            .ToContainer("Profiles");

        modelBuilder.Entity<Profile>()
            .Property(p => p.Id)
            .HasConversion<string>()
            .HasValueGenerator<SequentialGuidValueGenerator>();

        modelBuilder.Entity<Profile>()
          .HasPartitionKey(nameof(Profile.Id))
          .OwnsOne(p => p.PersonalDetails);

        modelBuilder.Entity<Profile>()
            .HasPartitionKey(nameof(Profile.Id))
            .OwnsMany(p => p.AddressDetails);

        modelBuilder.Entity<Profile>()
            .HasPartitionKey(nameof(Profile.Id))
            .OwnsMany(p => p.BlogDetails);

        modelBuilder.Entity<Profile>()
           .HasPartitionKey(nameof(Profile.Id))
           .OwnsMany(p => p.CompanyDetails);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.Id))
         .OwnsMany(p => p.EducationDetails);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.Id))
         .OwnsMany(p => p.ExperienceDetails);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.Id))
         .OwnsMany(p => p.SkillDetails);

        modelBuilder.Entity<Profile>()
         .HasPartitionKey(nameof(Profile.Id))
         .OwnsMany(p => p.WorkFlowDetails);
    }
}

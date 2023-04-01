using Nakshatra.HostedServices.Services.Contexts;
using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

namespace Nakshatra.HostedServices.Services.Repositories;
public class ProfileRepository : IProfileRepository
{
    private readonly ProfileContext _context;

    public ProfileRepository(ProfileContext profileContext) => _context = profileContext;

    public async Task<Profile> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var keyValues = new object[] { id };
            var profile = await _context.Set<Profile>().FindAsync(keyValues, cancellationToken);
            return profile;
        }
        catch
        {
            throw;
        }
    }
    public async Task<Profile> AddAsync(Profile entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await this._context.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteAsync(Profile entity, CancellationToken cancellationToken = default)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Profile entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IReadOnlyList<Profile>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var profiles = await _context.Profiles.ToListAsync();

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;

            return profiles;
        }
        catch
        {
            throw;
        }
    }
}

using Api.Entities.Profile;
using Microsoft.EntityFrameworkCore;
using Services.Contexts;
using System.Diagnostics;

namespace Services.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext profileContext) => _context = profileContext;

        public async Task<Profile> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var keyValues = new object[] { int.Parse(id) };
                var profile = await this._context.Set<Profile>().FindAsync(keyValues, cancellationToken);
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

                await this._context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(Profile entity, CancellationToken cancellationToken = default)
        {
            this._context.Remove(entity);
            await this._context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Profile entity, CancellationToken cancellationToken = default)
        {
            try
            {
                this._context.Update(entity);
                await this._context.SaveChangesAsync(cancellationToken);
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

                var profiles = await this._context.Profiles.ToListAsync();

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
}

using API.Model;

namespace API.Repositories
{
    public interface IProfileRepository
    {
        Profile GetProfile(int profileId);
    }
}

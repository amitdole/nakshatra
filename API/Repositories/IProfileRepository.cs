﻿using API.Model.Profile;

namespace API.Repositories
{
    public interface IProfileRepository
    {
        ProfileInfo GetProfile(int profileId);
    }
}

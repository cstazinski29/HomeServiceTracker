﻿using HomeServiceTracker.Shared.Models.HomeInfo;

namespace HomeServiceTracker.Server.Services.HomeInfo
{
    public interface IHomeInfoService
    {
        Task<IEnumerable<HomeInfoListItem>> GetAllHomeInfoAsync();
        Task<bool> CreateHomeInfoAsync(HomeInfoCreate model);
        Task<HomeInfoDetail> GetHomeInfoByIdAsync(int homeId);
        Task<bool> UpdateHomeInfoAsync(HomeInfoEdit model);
        Task<bool> DeleteHomeInfoAsync(int homeId);
        // userId was previously a string, I changed to guid as that was what marty had in his
        void SetUserId(Guid userId);
        Task<bool> SeedHomeInfoAsync();
    }
}

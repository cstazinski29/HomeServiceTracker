using HomeServiceTracker.Shared.Models.HomeInfo;

namespace HomeServiceTracker.Server.Services.HomeInfo
{
    public interface IHomeInfoService
    {
        Task<IEnumerable<HomeInfoListItem>> GetAllHomeInfoAsync();
        Task<bool> CreateHomeInfoAsync(HomeInfoCreate model);
        Task<HomeInfoDetail> GetHomeInfoByIdAsync(int homeId);
        Task<bool> UpdateHomeInfoAsync(HomeInfoEdit model);
        Task<bool> DeleteHomeInfoAsync(int homeId);
        Task<bool> DeleteHomeInfoAsync(string userId);
        void SetUserId(string userId);
    }
}

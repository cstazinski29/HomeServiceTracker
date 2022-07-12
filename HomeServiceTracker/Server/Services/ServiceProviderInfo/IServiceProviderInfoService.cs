using HomeServiceTracker.Shared.Models.ServiceProviderInfo;

namespace HomeServiceTracker.Server.Services.ServiceProviderInfo
{
    public interface IServiceProviderInfoService
    {
        Task<IEnumerable<ServiceProviderInfoListItem>> GetAllServiceProviderInfosAsync();
        Task<bool> CreateServiceProviderInfoAsync(ServiceProviderInfoCreate model);
        Task<ServiceProviderInfoDetail> GetServiceProviderInfoByIdAsync(int serviceProviderInfoId);
        Task<bool> UpdateServiceProviderInfoAsync(ServiceProviderInfoEdit model);
        Task<bool> DeleteServiceProviderInfoAsync(int serviceProviderInfoId);
        Task<bool> DeleteServiceProviderInfoAsync(string userId);
    }
}

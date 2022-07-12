using HomeServiceTracker.Shared.Models.ServiceItem;

namespace HomeServiceTracker.Server.Services.ServiceItem
{
    public interface IServiceItemService
    {
        Task<IEnumerable<ServiceItemListItem>> GetAllServiceItemsAsync();
        Task<bool> CreateServiceItemAsync(ServiceItemCreate model);
        Task<ServiceItemDetail> GetServiceItemByIdAsync(int serviceItemId);
        Task<bool> UpdateServiceItemAsync(ServiceItemEdit model);
        Task<bool> DeleteServiceItemAsync(int serviceItemId);
        Task<bool> DeleteServiceItemAsync(string userId);
    }
}

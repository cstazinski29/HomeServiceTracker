using HomeServiceTracker.Shared.Models.ScheduledService;

namespace HomeServiceTracker.Server.Services.ScheduledService
{
    public interface IScheduledServiceService
    {
        Task<IEnumerable<ScheduledServiceListItem>> GetAllScheduledServicesAsync();
        Task<bool> CreateScheduledServiceAsync(ScheduledServiceCreate model);
        Task<ScheduledServiceDetail> GetScheduledServiceByIdAsync(int scheduledServiceId);
        Task<bool> UpdateScheduledServiceAsync(ScheduledServiceEdit model);
        Task<bool> DeleteScheduledServiceAsync(int scheduledServiceId);
        Task<IEnumerable<ScheduledServiceListItem>> GetAllScheduledServiceByHomeIdAsync(int homeId);
        void SetUserId(Guid userId);

        Task<bool> SeedScheduledServicesAsync();
    }
}

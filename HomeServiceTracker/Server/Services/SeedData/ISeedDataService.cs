namespace HomeServiceTracker.Server.Services.SeedData
{
    public interface ISeedDataService
    {
        Task<bool> SeedHomeInfoAsync();
        Task<bool> SeedServiceItemsAsync();
        Task<bool> SeedScheduledServicesAsync();
        Task<bool> SeedServiceProviderInfoAsync();
    }
}

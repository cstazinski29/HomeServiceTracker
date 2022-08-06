using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Server.Services.HomeInfo;
using HomeServiceTracker.Server.Services.ScheduledService;
using HomeServiceTracker.Server.Services.ServiceItem;
using HomeServiceTracker.Server.Services.ServiceProviderInfo;
using HomeServiceTracker.Shared.Models.HomeInfo;

namespace HomeServiceTracker.Server.Services.SeedData
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHomeInfoService _homeInfo;
        private readonly IServiceItemService _serviceItem;
        private readonly IScheduledServiceService _scheduledService;
        private readonly IServiceProviderInfoService _serviceProviderInfoService;

        public SeedDataService(ApplicationDbContext context, IHomeInfoService homeInfo, IServiceItemService serviceItem, IScheduledServiceService scheduledService, IServiceProviderInfoService serviceProviderInfoService)
        {
            _context = context;
            _homeInfo = homeInfo;
            _serviceItem = serviceItem;
            _scheduledService = scheduledService;
            _serviceProviderInfoService = serviceProviderInfoService;
        }

        public async Task<bool> SeedHomeInfoAsync()
        {
            int count = _context.HomeInfo.Count();
            if (count == 0)
            {
                var firstHome = new HomeInfoCreate()
                {
                    HomeName = "Home Stark",
                    BuildYear = 1880,
                    SquareFootage = 2700,
                    Beds = 5,
                    Baths = 2
                };

                var secondHome = new HomeInfoCreate()
                {
                    HomeName = "Home Lannister",
                    BuildYear = 1910,
                    SquareFootage = 2000,
                    Beds = 3,
                    Baths = 3
                };

                var thirdHome = new HomeInfoCreate()
                {
                    HomeName = "Home Targaryen",
                    BuildYear = 2004,
                    SquareFootage = 1700,
                    Beds = 3,
                    Baths = 2
                };

                await _homeInfo.CreateHomeInfoAsync(firstHome);
                await _homeInfo.CreateHomeInfoAsync(secondHome);
                await _homeInfo.CreateHomeInfoAsync(thirdHome);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> SeedScheduledServicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SeedServiceItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SeedServiceProviderInfoAsync()
        {
            throw new NotImplementedException();
        }
    }
}

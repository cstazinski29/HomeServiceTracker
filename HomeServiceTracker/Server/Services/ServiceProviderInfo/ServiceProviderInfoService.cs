using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Shared.Models.ServiceProviderInfo;
using Microsoft.EntityFrameworkCore;

namespace HomeServiceTracker.Server.Services.ServiceProviderInfo
{
    public class ServiceProviderInfoService : IServiceProviderInfoService
    {
        private readonly ApplicationDbContext _context;
        public ServiceProviderInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        private Guid _userId;
        public void SetUserId(Guid userId) => _userId = userId;

        public async Task<bool> CreateServiceProviderInfoAsync(ServiceProviderInfoCreate model)
        {
            if (model == null)
                return false;
            var serviceProviderInfoEntity = new HomeServiceTracker.Server.Models.ServiceProviderInfo
            {
                ServiceProviderName = model.ServiceProviderName,
                OwnerId = _userId
            };
            _context.ServiceProviders.Add(serviceProviderInfoEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ServiceProviderInfoListItem>> GetAllServiceProviderInfosAsync()
        {
            var serviceProviderInfoQuery = _context.ServiceProviders.Where(o => o.OwnerId == _userId)
                .Select(entity => new ServiceProviderInfoListItem
            {
                Id = entity.Id,
                ServiceProviderName = entity.ServiceProviderName,
                AverageServiceProviderRating = entity.AverageServiceProviderRating
            });
            return await serviceProviderInfoQuery.ToListAsync();
        }

        public async Task<ServiceProviderInfoDetail> GetServiceProviderInfoByIdAsync(int serviceProviderInfoId)
        {
            var serviceProviderInfoEntity = await _context.ServiceProviders.FirstOrDefaultAsync(s => s.Id == serviceProviderInfoId && s.OwnerId == _userId);

            if (serviceProviderInfoEntity is null)
                return null;
            var detail = new ServiceProviderInfoDetail
            {
                Id = serviceProviderInfoEntity.Id,
                ServiceProviderName = serviceProviderInfoEntity.ServiceProviderName,
                NumberOfServices = serviceProviderInfoEntity.NumberOfServices,
                AverageServiceProviderRating = serviceProviderInfoEntity.AverageServiceProviderRating
            };

            return detail;
        }

        public async Task<bool> UpdateServiceProviderInfoAsync(ServiceProviderInfoEdit model)
        {
            if (model == null) return false;
            var entity = await _context.ServiceProviders.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.ServiceProviderName= model.ServiceProviderName;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteServiceProviderInfoAsync(int serviceProviderInfoId)
        {
            var entity = await _context.ServiceProviders.FindAsync(serviceProviderInfoId);

            if (entity?.OwnerId != _userId) return false;

            _context.ServiceProviders.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> SeedServiceProviderInfoAsync()
        {
            int count = _context.ServiceProviders.Where(o => o.OwnerId == _userId).Count();
            if (count == 0)
            {
                var firstServiceProvider = new ServiceProviderInfoCreate()
                {
                    ServiceProviderName = "Handyman Hank"
                };

                var secondServiceProvider = new ServiceProviderInfoCreate()
                {
                    ServiceProviderName = "Linda's Lawn Care"
                };

                var thirdServiceProvider = new ServiceProviderInfoCreate()
                {
                    ServiceProviderName = "Bob the Builder"
                };

                await CreateServiceProviderInfoAsync(firstServiceProvider);
                await CreateServiceProviderInfoAsync(secondServiceProvider);
                await CreateServiceProviderInfoAsync(thirdServiceProvider);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

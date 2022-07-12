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

        public async Task<bool> CreateServiceProviderInfoAsync(ServiceProviderInfoCreate model)
        {
            if (model == null)
                return false;
            var serviceProviderInfoEntity = new HomeServiceTracker.Server.Models.ServiceProviderInfo
            {
                ServiceProviderName = model.ServiceProviderName
            };
            _context.ServiceProviders.Add(serviceProviderInfoEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ServiceProviderInfoListItem>> GetAllServiceProviderInfosAsync()
        {
            var serviceProviderInfoQuery = _context.ServiceProviders.Select(entity => new ServiceProviderInfoListItem
            {
                Id = entity.Id,
                ServiceProviderName = entity.ServiceProviderName,
                AverageServiceProviderRating = entity.AverageServiceProviderRating
            });
            return await serviceProviderInfoQuery.ToListAsync();
        }

        public async Task<ServiceProviderInfoDetail> GetServiceProviderInfoByIdAsync(int serviceProviderInfoId)
        {
            var serviceProviderInfoEntity = await _context.ServiceProviders.FirstOrDefaultAsync(s => s.Id == serviceProviderInfoId);

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

        public Task<bool> UpdateServiceProviderInfoAsync(ServiceProviderInfoEdit model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteServiceProviderInfoAsync(int serviceProviderInfoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteServiceProviderInfoAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

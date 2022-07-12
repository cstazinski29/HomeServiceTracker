using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Shared.Models.ServiceItem;
using Microsoft.EntityFrameworkCore;

namespace HomeServiceTracker.Server.Services.ServiceItem
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly ApplicationDbContext _context;
        public ServiceItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateServiceItemAsync(ServiceItemCreate model)
        {
            if (model == null)
                return false;
            var serviceItemEntity = new HomeServiceTracker.Server.Models.ServiceItem
            {
                ServiceName = model.ServiceName,
                ServiceDescription = model.ServiceDescription,
                ServiceFrequency = model.ServiceFrequency
            };

            _context.ServiceItems.Add(serviceItemEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ServiceItemListItem>> GetAllServiceItemsAsync()
        {
            var serviceItemQuery = _context.ServiceItems.Select(entity => new ServiceItemListItem
            {
                Id = entity.Id,
                ServiceName = entity.ServiceName,
                ServiceFrequency = entity.ServiceFrequency
            });
            return await serviceItemQuery.ToListAsync();
        }

        public async Task<ServiceItemDetail> GetServiceItemByIdAsync(int serviceItemId)
        {
            var serviceItemEntity = await _context.ServiceItems.FirstOrDefaultAsync(s => s.Id == serviceItemId);

            if (serviceItemEntity is null)
                return null;
            var detail = new ServiceItemDetail
            {
                Id = serviceItemEntity.Id,
                ServiceName = serviceItemEntity.ServiceName,
                ServiceDescription = serviceItemEntity.ServiceDescription,
                ServiceFrequency = serviceItemEntity.ServiceFrequency
            };

            return detail;
        }

        public Task<bool> UpdateServiceItemAsync(ServiceItemEdit model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteServiceItemAsync(int serviceItemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteServiceItemAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

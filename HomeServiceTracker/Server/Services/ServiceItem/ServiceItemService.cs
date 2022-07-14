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

        private Guid _userId;
        public void SetUserId(Guid userId) => _userId = userId;

        public async Task<bool> CreateServiceItemAsync(ServiceItemCreate model)
        {
            if (model == null)
                return false;
            var serviceItemEntity = new HomeServiceTracker.Server.Models.ServiceItem
            {
                ServiceName = model.ServiceName,
                ServiceDescription = model.ServiceDescription,
                ServiceFrequency = model.ServiceFrequency,
                OwnerId = _userId
            };

            _context.ServiceItems.Add(serviceItemEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ServiceItemListItem>> GetAllServiceItemsAsync()
        {
            var serviceItemQuery = _context.ServiceItems.Where(o => o.OwnerId == _userId)
                .Select(entity => new ServiceItemListItem
            {
                Id = entity.Id,
                ServiceName = entity.ServiceName,
                ServiceFrequency = entity.ServiceFrequency
            });
            return await serviceItemQuery.ToListAsync();
        }


        public async Task<ServiceItemDetail> GetServiceItemByIdAsync(int serviceItemId)
        {
            var serviceItemEntity = await _context.ServiceItems.FirstOrDefaultAsync(s => s.Id == serviceItemId && s.OwnerId == _userId);

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

        public async Task<bool> UpdateServiceItemAsync(ServiceItemEdit model)
        {
            if (model == null) return false;
            var entity = await _context.ServiceItems.FindAsync(model.Id);

            // NEED TO ASSIGN A PERMISSION TO EDIT A SERVICE ITEM
            if (entity?.OwnerId != _userId) return false;

            entity.ServiceName = model.ServiceName;
            entity.ServiceDescription = model.ServiceDescription;
            entity.ServiceFrequency = model.ServiceFrequency;

            return await _context.SaveChangesAsync() == 1;
        }
        public async Task<bool> DeleteServiceItemAsync(int serviceItemId)
        {
            var entity = await _context.ServiceItems.FindAsync(serviceItemId);

            // NEED TO ASSIGN A PERMISSION TO DELETE A SERVICE ITEM
            if (entity?.OwnerId != _userId) return false;

            _context.ServiceItems.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

    }
}

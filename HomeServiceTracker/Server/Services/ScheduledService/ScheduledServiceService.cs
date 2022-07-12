using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Shared.Models.ScheduledService;
using Microsoft.EntityFrameworkCore;

namespace HomeServiceTracker.Server.Services.ScheduledService
{
    public class ScheduledServiceService : IScheduledServiceService
    {
        private readonly ApplicationDbContext _context;
        public ScheduledServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateScheduledServiceAsync(ScheduledServiceCreate model)
        {
            if (model == null)
                return false;

            var scheduledServiceEntity = new HomeServiceTracker.Server.Models.ScheduledService
            {
                // Pretty sure I need to cut this down & make some of the fields auto-populate based on the user & other references (such as latest date)
                ServiceItemId = model.Id,
                HomeId = model.HomeId,
                LastServiceDate = model.LastServiceDate,
                NextServiceDate = model.NextServiceDate,
                ScheduledServiceDate = model.ScheduledServiceDate,
                ServiceCompleted = model.ServiceCompleted,
                ServiceProviderId = model.ServiceProviderId,
                ServiceCost = model.ServiceCost,
                ServiceRating = model.ServiceRating
            };
            _context.ScheduledServices.Add(scheduledServiceEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ScheduledServiceListItem>> GetAllScheduledServicesAsync()
        {
            // need to add a user reference to scheduled services so that we can only pull those relevant to the user
            var scheduledServiceQuery = _context.ScheduledServices.Select(entity => new ScheduledServiceListItem
            {
                Id = entity.Id,
                ServiceItemId = entity.ServiceItemId,
                LastServiceDate = entity.LastServiceDate,
                NextServiceDate = entity.NextServiceDate,
                ScheduledServiceDate = entity.ScheduledServiceDate
            });

            return await scheduledServiceQuery.ToListAsync();
        }

        public async Task<ScheduledServiceDetail> GetScheduledServiceByIdAsync(int scheduledServiceId)
        {
            var scheduledServiceEntity = await _context.ScheduledServices.FirstOrDefaultAsync(s => s.Id == scheduledServiceId);

            if (scheduledServiceEntity is null)
                return null;
            var detail = new ScheduledServiceDetail
            {
                Id = scheduledServiceEntity.Id,
                ServiceItemId = scheduledServiceEntity.ServiceItemId,
                HomeId = scheduledServiceEntity.HomeId,
                LastServiceDate = scheduledServiceEntity.LastServiceDate,
                NextServiceDate = scheduledServiceEntity.NextServiceDate,
                ScheduledServiceDate = scheduledServiceEntity.ScheduledServiceDate,
                ServiceCompleted = scheduledServiceEntity.ServiceCompleted,
                ServiceProviderId = scheduledServiceEntity.ServiceProviderId,
                ServiceCost = scheduledServiceEntity.ServiceCost,
                ServiceRating = scheduledServiceEntity.ServiceRating
            };

            return detail;
        }

        public Task<bool> UpdateScheduledServiceAsync(ScheduledServiceEdit model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteScheduledServiceAsync(int scheduledServiceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteScheduledServiceAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

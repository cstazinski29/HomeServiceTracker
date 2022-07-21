using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Shared.Models.ScheduledService;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace HomeServiceTracker.Server.Services.ScheduledService
{
    public class ScheduledServiceService : IScheduledServiceService
    {
        private readonly ApplicationDbContext _context;
        public ScheduledServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        private Guid _userId;
        public void SetUserId(Guid userId) => _userId = userId;

        public async Task<bool> CreateScheduledServiceAsync(ScheduledServiceCreate model)
        {
            if (model == null)
                return false;

            //IEnumerable<SelectListItem> serviceItemOptions = await _context.ServiceItems.Select(s => new SelectListItem()
            //{
            //    Text = s.ServiceName,
            //    Value = s.Id.ToString()
            //}).ToListAsync();

            var scheduledServiceEntity = new HomeServiceTracker.Server.Models.ScheduledService
            {
                // Pretty sure I need to cut this down & make some of the fields auto-populate based on the user & other references (such as latest date)
                ServiceItemId = model.ServiceItemId,
                HomeId = model.HomeId,
                LastServiceDate = model.LastServiceDate,
                NextServiceDate = model.NextServiceDate,
                ScheduledServiceDate = model.ScheduledServiceDate,
                ServiceCompleted = model.ServiceCompleted,
                ServiceProviderId = model.ServiceProviderId,
                ServiceCost = model.ServiceCost,
                ServiceRating = model.ServiceRating,
                OwnerId = _userId
            };

            _context.ScheduledServices.Add(scheduledServiceEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ScheduledServiceListItem>> GetAllScheduledServicesAsync()
        {
            // need to add a user reference to scheduled services so that we can only pull those relevant to the user
            var scheduledServiceQuery = _context.ScheduledServices
                .Include(s => s.ServiceItem)
                .Where(o => o.OwnerId == _userId)
                .Select(entity => new ScheduledServiceListItem
            {
                Id = entity.Id,
                ServiceItemId = entity.ServiceItemId,
                LastServiceDate = entity.LastServiceDate,
                NextServiceDate = entity.NextServiceDate,
                ScheduledServiceDate = entity.ScheduledServiceDate,
                ServiceName = entity.ServiceItem.ServiceName
            });

            return await scheduledServiceQuery.ToListAsync();
        }

        public async Task<ScheduledServiceDetail> GetScheduledServiceByIdAsync(int scheduledServiceId)
        {
            var scheduledServiceEntity = await _context.ScheduledServices.FirstOrDefaultAsync(s => s.Id == scheduledServiceId && s.OwnerId == _userId);

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

        public async Task<bool> UpdateScheduledServiceAsync(ScheduledServiceEdit model)
        {
            if (model == null) return false;
            var entity = await _context.ScheduledServices.FindAsync(model.Id);

            // NEED TO ASSIGN A PERMISSION TO EDIT A SERVICE
            if (entity?.OwnerId != _userId) return false;

            entity.ServiceItemId = model.ServiceItemId;
            entity.HomeId = model.HomeId;
            entity.ScheduledServiceDate = model.ScheduledServiceDate;
            entity.ServiceCompleted = model.ServiceCompleted;
            entity.ServiceProviderId = model.ServiceProviderId;
            entity.ServiceCost = model.ServiceCost;
            entity.ServiceRating = model.ServiceRating;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteScheduledServiceAsync(int scheduledServiceId)
        {
            var entity = await _context.ScheduledServices.FindAsync(scheduledServiceId);

            // NEED TO ASSIGN A PERMISSION TO EDIT A SERVICE ITEM
            if (entity?.OwnerId != _userId) return false;

            _context.ScheduledServices.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

    }
}

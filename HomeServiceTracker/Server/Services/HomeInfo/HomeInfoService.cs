using HomeServiceTracker.Server.Data;
using HomeServiceTracker.Shared.Models.HomeInfo;
using Microsoft.EntityFrameworkCore;

namespace HomeServiceTracker.Server.Services.HomeInfo
{
    public class HomeInfoService : IHomeInfoService
    {
        private readonly ApplicationDbContext _context;
        public HomeInfoService(ApplicationDbContext context)
        {
            _context = context;
        }
        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
        public async Task<bool> CreateHomeInfoAsync(HomeInfoCreate model)
        {
            var homeInfoEntity = new HomeServiceTracker.Server.Models.HomeInfo
            {
                HomeName = model.HomeName,
                BuildYear = model.BuildYear,
                SquareFootage = model.SquareFootage,
                Beds = model.Beds,
                Baths = model.Baths
                // need to set primaryHomeownerId to be current userId
                // may need to add CreatedUtc field here if want to track when home added
            };

            _context.HomeInfo.Add(homeInfoEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<IEnumerable<HomeInfoListItem>> GetAllHomeInfoAsync()
        {
            var homeInfoQuery = _context.HomeInfo.Where(h => h.PrimaryHomeownerId == _userId)
                .Select(h => new HomeInfoListItem
                {
                    Id = h.Id,
                    HomeName = h.HomeName
                });
            return homeInfoQuery.ToList();
            // module has the above return as an "await" and "ToListAsync"
        }

        public async Task<HomeInfoDetail> GetHomeInfoByIdAsync(int homeId)
        {
            var homeEntity = await _context.HomeInfo.FirstOrDefaultAsync(h => h.Id == homeId && h.PrimaryHomeownerId == _userId);

            if (homeEntity is null)
                return null;
            var detail = new HomeInfoDetail
            {
                Id=homeEntity.Id,
                HomeName=homeEntity.HomeName,
                BuildYear=homeEntity.BuildYear,
                SquareFootage = homeEntity.SquareFootage,
                Beds = homeEntity.Beds,
                Baths = homeEntity.Baths,
            };

            return detail;
        }

        public Task<bool> DeleteHomeInfoAsync(int homeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHomeInfoAsync(string userId)
        {
            throw new NotImplementedException();
        }


        public void SetUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateHomeInfoAsync(HomeInfoEdit model)
        {
            throw new NotImplementedException();
        }
    }
}

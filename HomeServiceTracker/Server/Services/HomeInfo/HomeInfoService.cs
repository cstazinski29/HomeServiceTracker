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

        private Guid _userId;
        public void SetUserId(Guid userId) => _userId = userId;
        public async Task<bool> CreateHomeInfoAsync(HomeInfoCreate model)
        {
            if (model == null)
                return false;

            var homeInfoEntity = new HomeServiceTracker.Server.Models.HomeInfo
            {
                HomeName = model.HomeName,
                BuildYear = model.BuildYear,
                SquareFootage = model.SquareFootage,
                Beds = model.Beds,
                Baths = model.Baths,
                OwnerId = _userId,
                //CreatedUtc = DateTime.Now
            };

            _context.HomeInfo.Add(homeInfoEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<IEnumerable<HomeInfoListItem>> GetAllHomeInfoAsync()
        {
            var homeInfoQuery = _context.HomeInfo.Where(h => h.OwnerId == _userId)
                .Select(h => new HomeInfoListItem
                {
                    Id = h.Id,
                    HomeName = h.HomeName,
                    BuildYear = h.BuildYear
                });
            return await homeInfoQuery.ToListAsync();
        }

        public async Task<HomeInfoDetail> GetHomeInfoByIdAsync(int homeId)
        {
            var homeEntity = await _context.HomeInfo.FirstOrDefaultAsync(h => h.Id == homeId && h.OwnerId == _userId);

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

        public async Task<bool> UpdateHomeInfoAsync(HomeInfoEdit model)
        {
            if (model == null) return false;
            var entity = await _context.HomeInfo.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.HomeName = model.HomeName;
            entity.BuildYear = model.BuildYear;
            entity.SquareFootage = model.SquareFootage;
            entity.Beds = model.Beds;
            entity.Baths = model.Baths;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteHomeInfoAsync(int homeId)
        {
            var entity = await _context.HomeInfo.FindAsync(homeId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.HomeInfo.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> SeedHomeInfoAsync()
        {
            int count = _context.HomeInfo.Where(h => h.OwnerId == _userId).Count();
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

                await CreateHomeInfoAsync(firstHome);
                await CreateHomeInfoAsync(secondHome);
                await CreateHomeInfoAsync(thirdHome);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

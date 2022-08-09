using HomeServiceTracker.Server.Services.HomeInfo;
using HomeServiceTracker.Server.Services.SeedData;
using HomeServiceTracker.Shared.Models.HomeInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // public class HomeInfoController : ControllerBase
    public class HomeInfoController : Controller
    {
        private readonly IHomeInfoService _homeInfoService;
        //private readonly ISeedDataService _seed;
        public HomeInfoController(IHomeInfoService homeInfoService)
        {
            _homeInfoService = homeInfoService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null)
                return default;
            return Guid.Parse(userIdClaim);
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _homeInfoService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<HomeInfoListItem>> Index()
        {
            //await _seed.SeedHomeInfoAsync();

            if (!SetUserIdInService()) return new List<HomeInfoListItem>();
            var homeInfo = await _homeInfoService.GetAllHomeInfoAsync();
            return homeInfo.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeInfoById(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var homeInfo = await _homeInfoService.GetHomeInfoByIdAsync(id);
            if (homeInfo == null)
                return NotFound();

            return Ok(homeInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeInfoCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _homeInfoService.CreateHomeInfoAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, HomeInfoEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();
            
            bool wasSuccessful = await _homeInfoService.UpdateHomeInfoAsync(model);
            if (wasSuccessful) return Ok();
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            
            var home = await _homeInfoService.GetHomeInfoByIdAsync(id);
            if (home == null) return NotFound();
            
            bool wasSuccessful = await _homeInfoService.DeleteHomeInfoAsync(id);
            if (!wasSuccessful) return BadRequest();
            
            return Ok();
        }
    }
}

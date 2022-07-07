using HomeServiceTracker.Server.Services.HomeInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeInfoController : ControllerBase
    {
        private readonly IHomeInfoService _homeInfoService;
        public HomeInfoController(IHomeInfoService homeInfoService)
        {
            _homeInfoService = homeInfoService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null)
                return null;
            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null)
                return false;
            _homeInfoService.SetUserId(userId);
            return true;
        }
    }
}

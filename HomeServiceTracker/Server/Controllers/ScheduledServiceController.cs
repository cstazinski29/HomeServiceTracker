using HomeServiceTracker.Server.Services.ScheduledService;
using HomeServiceTracker.Shared.Models.ScheduledService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledServiceController : Controller
    {
        private readonly IScheduledServiceService _scheduledServiceService;
        public ScheduledServiceController(IScheduledServiceService scheduledServiceService)
        {
            _scheduledServiceService = scheduledServiceService;
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
            _scheduledServiceService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<ScheduledServiceListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<ScheduledServiceListItem>();
            var scheduledServices = await _scheduledServiceService.GetAllScheduledServicesAsync();
            return scheduledServices.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ScheduledService(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var scheduledService = await _scheduledServiceService.GetScheduledServiceByIdAsync(id);
            if (scheduledService == null)
                return NotFound();
            return Ok(scheduledService);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduledServiceCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _scheduledServiceService.CreateScheduledServiceAsync(model);
            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ScheduledServiceEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();

            bool wasSuccessful = await _scheduledServiceService.UpdateScheduledServiceAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var service = await _scheduledServiceService.GetScheduledServiceByIdAsync(id);
            if (service == null) return NotFound();

            bool wasSuccessful = await _scheduledServiceService.DeleteScheduledServiceAsync(id);
            if (!wasSuccessful) return BadRequest();

            return Ok();
        }

        [HttpGet("home/{homeId}")]
        public async Task<List<ScheduledServiceListItem>> ScheduledServiceByHome(int homeId)
        {
            if (!SetUserIdInService()) return new List<ScheduledServiceListItem>();
            var scheduledServices = await _scheduledServiceService.GetAllScheduledServiceByHomeIdAsync(homeId);
            return scheduledServices.ToList();
        }
    }
}

using HomeServiceTracker.Server.Services.ScheduledService;
using HomeServiceTracker.Shared.Models.ScheduledService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class ScheduledServiceController : ControllerBase
    public class ScheduledServiceController : Controller
    {
        private readonly IScheduledServiceService _scheduledServiceService;
        public ScheduledServiceController(IScheduledServiceService scheduledServiceService)
        {
            _scheduledServiceService = scheduledServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var scheduledServices = await _scheduledServiceService.GetAllScheduledServicesAsync();
            return Ok(scheduledServices);
        }

        // NEED TO UPDATE SCHEDULED SERVICE SERVICE WITH THE GET BY ID METHOD FIRST
        [HttpGet("{id}")]
        public async Task<IActionResult> ScheduledService(int id)
        {
            var scheduledService = await _scheduledServiceService.GetScheduledServiceByIdAsync(id);
            if (scheduledService == null)
                return NotFound();
            return Ok(scheduledService);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScheduledServiceCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            bool wasSuccessful = await _scheduledServiceService.CreateScheduledServiceAsync(model);
            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ScheduledServiceEdit model)
        {
            // I think I'll need to explore this permissioning further
            //if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();

            bool wasSuccessful = await _scheduledServiceService.UpdateScheduledServiceAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // I think I'll need to explore this permissioning further
            //if (!SetUserIdInService()) return Unauthorized();

            var service = await _scheduledServiceService.GetScheduledServiceByIdAsync(id);
            if (service == null) return NotFound();

            bool wasSuccessful = await _scheduledServiceService.DeleteScheduledServiceAsync(id);
            if (!wasSuccessful) return BadRequest();

            return Ok();
        }
    }
}

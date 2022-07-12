using HomeServiceTracker.Server.Services.ServiceItem;
using HomeServiceTracker.Shared.Models.ServiceItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class ServiceItemController : ControllerBase
    public class ServiceItemController : Controller
    {
        private readonly IServiceItemService _serviceItemService;
        public ServiceItemController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceItems = await _serviceItemService.GetAllServiceItemsAsync();
            return Ok(serviceItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ServiceItem(int id)
        {
            var serviceItem = await _serviceItemService.GetServiceItemByIdAsync(id);
            if (serviceItem == null)
                return NotFound();
            return Ok(serviceItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceItemCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            bool wasSuccessful = await _serviceItemService.CreateServiceItemAsync(model);

            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }
    }
}

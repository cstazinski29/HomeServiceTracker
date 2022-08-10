using HomeServiceTracker.Server.Services.ServiceItem;
using HomeServiceTracker.Shared.Models.ServiceItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            _serviceItemService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<ServiceItemListItem>> Index()
        {
            //var serviceItems = await _serviceItemService.GetAllServiceItemsAsync();
            //return Ok(serviceItems);

            if (!SetUserIdInService()) return new List<ServiceItemListItem>();

            await _serviceItemService.SeedServiceItemsAsync();
            var serviceItems = await _serviceItemService.GetAllServiceItemsAsync();
            return serviceItems.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ServiceItem(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var serviceItem = await _serviceItemService.GetServiceItemByIdAsync(id);
            if (serviceItem == null)
                return NotFound();
            return Ok(serviceItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceItemCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();
            bool wasSuccessful = await _serviceItemService.CreateServiceItemAsync(model);

            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ServiceItemEdit model)
        {
            // I think I'll need to explore this permissioning further
            // E.G. ADD A CHECK FOR WHETHER USER HAS PERMISSION TO EDIT (OWNERID == USERID)

            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();

            bool wasSuccessful = await _serviceItemService.UpdateServiceItemAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // I think I'll need to explore this permissioning further
            // E.G. ADD A CHECK FOR WHETHER USER HAS PERMISSION TO DELETE (OWNERID == USERID)
                // there is a check for ownerid == userId in the GetServiceItemByIdAsync method in the service

            if (!SetUserIdInService()) return Unauthorized();

            var service = await _serviceItemService.GetServiceItemByIdAsync(id);
            if (service == null) return NotFound();

            bool wasSuccessful = await _serviceItemService.DeleteServiceItemAsync(id);
            if (!wasSuccessful) return BadRequest();

            return Ok();
        }

    }
}
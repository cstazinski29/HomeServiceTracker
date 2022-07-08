using HomeServiceTracker.Server.Services.ServiceProviderInfo;
using HomeServiceTracker.Shared.Models.ServiceProviderInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class ServiceProviderInfoController : ControllerBase
    public class ServiceProviderInfoController : Controller
    {
        private readonly IServiceProviderInfoService _serviceProviderInfoService;
        public ServiceProviderInfoController(IServiceProviderInfoService serviceProviderInfoService)
        {
            _serviceProviderInfoService = serviceProviderInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceProviders = await _serviceProviderInfoService.GetAllServiceProviderInfosAsync();
            return Ok(serviceProviders);
        }

        // NEED TO UPDATE SERVICE PROVIDER INFO SERVICE WITH THE GET BY ID METHOD FIRST
        //[HttpGet("{id}")]
        //public async Task<IActionResult> ServiceProviderInfo(int id)
        //{
        //    var serviceProviderInfo = await _serviceProviderInfoService.GetServiceProviderInfoByIdAsync(id);
        //    if (serviceProviderInfo == null)
        //        return NotFound();
        //    return Ok(serviceProviderInfo);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ServiceProviderInfoCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            bool wasSuccessful = await _serviceProviderInfoService.CreateServiceProviderInfoAsync(model);
            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }

    }
}

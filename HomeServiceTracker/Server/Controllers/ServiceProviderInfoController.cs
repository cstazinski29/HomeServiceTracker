﻿using HomeServiceTracker.Server.Services.ServiceProviderInfo;
using HomeServiceTracker.Shared.Models.ServiceProviderInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeServiceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderInfoController : Controller
    {
        private readonly IServiceProviderInfoService _serviceProviderInfoService;
        public ServiceProviderInfoController(IServiceProviderInfoService serviceProviderInfoService)
        {
            _serviceProviderInfoService = serviceProviderInfoService;
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
            _serviceProviderInfoService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<ServiceProviderInfoListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<ServiceProviderInfoListItem>();

            await _serviceProviderInfoService.SeedServiceProviderInfoAsync();
            var serviceProviders = await _serviceProviderInfoService.GetAllServiceProviderInfosAsync();
            return serviceProviders.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ServiceProviderInfo(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var serviceProviderInfo = await _serviceProviderInfoService.GetServiceProviderInfoByIdAsync(id);
            if (serviceProviderInfo == null)
                return NotFound();
            return Ok(serviceProviderInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceProviderInfoCreate model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _serviceProviderInfoService.CreateServiceProviderInfoAsync(model);
            if (wasSuccessful)
                return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ServiceProviderInfoEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();

            bool wasSuccessful = await _serviceProviderInfoService.UpdateServiceProviderInfoAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var service = await _serviceProviderInfoService.GetServiceProviderInfoByIdAsync(id);
            if (service == null) return NotFound();

            bool wasSuccessful = await _serviceProviderInfoService.DeleteServiceProviderInfoAsync(id);
            if (!wasSuccessful) return BadRequest();

            return Ok();
        }

    }
}

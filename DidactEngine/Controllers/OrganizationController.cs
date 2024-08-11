using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DidactEngine.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<MaintenanceController> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public OrganizationController(ILogger<MaintenanceController> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        /// <summary>
        /// Returns organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet("/organizations")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrganizationsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get specific organization by name     
        /// </summary>
        /// <returns></returns>
        [HttpGet("/organization/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        public async Task<IActionResult> GetOrganizationByNameAsync()
        {
            throw new NotImplementedException();
        }
    }
}

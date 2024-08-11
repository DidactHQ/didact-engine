using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DidactEngine.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class FlowRunController : ControllerBase
    {
        private readonly ILogger<MaintenanceController> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public FlowRunController(ILogger<MaintenanceController> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        /// <summary>
        /// Returns didact flow runs
        /// </summary>
        /// <returns></returns>
        [HttpGet("/flows/runs")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlowRunsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get specific flow run by name        
        /// </summary>
        /// <returns></returns>
        [HttpGet("/flows/run/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        public async Task<IActionResult> GetFlowRunByNameAsync()
        {
            throw new NotImplementedException();
        }
    }
}

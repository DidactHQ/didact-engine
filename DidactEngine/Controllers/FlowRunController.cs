using DidactCore.Entities;
using DidactCore.Flows;
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
        private readonly IFlowRunRepository _flowRunRepository;

        public FlowRunController(ILogger<MaintenanceController> logger, IHostApplicationLifetime hostApplicationLifetime, IFlowRunRepository flowRunRepository)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _flowRunRepository = flowRunRepository;
        }

        [HttpGet("/flows/run/{flowRunId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> GetFlowRunAsync(long flowRunId)
        {
            var flow = await _flowRunRepository.GetFlowRunAsync(flowRunId);
            return Ok(flow);
        }

        [HttpGet("/flows/run/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> GetFlowRunByNameAsync(string name)
        {
            var flow = await _flowRunRepository.GetFlowRunByNameAsync(name);
            return Ok(flow);
        }

        [HttpGet("/flows/run/description/{description}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> GetFlowRunByDescriptionAsync(string description)
        {
            var flow = await _flowRunRepository.GetFlowRunByDescriptionAsync(description);
            return Ok(flow);
        }

        [HttpPost("/flows/run/enqueue")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> CreateAndEnqueueFlowRunAsync([FromBody] FlowRun flowRun)
        {
            var flow = await _flowRunRepository.CreateAndEnqueueFlowRunAsync(flowRun);
            return Ok(flow);
        }

        [HttpPost("/flows/run/execute")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> CreateAndExecuteFlowRunAsync([FromBody] FlowRun flowRun)
        {
            var flow = await _flowRunRepository.CreateAndExecuteFlowRunAsync(flowRun);
            return Ok(flow);
        }

        [HttpPut("/flows/run/{flowRunId}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(FlowRun))]
        public async Task<IActionResult> UpdateFlowRunAsync(long flowRunId, [FromBody] FlowRun flowRun)
        {
            var flow = await _flowRunRepository.UpdateFlowRunAsync(flowRunId, flowRun);
            return Ok(flow);
        }

        [HttpDelete("/flows/run/{flowRunId}")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFlowRunAsync(long flowRunId)
        {
            await _flowRunRepository.DeleteFlowRunAsync(flowRunId);
            return Ok();
        }
    }
}

using DidactCore;
using DidactCore.Blocks;
using DidactCore.Constants;
using DidactCore.DependencyInjection;
using DidactCore.Entities;
using DidactCore.Flows;
using DidactEngine.Services.Contexts;
using DidactEngine.Services.Contexts.Configurations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DidactEngine.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class FlowController : ControllerBase
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IFlowExecutor _flowExecutor;
        private readonly IFlowLogger _flowLogger;
        private readonly IFlowConfigurator _flowConfigurator;
        private readonly IDidactDependencyInjector _didactDependencyInjector;
        private readonly IFlowRepository _flowRepository;

        public FlowController(ILogger<MaintenanceController> logger, IHostApplicationLifetime hostApplicationLifetime, IFlowExecutor flowExecutor, IFlowLogger flowLogger, IFlowConfigurator flowConfigurator, IDidactDependencyInjector didactDependencyInjector, IFlowRepository flowRepository)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _flowExecutor = flowExecutor;
            _flowLogger = flowLogger;
            _flowConfigurator = flowConfigurator;
            _didactDependencyInjector = didactDependencyInjector;
            _flowRepository = flowRepository;
        }

        /// <summary>
        /// Returns didact flows
        /// </summary>
        /// <returns></returns>
        [HttpGet("/flows")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlowsAsync()
        {
            var flows = await _flowRepository.GetAllFlowsFromStorageAsync();
            return Ok(flows);
        }

        /// <summary>
        /// Get specific flow by name        
        /// </summary>
        /// <returns></returns>
        [HttpGet("/flow/{name}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(string))]
        public async Task<IActionResult> GetFlowByNameAsync()
        {
            var flow = await _flowRepository.GetFlowByNameAsync("TestFlow");
            return Ok(flow);
        }

        /// <summary>
        /// Create didact flow
        /// </summary>
        /// <returns></returns>
        [HttpPost("/flows")]
        [SwaggerResponse(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> CreateFlowAsync()
        {
            SomeFlow someFlow = new SomeFlow(_flowLogger, _flowConfigurator, _didactDependencyInjector);
            await _flowRepository.SaveConfigurationsAsync(_flowConfigurator);

            return Accepted();

            // await someFlow.ExecuteAsync("json test string running inside action task block in a flow run");

            

            //Flow flow = new Flow();
            //flow.Name = "TestFlow";
            //flow.Description = "Test Flow Description";
            //flow.AssemblyName = "DidactCore";
            //flow.TypeName = "DidactCore.Flows.TestFlow";
            //flow.ConcurrencyLimit = 1;
            //flow.Created = DateTime.Now;
            //flow.CreatedBy = "TestUser";
            //flow.LastUpdated = DateTime.Now;
            //flow.LastUpdatedBy = "TestUser";
            //flow.Active = true;
            //flow.RowVersion = new byte[0];
            //flow.OrganizationId = 1;

            //await _flowExecutor.ConfigureFlowsAsync();

            //_flowExecutor.ExecuteFlowInstanceAsync("TestFlow");

            //        var flow = new Flow("FetchAndSaveFlow")
            //.AddHttpTaskBlock("FetchDataFromWeb", new HttpTaskBlockOptions
            //{
            //    Url = "https://example.com/data",
            //    Method = HttpMethod.Get
            //})
            //.AddTransformBlock("ProcessData", data =>
            //{
            //    // Transform or parse data if necessary
            //    return parsedData;
            //})
            //.AddSqlTaskBlock("SaveDataToDatabase", new SqlTaskBlockOptions
            //{
            //    ConnectionString = "YourConnectionString",
            //    SqlCommand = "INSERT INTO YourTable (Column1) VALUES (@Value1)",
            //    Parameters = new { Value1 = parsedData.SomeProperty }
            //});

            //        flow.Execute();

            //Flow flow = new Flow();
            //flow.Name = "TestFlow";

            //FlowRun flowRun = new FlowRun();
            //flowRun.Flow = flow;
            //flowRun.FlowId = flow.FlowId;

            //await _flowExecutor.ConfigureFlowsAsync();

            //return CreatedAtAction(nameof(GetFlowsAsync), flow);
        }
    }
}

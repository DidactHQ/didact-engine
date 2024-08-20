using DidactCore.Entities;
using DidactCore.Exceptions;
using DidactEngine.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DidactCore.Flows
{
    public class FlowRepository : IFlowRepository
    {
        private readonly DidactDbContext _context;

        public FlowRepository(DidactDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task ActivateFlowByIdAsync(long flowId)
        {
            _context.Flows.Where(f => f.FlowId == flowId).FirstOrDefault().Active = true;
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateFlowByIdAsync(long flowId)
        {
            _context.Flows.Where(f => f.FlowId == flowId).FirstOrDefault().Active = false;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Flow>> GetAllFlowsFromStorageAsync()
        {
            var flows = await _context.Flows.ToListAsync();
            return flows;
        }

        public async Task<IEnumerable<Flow>> GetAllOrganizationFlowsFromStorageAsync(int organizationId)
        {
            var flows = await _context.Flows.Where(f => f.OrganizationId == organizationId).ToListAsync();
            return flows;
        }

        public async Task<Flow> GetFlowByIdAsync(long flowId)
        {
            var flow = await _context.Flows.Where(f => f.FlowId == flowId).FirstOrDefaultAsync();        
            return flow;
        }

        public async Task<Flow> GetFlowByNameAsync(string name)
        {
            var flow = await _context.Flows.Where(f => f.Name == name).FirstOrDefaultAsync();
            return flow;
        }

        public async Task<Flow> GetFlowByTypeNameAsync(string flowTypeName)
        {
            var flow = await _context.Flows.Where(f => f.TypeName == flowTypeName).FirstOrDefaultAsync();
            return flow;
        }

        public async Task SaveConfigurationsAsync(IFlowConfigurator flowConfigurator)
        {
            // Create a new FlowConfiguration entity to save to the database
            var flow = new Flow
            {
                Name = "testflow-" + Guid.NewGuid().ToString(),
                OrganizationId = 1,
                //Description = flowConfigurator.Description,
                AssemblyName = "ASM",                
                TypeName = "type",
                ConcurrencyLimit = 1,
                Created = DateTime.Now,
                CreatedBy = "Casper",
                LastUpdated = DateTime.Now,
                LastUpdatedBy = "Casper",
                Active = true,
                //RowVersion = 
                

                
                //ver = flowConfigurator.Version,
                
                //QueueType = flowConfigurator.QueueType,
                //QueueName = flowConfigurator.QueueName,
                //Delay = flowConfigurator.Delay,
                //CronExpression = flowConfigurator.CronExpression,
                //StartDateTime = flowConfigurator.StartDateTime,
                //EndDateTime = flowConfigurator.EndDateTime
            };

            try
            {
                // Add the configuration to the database context and save changes
                _context.Flows.Add(flow);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you would normally inject a logger)
                Console.WriteLine($"An error occurred while saving the flow configurations: {ex.Message}");
                throw new SaveFlowConfigurationsException("Failed to save the flow configurations.", ex);
            }
        }
    }
}
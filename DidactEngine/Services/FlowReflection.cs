using DidactCore.Flows;

namespace DidactEngine.Services
{
    public class FlowReflection
    {
        private readonly IServiceProvider _serviceProvider;

        public FlowReflection(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IFlow CreateFlow(string flowTypeName)
        {
            // Traverse the AppDomain's assemblies to get the type.
            // Remember that .NET 5+ only has 1 AppDomain going forward, so CurrentDomain is sufficient.
            var flowType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(t => t.Name == flowTypeName).SingleOrDefault()
                ?? throw new NullReferenceException();

            // Create an instance of the type using the dependency injection system.
            // Then safe cast to an IFlow.
            var flow = ActivatorUtilities.CreateInstance(_serviceProvider, flowType) as IFlow
                ?? throw new NullReferenceException();

            return flow;
        }
    }
}

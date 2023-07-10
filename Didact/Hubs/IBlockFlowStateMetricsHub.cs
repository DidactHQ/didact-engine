namespace DidactEngine.Hubs
{
    public interface IBlockFlowStateMetricsHub
    {
        Task SendMessage(string message);
    }
}

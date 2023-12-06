namespace DidactEngine.Models.Entities
{
    public class Organization
    {
        public int OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<Flow> Flows { get; } = new List<Flow>();

        public virtual ICollection<FlowRun> FlowRuns { get; } = new List<FlowRun>();

        public virtual ICollection<HyperQueue> HyperQueues { get; } = new List<HyperQueue>();

        public virtual ICollection<FifoQueue> FifoQueues { get; } = new List<FifoQueue>();

        public virtual ICollection<HyperQueueInbound> HyperQueueInbounds { get; } = new List<HyperQueueInbound>();

        public virtual ICollection<FifoQueueInbound> FifoQueueInbounds { get; } = new List<FifoQueueInbound>();
    }
}

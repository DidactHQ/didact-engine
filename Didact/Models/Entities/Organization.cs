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

        public virtual ICollection<StandardQueue> StandardQueues { get; } = new List<StandardQueue>();

        public virtual ICollection<FifoQueue> FifoQueues { get; } = new List<FifoQueue>();

        public virtual ICollection<StandardQueueInbound> StandardQueueInbounds { get; } = new List<StandardQueueInbound>();

        public virtual ICollection<FifoQueueInbound> FifoQueueInbounds { get; } = new List<FifoQueueInbound>();
    }
}

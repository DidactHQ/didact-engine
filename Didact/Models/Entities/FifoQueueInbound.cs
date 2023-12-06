namespace DidactEngine.Models.Entities
{
    public class FifoQueueInbound
    {
        public long FifoQueueInboundId { get; set; }

        public int OrganizationId { get; set; }

        public int FifoQueueId { get; set; }

        public long FlowId { get; set; }

        public long FlowRunId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual FifoQueue FifoQueue { get; set; } = null!;

        public virtual Flow Flow { get; set; } = null!;

        public virtual FlowRun FlowRun { get; set; } = null!;
    }
}

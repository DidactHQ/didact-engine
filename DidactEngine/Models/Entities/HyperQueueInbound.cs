namespace DidactEngine.Models.Entities
{
    public class HyperQueueInbound
    {
        public long HyperQueueInboundId { get; set; }

        public int OrganizationId { get; set; }

        public int HyperQueueId { get; set; }

        public long FlowRunId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public byte[] RowVersion { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual HyperQueue HyperQueue { get; set; } = null!;

        public virtual FlowRun FlowRun { get; set; } = null!;
    }
}

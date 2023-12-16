namespace DidactEngine.Models.Entities
{
    public class FlowRun
    {
        public long FlowRunId { get; set; }

        public long FlowId { get; set; }

        public int OrganizationId { get; set; }

        public long? ParentFlowRunId { get; set; }

        public int TriggerTypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? JsonPayload { get; set; }

        public DateTime? ExecuteAfter { get; set; }

        public decimal? TimeoutSeconds { get; set; }

        public int StateId { get; set; }

        public DateTime StateLastUpdated { get; set; }

        public string StateLastUpdatedBy { get; set; } = null!;

        public DateTime? ExecutionStarted { get; set; }

        public DateTime? ExecutionEnded { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual Flow Flow { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual State State { get; set; } = null!;

        public virtual TriggerType TriggerType { get; set; } = null!;

        public virtual ICollection<BlockRun> BlockRuns { get; set; } = new List<BlockRun>();

        public virtual ICollection<HyperQueueInbound> HyperQueueInbounds { get; } = new List<HyperQueueInbound>();

        public virtual ICollection<FifoQueueInbound> FifoQueueInbounds { get; } = new List<FifoQueueInbound>();
    }
}

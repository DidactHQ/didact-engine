namespace DidactEngine.Models.Entities
{
    public class BlockRun
    {
        public long BlockRunId { get; set; }

        public long FlowRunId { get; set; }

        public int OrganizationId { get; set; }

        public string? BlockName { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? ExecutionStarted { get; set; }

        public DateTime? ExecutionEnded { get; set; }

        public int StateId { get; set; }

        public DateTime StateLastUpdated { get; set; }

        public string StateLastUpdatedBy { get; set; } = null!;

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual FlowRun FlowRun { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual State State { get; set; } = null!;
    }
}

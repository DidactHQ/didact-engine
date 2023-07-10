namespace DidactEngine.Models.Entities
{
    public class Flow
    {
        public long FlowId { get; set; }

        public int OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Version { get; set; }

        public string AssemblyName { get; set; } = null!;

        public string FullyQualifiedTypeName { get; set; } = null!;

        public int ConcurrencyLimit { get; set; }

        public int TriggerTypeId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual TriggerType TriggerType { get; set; } = null!;
    }
}

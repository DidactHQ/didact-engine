namespace DidactEngine.Models.Entities
{
    public class Engine
    {
        public int EngineId { get; set; }

        public int OrganizationId { get; set; }

        public string UniqueName { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;
    }
}

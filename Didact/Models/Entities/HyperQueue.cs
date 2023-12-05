namespace DidactEngine.Models.Entities
{
    public class HyperQueue
    {
        public int HyperQueueId { get; set; }

        public int OrganizationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; } = null!;

        public bool Active { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public virtual Organization Organization { get; set; } = null!;

        public virtual ICollection<HyperQueueInbound> HyperQueueInbounds { get; } = new List<HyperQueueInbound>();
    }
}

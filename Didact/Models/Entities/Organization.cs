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
    }
}

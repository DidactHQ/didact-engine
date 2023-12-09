using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DidactEngine.Models.Entities;

namespace DidactEngine.Services.Contexts.Configurations
{
    public partial class FifoQueueInboundConfiguration : IEntityTypeConfiguration<FifoQueueInbound>
    {
        public void Configure(EntityTypeBuilder<FifoQueueInbound> entity)
        {
            entity.ToTable(nameof(FifoQueueInbound));
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.LastUpdatedBy).HasMaxLength(255);
            entity.Property(e => e.RowVersion).IsRowVersion().IsConcurrencyToken();

            entity.HasOne(d => d.Organization)
                .WithMany(p => p.FifoQueueInbounds)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(FifoQueueInbound)}_{nameof(Organization)}");

            entity.HasOne(d => d.FlowRun)
                .WithMany(p => p.FifoQueueInbounds)
                .HasForeignKey(d => d.FlowRunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(FifoQueueInbound)}_{nameof(FlowRun)}");

            entity.HasOne(d => d.FifoQueue)
                .WithMany(p => p.FifoQueueInbounds)
                .HasForeignKey(d => d.FifoQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(FifoQueueInbound)}_{nameof(FifoQueue)}");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FifoQueueInbound> entity);
    }
}
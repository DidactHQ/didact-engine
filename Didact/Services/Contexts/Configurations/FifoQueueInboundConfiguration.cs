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
                .HasConstraintName("FK_FifoQueueInbound_Organization");

            entity.HasOne(d => d.Flow)
                .WithMany(p => p.FifoQueueInbounds)
                .HasForeignKey(d => d.FlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FifoQueueInbound_Flow");

            entity.HasOne(d => d.FifoQueue)
                .WithMany(p => p.FifoQueueInbounds)
                .HasForeignKey(d => d.FifoQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FifoQueueInbound_FifoQueue");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FifoQueueInbound> entity);
    }
}
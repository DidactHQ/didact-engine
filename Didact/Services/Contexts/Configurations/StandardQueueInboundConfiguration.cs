using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DidactEngine.Models.Entities;

namespace DidactEngine.Services.Contexts.Configurations
{
    public partial class StandardQueueInboundConfiguration : IEntityTypeConfiguration<StandardQueueInbound>
    {
        public void Configure(EntityTypeBuilder<StandardQueueInbound> entity)
        {
            entity.ToTable(nameof(StandardQueueInbound));
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.LastUpdatedBy).HasMaxLength(255);
            entity.Property(e => e.RowVersion).IsRowVersion().IsConcurrencyToken();

            entity.HasOne(d => d.Organization)
                .WithMany(p => p.StandardQueueInbounds)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StandardQueueInbound_Organization");

            entity.HasOne(d => d.Flow)
                .WithMany(p => p.StandardQueueInbounds)
                .HasForeignKey(d => d.FlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StandardQueueInbound_Flow");

            entity.HasOne(d => d.StandardQueue)
                .WithMany(p => p.StandardQueueInbounds)
                .HasForeignKey(d => d.StandardQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StandardQueueInbound_StandardQueue");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StandardQueueInbound> entity);
    }
}
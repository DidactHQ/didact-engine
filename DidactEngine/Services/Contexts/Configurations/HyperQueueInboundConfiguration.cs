﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DidactCore.Entities;

namespace DidactEngine.Services.Contexts.Configurations
{
    public partial class HyperQueueInboundConfiguration : IEntityTypeConfiguration<HyperQueueInbound>
    {
        public void Configure(EntityTypeBuilder<HyperQueueInbound> entity)
        {
            entity.ToTable(nameof(HyperQueueInbound));
            entity.Property(e => e.CreatedBy).HasMaxLength(255);
            entity.Property(e => e.LastUpdatedBy).HasMaxLength(255);
            entity.Property(e => e.RowVersion).IsRowVersion().IsConcurrencyToken();

            entity.HasOne(d => d.Organization)
                .WithMany(p => p.HyperQueueInbounds)
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(HyperQueueInbound)}_{nameof(Organization)}");

            entity.HasOne(d => d.FlowRun)
                .WithMany(p => p.HyperQueueInbounds)
                .HasForeignKey(d => d.FlowRunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(HyperQueueInbound)}_{nameof(FlowRun)}");

            entity.HasOne(d => d.HyperQueue)
                .WithMany(p => p.HyperQueueInbounds)
                .HasForeignKey(d => d.HyperQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName($"FK_{nameof(HyperQueueInbound)}_${nameof(HyperQueue)}");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<HyperQueueInbound> entity);
    }
}
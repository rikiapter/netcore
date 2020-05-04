

using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.DAL.Configurations
{

    public class AlertsConfiguration : IEntityTypeConfiguration<Alerts>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Alerts> builder)
        {

            builder.HasKey(e => e.AlertId);

            builder.ToTable("BB_GEN_Alerts");

            builder.Property(e => e.AlertId).HasColumnName("AlertID");

            builder.Property(e => e.AlertTypeId).HasColumnName("AlertTypeID");
            builder.Property(e => e.AlertStatusId).HasColumnName("AlertStatus");

            builder.Property(e => e.AlertValidDate).HasColumnType("datetime");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.Date).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                        .HasColumnName("dateInsert")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                        .HasColumnName("dateUpdate")
                        .HasColumnType("datetime");

            builder.Property(e => e.EntityId).HasColumnName("EntityID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.SiteId).HasColumnName("SiteID");

            builder.Property(e => e.State)
                        .HasColumnName("state")
                        .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                        .HasColumnName("userInsert")
                        .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.AlertType)
                        .WithMany(p => p.Alerts)
                        .HasForeignKey(d => d.AlertTypeId)
                        .HasConstraintName("FK_BB_GEN_Alerts_AlertTypeID");

            //builder.HasOne(d => d.EntityType)
            //            .WithMany(p => p.Alerts)
            //            .HasForeignKey(d => d.EntityTypeId)
            //            .HasConstraintName("FK_BB_GEN_Alerts_EntityTypeID");

            //builder.HasOne(d => d.Site)
            //            .WithMany(p => p.Alerts)
            //            .HasForeignKey(d => d.SiteId)
            //            .HasConstraintName("FK_BB_GEN_Alerts_SiteID");
        }
    }
    public class AlertTypeConfiguration : IEntityTypeConfiguration<AlertType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>

        public void Configure(EntityTypeBuilder<AlertType> builder)
        {

            builder.HasKey(e => e.AlertTypeId);

            builder.ToTable("BB_GEN_AlertType");

            builder.Property(e => e.AlertTypeId).HasColumnName("AlertTypeID");

            builder.Property(e => e.AlertTypeName).HasMaxLength(50);

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class AlertTypeEntityConfiguration : IEntityTypeConfiguration<AlertTypeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>

        public void Configure(EntityTypeBuilder<AlertTypeEntity> builder)
        {

            builder.HasKey(e => e.AlertEntityId);

            builder.ToTable("BB_GEN_AlertTypeEntity");

            builder.Property(e => e.AlertEntityId).HasColumnName("AlertEntityID");

            builder.Property(e => e.AlertTypeId).HasColumnName("AlertTypeID");

            builder.Property(e => e.DateInsert)
                                .HasColumnName("dateInsert")
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                                .HasColumnName("dateUpdate")
                                .HasColumnType("datetime");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.State)
                                .HasColumnName("state")
                                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                                .HasColumnName("userInsert")
                                .HasDefaultValueSql("((1))");

            //builder.HasOne(d => d.AlertType)
            //.WithMany(p => p.AlertTypeEntity)
            //                    .HasForeignKey(d => d.AlertTypeId)
            //                    .HasConstraintName("FK_BB_GEN_AlertTypeEntity_AlertTypeID");

            //builder.HasOne(d => d.EntityType)
            //                    .WithMany(p => p.AlertTypeEntity)
            //                    .HasForeignKey(d => d.EntityTypeId)
            //                    .HasConstraintName("FK_BB_GEN_AlertTypeEntity_EntityTypeID");
        }
    }
    public class AlertStatusConfiguration : IEntityTypeConfiguration<AlertStatus>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>

        public void Configure(EntityTypeBuilder<AlertStatus> builder)
        {

            builder.HasKey(e => e.AlertStatusId);

            builder.ToTable("BB_GEN_AlertStatus");

            builder.Property(e => e.AlertStatusId).HasColumnName("AlertStatusID");

            builder.Property(e => e.AlertStatusName).HasMaxLength(50);

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
}
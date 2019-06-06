using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.DAL.Configurations
{


    public class EquipmenAtSiteConfiguration : IEntityTypeConfiguration<EquipmenAtSite>
    {
        public void Configure(EntityTypeBuilder<EquipmenAtSite> builder)
        {

            builder.HasKey(e => e.EquipmentAtSiteId);

            builder.ToTable("BB_EC_EquipmenAtSite");

            builder.Property(e => e.EquipmentAtSiteId).HasColumnName("EquipmentAtSiteID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateFrom)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTo).HasColumnType("datetime");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

            builder.Property(e => e.SiteId).HasColumnName("SiteID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Equipment)
                .WithMany(p => p.EquipmenAtSite)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_BB_EC_EquipmenAtSite_EquipmentID");

            builder.HasOne(d => d.Site)
                .WithMany(p => p.EquipmenAtSite)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_BB_EC_EquipmenAtSite_SiteID");


        }
    }

    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(e => e.EquipmentId);

            builder.ToTable("BB_EC_Equipment");

            builder.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EquipmentStatusTypeId).HasColumnName("EquipmentStatusTypeID");

            builder.Property(e => e.EquipmentTypeId).HasColumnName("EquipmentTypeID");

            builder.Property(e => e.ManufactureName).HasMaxLength(50);

            builder.Property(e => e.Model).HasMaxLength(50);

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.EquipmentStatusType)
                .WithMany(p => p.Equipment)
                .HasForeignKey(d => d.EquipmentStatusTypeId)
                .HasConstraintName("FK_BB_EC_Equipment_EquipmentStatusTypeID");

            builder.HasOne(d => d.EquipmentType)
                .WithMany(p => p.Equipment)
                .HasForeignKey(d => d.EquipmentTypeId)
                .HasConstraintName("FK_BB_EC_Equipment_EquipmentTypeID");

            builder.HasOne(d => d.Organization)
                .WithMany(p => p.Equipment)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_BB_EC_Equipment_OrganizationID");
        }
    }
        public class EquipmentLocationConfiguration : IEntityTypeConfiguration<EquipmentLocation>
        {
            public void Configure(EntityTypeBuilder<EquipmentLocation> builder)
            {

                builder.HasKey(e => e.EquipmenLocationId);

                builder.ToTable("BB_EC_EquipmentLocation");

                builder.Property(e => e.EquipmenLocationId).HasColumnName("EquipmenLocationID");

                builder.Property(e => e.Comment).HasMaxLength(50);

                builder.Property(e => e.Date)
                            .HasColumnType("date")
                            .HasDefaultValueSql("(getdate())");

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

                builder.Property(e => e.Time).HasDefaultValueSql("(getdate())");

                builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
            }
        }
        public class EquipmentStatusTypeConfiguration : IEntityTypeConfiguration<EquipmentStatusType>
        {
            public void Configure(EntityTypeBuilder<EquipmentStatusType> builder)
            {
                builder.HasKey(e => e.EquipmentStatusTypeId);

                builder.ToTable("BB_EC_EquipmentStatusType");

                builder.Property(e => e.EquipmentStatusTypeId).HasColumnName("EquipmentStatusTypeID");

                builder.Property(e => e.Comment).HasMaxLength(50);

                builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

                builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

                builder.Property(e => e.EquipmentStatusTypeName).HasMaxLength(50);

                builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

                builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
            }
        }


        public class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
        {
            public void Configure(EntityTypeBuilder<EquipmentType> builder)
            {

                builder.HasKey(e => e.EquipmentTypeId);

                builder.ToTable("BB_EC_EquipmentType");

                builder.Property(e => e.EquipmentTypeId).HasColumnName("EquipmentTypeID");

                builder.Property(e => e.Comment).HasMaxLength(50);

                builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

                builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

                builder.Property(e => e.EquipmentTypeName).HasMaxLength(50);

                builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

                builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
            }
        }

    }
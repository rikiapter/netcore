

using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malam.Mastpen.Core.DAL.Configurations
{

    public class EmployeeEntryConfiguration : IEntityTypeConfiguration<EmployeeEntry>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EmployeeEntry> builder)
        {

                builder.HasKey(e => e.EmployeeEntryId);

                builder.ToTable("BB_EC_EmployeeEntry");

                builder.Property(e => e.EmployeeEntryId).HasColumnName("EmployeeEntryID");

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

                builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                builder.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                builder.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

                builder.Property(e => e.Time).HasDefaultValueSql("(getdate())");

                builder.Property(e => e.UserInsert)
                    .HasColumnName("userInsert")
                    .HasDefaultValueSql("((1))");

                builder.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEntry)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_BB_EC_EmployeeEntry_EmployeeID");

                builder.HasOne(d => d.Equipment)
                    .WithMany(p => p.EmployeeEntry)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_BB_EC_EmployeeEntry_EquipmentID");
        
        }
    }

    public class SiteEmployeeConfiguration : IEntityTypeConfiguration<SiteEmployee>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<SiteEmployee> builder)
        {

            builder.HasKey(e => e.SiteEmployeeId);

            builder.ToTable("BB_EC_SiteEmployee");

            builder.Property(e => e.SiteEmployeeId).HasColumnName("SiteEmployeeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateFrom).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                    .HasColumnName("dateInsert")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTo).HasColumnType("datetime");

            builder.Property(e => e.DateUpdate)
                    .HasColumnName("dateUpdate")
                    .HasColumnType("datetime");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.SiteId).HasColumnName("SiteID");

            builder.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                    .HasColumnName("userInsert")
                    .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                    .WithMany(p => p.SiteEmployeeEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_BB_EC_SiteEmployee_EmployeeID");

            builder.HasOne(d => d.Site)
                    .WithMany(p => p.SiteEmployeeSite)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("FK_BB_EC_SiteEmployee_SiteID");
            
        }
    }


    public class EmplyeePictureConfiguration : IEntityTypeConfiguration<EmplyeePicture>
    {
        public void Configure(EntityTypeBuilder<EmplyeePicture> builder)
        {
            // Set configuration for entity  
            builder.HasKey(e => e.EmployeePictureId);

            builder.ToTable("BB_EmplyeePicture");

            builder.Property(e => e.EmployeePictureId).HasColumnName("EmployeePictureID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EmployeeFacePrintId)
                .HasColumnName("EmployeeFacePrintID")
                .HasMaxLength(50);

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmplyeePicture)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_EmplyeePicture_EmployeeID");

        }
    }
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.ToTable("BB_HR_Employee");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.BirthDate).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.FirstName).HasMaxLength(50);

            builder.Property(e => e.FirstNameEn)
                .HasColumnName("FirstNameEN")
                .HasMaxLength(50);

            builder.Property(e => e.GenderId).HasColumnName("GenderID");

            builder.Property(e => e.Address).HasMaxLength(50);

            builder.Property(e => e.IdentificationTypeId)
                .HasColumnName("IdentificationTypeID")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.LastName).HasMaxLength(50);

            builder.Property(e => e.LastNameEn)
                .HasColumnName("LastNameEN")
                .HasMaxLength(50);

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            builder.Property(e => e.PassportCountryId).HasColumnName("PassportCountryID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Gender)
                .WithMany(p => p.Employee)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_BB_HR_Employee_GenderID");

            builder.HasOne(d => d.IdentificationType)
                .WithMany(p => p.Employee)
                .HasForeignKey(d => d.IdentificationTypeId)
                .HasConstraintName("FK_BB_HR_Employee_IdentificationTypeID");

            builder.HasOne(d => d.Organization)
                .WithMany(p => p.Employee)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_BB_HR_Employee_OrganizationID");

            builder.HasOne(d => d.PassportCountry)
                .WithMany(p => p.Employee)
                .HasForeignKey(d => d.PassportCountryId)
                .HasConstraintName("FK_BB_HR_Employee_PassportCountryID");
        }
    }

    public class EmployeeAuthtorizationConfiguration : IEntityTypeConfiguration<EmployeeAuthtorization>
    {
        public void Configure(EntityTypeBuilder<EmployeeAuthtorization> builder)
        {
            builder.HasKey(e => e.EmployeeAuthorizationId);

            builder.ToTable("BB_HR_EmployeeAuthtorization");

            builder.Property(e => e.EmployeeAuthorizationId).HasColumnName("EmployeeAuthorizationID");

            builder.Property(e => e.AuthorizationTypeId).HasColumnName("AuthorizationTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateFrom).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTo).HasColumnType("datetime");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.AuthorizationType)
                            .WithMany(p => p.EmployeeAuthtorization)
                            .HasForeignKey(d => d.AuthorizationTypeId)
                            .HasConstraintName("FK_BB_HR_EmployeeAuthtorization_AuthorizationTypeID");

            builder.HasOne(d => d.Employee)
                            .WithMany(p => p.EmployeeAuthtorization)
                            .HasForeignKey(d => d.EmployeeId)
                            .HasConstraintName("FK_BB_HR_EmployeeAuthtorization_EmployeeID");
        }
    }
    public class EmployeeTrainingPictureConfiguration : IEntityTypeConfiguration<EmployeeTraining>
    {
        public void Configure(EntityTypeBuilder<EmployeeTraining> builder)
        {
            builder.HasKey(e => e.EmployeeTrainingId);

            builder.ToTable("BB_HR_EmployeeTraining");

            builder.Property(e => e.EmployeeTrainingId).HasColumnName("EmployeeTrainingID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateFrom).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTo).HasColumnType("datetime");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.TrainingTypeId).HasColumnName("TrainingTypeID");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeTraining)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_HR_EmployeeTraining_EmployeeID");

            builder.HasOne(d => d.TrainingType)
                .WithMany(p => p.EmployeeTraining)
                .HasForeignKey(d => d.TrainingTypeId)
                .HasConstraintName("FK_BB_HR_EmployeeTraining_TrainingTypeID");

        }
    }
    public class EmployeeWorkPermitConfiguration : IEntityTypeConfiguration<EmployeeWorkPermit>
    {
        public void Configure(EntityTypeBuilder<EmployeeWorkPermit> builder)
        {
            builder.HasKey(e => e.EmployeeWorkPermitId);

            builder.ToTable("BB_HR_EmployeeWorkPermit");

            builder.Property(e => e.EmployeeWorkPermitId).HasColumnName("EmployeeWorkPermitID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateFrom).HasColumnType("datetime");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTo).HasColumnType("datetime");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeWorkPermit)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_HR_EmployeeWorkPermit_EmployeeID");
        }
    }

    public class EmployeeProffesionTypeConfiguration : IEntityTypeConfiguration<EmployeeProffesionType>
    {
        public void Configure(EntityTypeBuilder<EmployeeProffesionType> builder)
        {
            builder.HasKey(e => e.EmployeeAuthorizationId);

            builder.ToTable("BB_HR_EmployeeProffesionType");

            builder.Property(e => e.EmployeeAuthorizationId).HasColumnName("EmployeeAuthorizationID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.ProffesionTypeId).HasColumnName("ProffesionTypeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeProffesionType)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_HR_EmployeeProffesionType_EmployeeID");

            builder.HasOne(d => d.ProffesionType)
                .WithMany(p => p.EmployeeProffesionType)
                .HasForeignKey(d => d.ProffesionTypeId)
                .HasConstraintName("FK_BB_HR_EmployeeProffesionType_ProffesionTypeID");

        }
    }

}
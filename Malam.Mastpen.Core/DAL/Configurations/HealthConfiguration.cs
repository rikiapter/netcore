
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Malam.Mastpen.Core.DAL.Configurations
{
    public class EmployeeTemperatureConfiguration : IEntityTypeConfiguration<EmployeeTemperature>
    {
        public void Configure(EntityTypeBuilder<EmployeeTemperature> builder)
        {
            builder.HasKey(e => e.EmployeeTemperatureId)
                   .HasName("PK_BB_VR_Employee_Temperature");

            builder.ToTable("BB_VR_EmployeeTemperature");

            builder.Property(e => e.EmployeeTemperatureId).HasColumnName("EmployeeTemperatureID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateTest)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

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
                .WithMany(p => p.EmployeeTemperature)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_VR_Employee_Temperature_EmployeeID");

        }
    }
    public class EmployeeHealthConditionConfiguration : IEntityTypeConfiguration<EmployeeHealthCondition>
    {
        public void Configure(EntityTypeBuilder<EmployeeHealthCondition> builder)
        {
            builder.HasKey(e => e.EmployeeHealthConditionId);

            builder.ToTable("BB_VR_EmployeeHealthCondition");

            builder.Property(e => e.EmployeeHealthConditionId).HasColumnName("EmployeeHealthConditionID");

            builder.Property(e => e.ConditionTypeId).HasColumnName("ConditionTypeID");

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

            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeHealthCondition)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_VR_EmployeeHealthCondition_EmployeeID");

        }
    }
    public class HealthConditionTypeConfiguration : IEntityTypeConfiguration<HealthConditionType>
    {
        public void Configure(EntityTypeBuilder<HealthConditionType> builder)
        {
            builder.HasKey(e => e.ConditionTypeId);

            builder.ToTable("BB_VR_HealthConditionType");

            builder.Property(e => e.ConditionTypeId).HasColumnName("ConditionTypeID");

            builder.Property(e => e.ConditionTypeName).HasMaxLength(50);

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
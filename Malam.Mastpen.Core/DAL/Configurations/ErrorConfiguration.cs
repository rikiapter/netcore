

using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Malam.Mastpen.Core.DAL.Configurations
{
    public class ErrorConfiguration : IEntityTypeConfiguration<BbError>
    {
        public void Configure(EntityTypeBuilder<BbError> builder)
        {
            builder.HasKey(e => e.ErrorId);

            builder.ToTable("BB_Error");

            builder.Property(e => e.ErrorId).HasColumnName("ErrorID");
            builder.Property(e => e.ErrorTypeId).HasColumnName("ErrorTypeID");

            builder.Property(e => e.ErrorTitle).HasMaxLength(250);
            builder.Property(e => e.ErrorMessage).HasMaxLength(250);

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
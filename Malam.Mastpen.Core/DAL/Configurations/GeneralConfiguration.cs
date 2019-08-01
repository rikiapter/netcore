
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Malam.Mastpen.Core.DAL.Configurations
{
    public class AdressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.AddressId);

            builder.ToTable("BB_GEN_Address");

            builder.Property(e => e.AddressId).HasColumnName("AddressID");

            builder.Property(e => e.AptNo).HasMaxLength(50);

            builder.Property(e => e.CityId).HasColumnName("CityID");

            builder.Property(e => e.Comments).HasMaxLength(100);

            builder.Property(e => e.CoorX).HasMaxLength(50);

            builder.Property(e => e.CoorY).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EntityId).HasColumnName("EntityID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.EntranceNo).HasMaxLength(50);

            builder.Property(e => e.Pob).HasColumnName("POB");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.StreetName).HasMaxLength(50);

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.City)
                .WithMany(p => p.Address)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_BB_GEN_Adress_CityID");

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.AddressEntityType)
                .HasForeignKey(d => d.EntityTypeId)
                .HasConstraintName("FK_BB_GEN_Adress_EntityTypeID");

        }
    }

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {

            builder.HasKey(e => e.CityId);

            builder.ToTable("BB_GEN_City");

            builder.Property(e => e.CityId).HasColumnName("CityID");

            builder.Property(e => e.CityName).HasMaxLength(50);

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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.HasKey(e => e.CountryId);

            builder.ToTable("BB_GEN_Country");

            builder.Property(e => e.CountryId).HasColumnName("CountryID");
            builder.Property(e => e.LanguageId).HasColumnName("LanguageID");

            builder.Property(e => e.CountryName).HasMaxLength(50);

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
    public class DocsConfiguration : IEntityTypeConfiguration<Docs>
    {
        public void Configure(EntityTypeBuilder<Docs> builder)
        {
            builder.HasKey(e => e.DocumentId);

            builder.ToTable("BB_GEN_Docs");

            builder.Property(e => e.DocumentId).HasColumnName("DocumentID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.DocumentDate).HasColumnType("datetime");

            builder.Property(e => e.DocumentPath).HasMaxLength(250);

            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            builder.Property(e => e.EntityId).HasColumnName("EntityID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.LanguageId).HasColumnName("LanguageID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.ValIdDate).HasColumnType("datetime");

            builder.HasOne(d => d.DocumentType)
                .WithMany(p => p.Docs)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_BB_GEN_Docs_DocumentTypeID");

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.Docs)
                .HasForeignKey(d => d.EntityTypeId)
                .HasConstraintName("FK_BB_GEN_Docs_EntityTypeID");

            builder.HasOne(d => d.Language)
                .WithMany(p => p.Docs)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("FK_BB_GEN_Docs_LanguageID");

        }
    }


    public class DocTypeConfiguration : IEntityTypeConfiguration<DocType>
    {
        public void Configure(EntityTypeBuilder<DocType> builder)
        {
            builder.HasKey(e => e.DocumentTypeId);

            builder.ToTable("BB_GEN_DocType");

            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.DocumentTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

        }
    }
    public class DocTypeEntityConfiguration : IEntityTypeConfiguration<DocTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DocTypeEntity> builder)
        {

            builder.HasKey(e => e.DocumentEntityId);

            builder.ToTable("BB_GEN_DocTypeEntity");

            builder.Property(e => e.DocumentEntityId).HasColumnName("DocumentEntityID");

            builder.Property(e => e.DateInsert)
                    .HasColumnName("dateInsert")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                    .HasColumnName("dateUpdate")
                    .HasColumnType("datetime");

            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                    .HasColumnName("userInsert")
                    .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.DocumentType)
                    .WithMany(p => p.DocTypeEntity)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("FK_BB_GEN_DocTypeEntity_DocumentTypeID");

            builder.HasOne(d => d.EntityType)
                    .WithMany(p => p.DocTypeEntity)
                    .HasForeignKey(d => d.EntityTypeId)
                    .HasConstraintName("FK_BB_GEN_DocTypeEntity_EntityTypeID");


        }
    }
    public class EntityTypeConfiguration : IEntityTypeConfiguration<EntityType>
    {
        public void Configure(EntityTypeBuilder<EntityType> builder)
        {

            builder.HasKey(e => e.EntityTypeId);

            builder.ToTable("BB_GEN_EntityType");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.EntityTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.TableName).HasMaxLength(50);

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {


            builder.HasKey(e => e.LanguageId);

            builder.ToTable("BB_GEN_Language ");

            builder.Property(e => e.LanguageId).HasColumnName("LanguageID");

            builder.Property(e => e.DateInsert)
                    .HasColumnName("dateInsert")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                    .HasColumnName("dateUpdate")
                    .HasColumnType("datetime");

            builder.Property(e => e.LanguageName).HasMaxLength(50);

            builder.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                    .HasColumnName("userInsert")
                    .HasDefaultValueSql("((1))");
        }
    }

    public class NotesConfiguration : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {
            builder.HasKey(e => e.NoteId);

            builder.ToTable("BB_GEN_Notes");

            builder.Property(e => e.NoteId).HasColumnName("NoteID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.EntityId).HasColumnName("EntityID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.NoteContent).HasMaxLength(50);

            builder.Property(e => e.NoteTypeId).HasColumnName("NoteTypeID");
            builder.Property(e => e.SiteId).HasColumnName("SiteID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.Notes)
                .HasForeignKey(d => d.EntityTypeId)
                .HasConstraintName("FK_BB_GEN_Notes_EntityTypeID");

            builder.HasOne(d => d.NoteType)
                .WithMany(p => p.Notes)
                .HasForeignKey(d => d.NoteTypeId)
                .HasConstraintName("FK_BB_GEN_Notes_NoteTypeID");

        }
    }

    public class NoteTypeConfiguration : IEntityTypeConfiguration<NoteType>
    {
        public void Configure(EntityTypeBuilder<NoteType> builder)
        {
            builder.HasKey(e => e.NoteTypeId);

            builder.ToTable("BB_GEN_NoteType");

            builder.Property(e => e.NoteTypeId).HasColumnName("NoteTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.NoteTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(e => e.OrganizationId);

            builder.ToTable("BB_GEN_Organization");

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.OrganizationName).HasMaxLength(50);

            builder.Property(e => e.OrganizationParentId).HasColumnName("OrganizationParentID");

            builder.Property(e => e.OrganizationTypeId).HasColumnName("OrganizationTypeID");

            builder.Property(e => e.OrganizationExpertiseTypeId).HasColumnName("OrganizationExpertiseTypeID");

            builder.Property(e => e.OrganizationFaceGroup).HasMaxLength(50);

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            //builder.HasOne(d => d.OrganizationType)
            //    .WithMany(p => p.Organization)
            //    .HasForeignKey(d => d.OrganizationTypeId)
            //    .HasConstraintName("FK_BB_GEN_Organization_OrganizationTypeID");


            //builder.HasOne(d => d.OrganizationExpertiseType)
            //    .WithMany(p => p.Organization)
            //    .HasForeignKey(d => d.OrganizationExpertiseTypeId)
            //    .HasConstraintName("FK_BB_GEN_Organization_OrganizationTypeID");

        }
    }


    public class OrganizationExpertiseTypeConfiguration : IEntityTypeConfiguration<OrganizationExpertiseType>
    {
        public void Configure(EntityTypeBuilder<OrganizationExpertiseType> builder)
        {
            builder.HasKey(e => e.OrganizationExpertiseTypeId);

            builder.ToTable("BB_GEN_OrganizationExpertiseType");

            builder.Property(e => e.OrganizationExpertiseTypeId).HasColumnName("OrganizationExpertiseTypeID");

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.OrganizationTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class OrganizationTypeConfiguration : IEntityTypeConfiguration<OrganizationType>
    {
        public void Configure(EntityTypeBuilder<OrganizationType> builder)
        {

            builder.HasKey(e => e.OrganizationTypeId);

            builder.ToTable("BB_GEN_OrganizationType");

            builder.Property(e => e.OrganizationTypeId).HasColumnName("OrganizationTypeID");

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.OrganizationTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class PhoneMailConfiguration : IEntityTypeConfiguration<PhoneMail>
    {
        public void Configure(EntityTypeBuilder<PhoneMail> builder)
        {
            builder.HasKey(e => e.ContactId);

            builder.ToTable("BB_GEN_PhoneMail");

            builder.Property(e => e.ContactId).HasColumnName("ContactID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.Email)
                .HasColumnName("EMail")
                .HasMaxLength(50);

            builder.Property(e => e.EntityId).HasColumnName("EntityID");

            builder.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");

            builder.Property(e => e.PhoneNumber).HasMaxLength(50);

            builder.Property(e => e.PhoneTypeId).HasColumnName("PhoneTypeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.PhoneMail)
                .HasForeignKey(d => d.EntityTypeId)
                .HasConstraintName("FK_BB_GEN_PhoneMail_EntityTypeID");

            builder.HasOne(d => d.PhoneType)
                .WithMany(p => p.PhoneMail)
                .HasForeignKey(d => d.PhoneTypeId)
                .HasConstraintName("FK_BB_GEN_PhoneMail_PhoneTypeID");

        }
    }

    public class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
    {
        public void Configure(EntityTypeBuilder<PhoneType> builder)
        {

            builder.HasKey(e => e.PhoneTypeId);

            builder.ToTable("BB_GEN_PhoneType");

            builder.Property(e => e.PhoneTypeId).HasColumnName("PhoneTypeID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.PhoneTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

        }
    }


    public class SiteRoleConfiguration : IEntityTypeConfiguration<SiteRole>
    {
        public void Configure(EntityTypeBuilder<SiteRole> builder)
        {
            builder.HasKey(e => e.SiteRoleId);

            builder.ToTable("BB_GEN_SiteRole");

            builder.Property(e => e.SiteRoleId).HasColumnName("SiteRoleID");

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

            builder.Property(e => e.SiteRoleTypeId).HasColumnName("SiteRoleTypeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.SiteRole)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BB_GEN_SiteRole_EmployeeID");

            builder.HasOne(d => d.Site)
                .WithMany(p => p.SiteRole)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_BB_GEN_SiteRole_SiteID");

            builder.HasOne(d => d.SiteRoleType)
                .WithMany(p => p.SiteRole)
                .HasForeignKey(d => d.SiteRoleTypeId)
                .HasConstraintName("FK_BB_GEN_SiteRole_SiteRoleTypeID");

        }
    }


    public class SiteRoleTypeConfiguration : IEntityTypeConfiguration<SiteRoleType>
    {
        public void Configure(EntityTypeBuilder<SiteRoleType> builder)
        {
            builder.HasKey(e => e.SiteRoleTypeId);

            builder.ToTable("BB_GEN_SiteRoleType");

            builder.Property(e => e.SiteRoleTypeId).HasColumnName("SiteRoleTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                                .HasColumnName("dateInsert")
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                                .HasColumnName("dateUpdate")
                                .HasColumnType("datetime");

            builder.Property(e => e.SiteRoleTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                                .HasColumnName("state")
                                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                                .HasColumnName("userInsert")
                                .HasDefaultValueSql("((1))");
        }
    }
    public class SitesConfiguration : IEntityTypeConfiguration<Sites>
    {
        public void Configure(EntityTypeBuilder<Sites> builder)
        {
            builder.HasKey(e => e.SiteId);

            builder.ToTable("BB_GEN_Sites");

            builder.Property(e => e.SiteId).HasColumnName("SiteID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

            builder.Property(e => e.SiteActivityStartDate).HasColumnType("datetime");

            builder.Property(e => e.SiteName).HasMaxLength(50);

            builder.Property(e => e.SiteStatusId).HasColumnName("SiteStatusID");

            builder.Property(e => e.SiteTypeId).HasColumnName("SiteTypeID");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");

            //builder.HasOne(d => d.Organization)
            //    .WithMany(p => p.Sites)
            //    .HasForeignKey(d => d.OrganizationId)
            //    .HasConstraintName("FK_BB_GEN_Sites_OrganizationID");

            builder.HasOne(d => d.SiteStatus)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.SiteStatusId)
                .HasConstraintName("FK_BB_GEN_Sites_SiteStatusID");

            builder.HasOne(d => d.SiteType)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.SiteTypeId)
                .HasConstraintName("FK_BB_GEN_Sites_SiteTypeID");
        }
    }

    public class SiteStatusConfiguration : IEntityTypeConfiguration<SiteStatus>
    {
        public void Configure(EntityTypeBuilder<SiteStatus> builder)
        {
            builder.HasKey(e => e.SiteStatusId);

            builder.ToTable("BB_GEN_SiteStatus");

            builder.Property(e => e.SiteStatusId).HasColumnName("SiteStatusID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.SiteStatusName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class SiteTypeConfiguration : IEntityTypeConfiguration<SiteType>
    {
        public void Configure(EntityTypeBuilder<SiteType> builder)
        {

            builder.HasKey(e => e.SiteTypeId);

            builder.ToTable("BB_GEN_SiteType");

            builder.Property(e => e.SiteTypeId).HasColumnName("SiteTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                        .HasColumnName("dateInsert")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                        .HasColumnName("dateUpdate")
                        .HasColumnType("datetime");

            builder.Property(e => e.SiteTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                        .HasColumnName("state")
                        .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                        .HasColumnName("userInsert")
                        .HasDefaultValueSql("((1))");
        }
    }
    public class AuthtorizationTypeConfiguration : IEntityTypeConfiguration<AuthtorizationType>
    {
        public void Configure(EntityTypeBuilder<AuthtorizationType> builder)
        {

            builder.HasKey(e => e.AuthorizationTypeId);

            builder.ToTable("BB_HR_AuthtorizationType");

            builder.Property(e => e.AuthorizationTypeId).HasColumnName("AuthorizationTypeID");

            builder.Property(e => e.AuthorizationTypeName).HasMaxLength(50);

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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {

            builder.HasKey(e => e.GenderId);

            builder.ToTable("BB_HR_Gender");

            builder.Property(e => e.GenderId).HasColumnName("GenderID");

            builder.Property(e => e.DateInsert)
                .HasColumnName("dateInsert")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                .HasColumnName("dateUpdate")
                .HasColumnType("datetime");

            builder.Property(e => e.GenderName)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'זכר')");

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                .HasColumnName("userInsert")
                .HasDefaultValueSql("((1))");
        }
    }
    public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
    {
        public void Configure(EntityTypeBuilder<IdentificationType> builder)
        {

            builder.HasKey(e => e.IdentificationTypeId);

            builder.ToTable("BB_HR_IdentificationType");

            builder.Property(e => e.IdentificationTypeId).HasColumnName("IdentificationTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.IdentificationTypeName)
                            .HasMaxLength(50)
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }

    public class ProffesionTypeConfiguration : IEntityTypeConfiguration<ProffesionType>
    {
        public void Configure(EntityTypeBuilder<ProffesionType> builder)
        {

            builder.HasKey(e => e.ProffesionTypeId);

            builder.ToTable("BB_HR_ProffesionType");

            builder.Property(e => e.ProffesionTypeId).HasColumnName("ProffesionTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                            .HasColumnName("dateInsert")
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                            .HasColumnName("dateUpdate")
                            .HasColumnType("datetime");

            builder.Property(e => e.ProffesionTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                            .HasColumnName("state")
                            .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                            .HasColumnName("userInsert")
                            .HasDefaultValueSql("((1))");
        }
    }
    public class SearchTypeAdvancedConfiguration : IEntityTypeConfiguration<SearchTypeAdvanced>
    {
        public void Configure(EntityTypeBuilder<SearchTypeAdvanced> builder)
        {

            builder.HasKey(e => e.SearchAdvancedTypeId);

            builder.ToTable("BB_HR_SearchTypeAdvanced ");

            builder.Property(e => e.SearchAdvancedTypeId).HasColumnName("SearchAdvancedTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                                .HasColumnName("dateInsert")
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                                .HasColumnName("dateUpdate")
                                .HasColumnType("datetime");

            builder.Property(e => e.SearchAdvancedTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                                .HasColumnName("state")
                                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                                .HasColumnName("userInsert")
                                .HasDefaultValueSql("((1))");
        }
    }
    public class SearchTypeSimpleConfiguration : IEntityTypeConfiguration<SearchTypeSimple>
    {
        public void Configure(EntityTypeBuilder<SearchTypeSimple> builder)
        {

            builder.HasKey(e => e.SearchSimpleTypeId);

            builder.ToTable("BB_HR_SearchTypeSimple");

            builder.Property(e => e.SearchSimpleTypeId).HasColumnName("SearchSimpleTypeID");

            builder.Property(e => e.Comment).HasMaxLength(50);

            builder.Property(e => e.DateInsert)
                                .HasColumnName("dateInsert")
                                .HasColumnType("datetime")
                                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateUpdate)
                                .HasColumnName("dateUpdate")
                                .HasColumnType("datetime");

            builder.Property(e => e.SearchSimpleTypeName).HasMaxLength(50);

            builder.Property(e => e.State)
                                .HasColumnName("state")
                                .HasDefaultValueSql("((1))");

            builder.Property(e => e.UserInsert)
                                .HasColumnName("userInsert")
                                .HasDefaultValueSql("((1))");
        }
    }
    public class TrainingTypeConfiguration : IEntityTypeConfiguration<TrainingType>
    {
        public void Configure(EntityTypeBuilder<TrainingType> builder)
        {

            builder.HasKey(e => e.TrainingTypeId);

            builder.ToTable("BB_HR_TrainingType");

            builder.Property(e => e.TrainingTypeId).HasColumnName("TrainingTypeID");

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

            builder.Property(e => e.TrainingTypeName).HasMaxLength(50);

            builder.Property(e => e.UserInsert)
                                .HasColumnName("userInsert")
                                .HasDefaultValueSql("((1))");
        }
    }

}
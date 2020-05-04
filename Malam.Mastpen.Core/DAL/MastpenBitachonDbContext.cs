using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malam.Mastpen.Core.DAL.Configurations;
using Malam.Mastpen.Core.DAL.Dbo;
using Malam.Mastpen.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Malam.Mastpen.Core.DAL
{

    public class MastpenBitachonDbContext : DbContext
    {

        /// <summary>
        ///ניתן ליצור אוטמטית עי הפקודה
        ///Scaffold-DbContext "server=malam-dev01.database.windows.net;database=MastpenDb;User Id=dbadmin; Password=!1qaz@2wsx;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        ///בפרויקט נפרד MVC core
        /// </summary>
        /// <param name="options"></param>

        public MastpenBitachonDbContext(DbContextOptions<MastpenBitachonDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// todo modelBuilder יש להוסיף לכל מבנה חדש שורה ב 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity


            //יש להוסיף כל טבלת קונפיג חדשה לכאן
            modelBuilder
                .ApplyConfiguration(new EmployeeConfiguration())
                .ApplyConfiguration(new SiteEmployeeConfiguration())
                .ApplyConfiguration(new EmplyeePictureConfiguration())
                .ApplyConfiguration(new EmployeeTrainingPictureConfiguration())
                .ApplyConfiguration(new EmployeeWorkPermitConfiguration())
                .ApplyConfiguration(new EmployeeAuthtorizationConfiguration())
                .ApplyConfiguration(new EmployeeEntryConfiguration())
                .ApplyConfiguration(new EmployeeProffesionTypeConfiguration())
                 .ApplyConfiguration(new UsersConfiguration());

            modelBuilder
                 .ApplyConfiguration(new EquipmenAtSiteConfiguration())
                 .ApplyConfiguration(new EquipmentLocationConfiguration())
                 .ApplyConfiguration(new EquipmentStatusTypeConfiguration())
                 .ApplyConfiguration(new EquipmentTypeConfiguration())
                 .ApplyConfiguration(new EquipmentConfiguration());

            modelBuilder
                  .ApplyConfiguration(new AdressConfiguration())
                  .ApplyConfiguration(new DocsConfiguration())
                  .ApplyConfiguration(new DocTypeConfiguration())
                  .ApplyConfiguration(new DocTypeEntityConfiguration())
                  .ApplyConfiguration(new NotesConfiguration())
                  .ApplyConfiguration(new OrganizationConfiguration())
                  .ApplyConfiguration(new OrganizationExpertiseTypeConfiguration())
                  .ApplyConfiguration(new OrganizationTypeConfiguration())
                  .ApplyConfiguration(new PhoneMailConfiguration())
                  .ApplyConfiguration(new PhoneTypeConfiguration())
                  .ApplyConfiguration(new SiteRoleConfiguration())
                  .ApplyConfiguration(new CountryConfiguration())
                  .ApplyConfiguration(new CityConfiguration())
                  .ApplyConfiguration(new EntityTypeConfiguration())
                  .ApplyConfiguration(new GenderConfiguration())
                  .ApplyConfiguration(new IdentificationTypeConfiguration())
                  .ApplyConfiguration(new AuthtorizationTypeConfiguration())
                  .ApplyConfiguration(new NoteTypeConfiguration())
                  .ApplyConfiguration(new SitesConfiguration())
                  .ApplyConfiguration(new SiteRoleTypeConfiguration())
                  .ApplyConfiguration(new SearchTypeAdvancedConfiguration())
                  .ApplyConfiguration(new SearchTypeSimpleConfiguration())
                  .ApplyConfiguration(new LanguageConfiguration())
                  .ApplyConfiguration(new TrainingTypeConfiguration())
                  .ApplyConfiguration(new ProffesionTypeConfiguration())
                  .ApplyConfiguration(new EnteryRulesConfiguration())
                  .ApplyConfiguration(new OrganizationEnteryRulesConfiguration());

            modelBuilder
            .ApplyConfiguration(new EmployeeBodyHeatConfiguration())
            .ApplyConfiguration(new HealthConditionTypeConfiguration())
            .ApplyConfiguration(new EmployeeHealthConditionConfiguration());

            modelBuilder
              .ApplyConfiguration(new AlertsConfiguration())
              .ApplyConfiguration(new AlertTypeConfiguration())
              .ApplyConfiguration(new AlertStatusConfiguration())
              .ApplyConfiguration(new AlertTypeEntityConfiguration());

            modelBuilder
                .ApplyConfiguration(new TrainingDocsConfiguration());


            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// todo יש להוסיף לכל מבנה חדש שורה ב 
        /// </summary>
        /// <param name="modelBuilder"></param>

        public DbSet<ChangeLogExclusion> ChangeLogExclusions { get; set; }

        public virtual DbSet<EmployeeEntry> EmployeeEntry { get; set; }
        public virtual DbSet<EquipmenAtSite> EquipmenAtSite { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentLocation> EquipmentLocation { get; set; }
        public virtual DbSet<EquipmentStatusType> EquipmentStatusType { get; set; }
        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<SiteEmployee> SiteEmployee { get; set; }
        public virtual DbSet<EmplyeePicture> EmplyeePicture { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Docs> Docs { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<DocTypeEntity> DocTypeEntity { get; set; }
        public virtual DbSet<EntityType> EntityType { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<NoteType> NoteType { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationExpertiseType> OrganizationExpertiseType { get; set; }
        public virtual DbSet<OrganizationType> OrganizationType { get; set; }
        public virtual DbSet<PhoneMail> PhoneMail { get; set; }
        public virtual DbSet<PhoneType> PhoneType { get; set; }
        public virtual DbSet<SiteRole> SiteRole { get; set; }
        public virtual DbSet<SiteRoleType> SiteRoleType { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
        public virtual DbSet<SiteStatus> SiteStatus { get; set; }
        public virtual DbSet<SiteType> SiteType { get; set; }
        public virtual DbSet<AuthtorizationType> AuthtorizationType { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAuthtorization> EmployeeAuthtorization { get; set; }
        public virtual DbSet<EmployeeProffesionType> EmployeeProffesionType { get; set; }
        public virtual DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        public virtual DbSet<EmployeeWorkPermit> EmployeeWorkPermit { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<IdentificationType> IdentificationType { get; set; }
        public virtual DbSet<ProffesionType> ProffesionType { get; set; }
        public virtual DbSet<SearchTypeAdvanced> SearchTypeAdvanced { get; set; }
        public virtual DbSet<SearchTypeSimple> SearchTypeSimple { get; set; }
        public virtual DbSet<TrainingType> TrainingType { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<EnteryRules> EnteryRules { get; set; }
        public virtual DbSet<OrganizationEnteryRules> OrganizationEnteryRules { get; set; }


        public virtual DbSet<EmployeeBodyHeat> EmployeeBodyHeat { get; set; }
        public virtual DbSet<EmployeeHealthCondition> EmployeeHealthCondition { get; set; }
        public virtual DbSet<HealthConditionType> HealthConditionType { get; set; }
        public virtual DbSet<Alerts> Alerts { get; set; }
        public virtual DbSet<AlertType> AlertType { get; set; }
        public virtual DbSet<AlertTypeEntity> AlertTypeEntity { get; set; }
        public virtual DbSet<AlertStatus> AlertStatus { get; set; }
        public virtual DbSet<TrainingDocs> TrainingDocs { get; set; }
    }
}

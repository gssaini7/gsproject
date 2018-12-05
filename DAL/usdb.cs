namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using R.BusinessEntities;
    using System.Configuration;
    using System.Data.SqlClient;

    public partial class usdbdatacontext : DbContext
    {
        //public string UssSchemaName = ConfigurationManager.AppSettings["UssSchemaName"];
        //public string SchoolSchemaName = ConfigurationManager.AppSettings["SchoolSchemaName"];


        public usdbdatacontext()
            : base("name=ussdbEntities")
        {
            Database.SetInitializer<usdbdatacontext>(null);
        }

        public usdbdatacontext(MainDatabasesModel connstr)
           : base(ConnectionString(connstr))
        {
            Database.SetInitializer<usdbdatacontext>(null);
        }

        private static string ConnectionString(MainDatabasesModel dbname)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dbname.IDataSource;
            sqlBuilder.InitialCatalog = dbname.IInitialCatalog;
            if (dbname.IUsername == null || dbname.IUsername == string.Empty)
            {
                sqlBuilder.PersistSecurityInfo = true;
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                sqlBuilder.UserID = dbname.IUsername;
                sqlBuilder.Password = dbname.IPassword;
            }
            sqlBuilder.MultipleActiveResultSets = true;
            return sqlBuilder.ToString();
        }


        public virtual DbSet<MainDatabasesModel> MainDatabasesModels { get; set; }

        
        public virtual DbSet<AlbumModel> Albums { get; set; }
        public virtual DbSet<ImageGalleryModel> ImageGallerys { get; set; }
        public virtual DbSet<StudentModel> Students { get; set; }
        public virtual DbSet<AttendanceModel> Attendances { get; set; }
        public virtual DbSet<LedgerDetailModel> LedgerDetails { get; set; }
        public virtual DbSet<LedgerSumaryModel> LedgerSummarys { get; set; }
        public virtual DbSet<AcademicModel> Academics { get; set; }
        public virtual DbSet<SchoolModel> Schools { get; set; }
        public virtual DbSet<NotificationScheduleModel> NotificationSchedules { get; set; }

        public virtual DbSet<BlogTypeModel> BlogTypes { get; set; }
        public virtual DbSet<BlogModel> BLogModels { get; set; }
        public virtual DbSet<ClassModel> ClassModels { get; set; }
        public virtual DbSet<SectionModel> SectionModels { get; set; }
        public virtual DbSet<BranchModel> BranchModels { get; set; }

        public virtual DbSet<SubjectModel> SubjectModels { get; set; }
        public virtual DbSet<TCSSRelModel> TCSSRelModels { get; set; }

        public virtual DbSet<FeedbackModel> FeedbackModels { get; set; }
        public virtual DbSet<SettingsModel> Settingss { get; set; }


        public virtual DbSet<AcademicDetailModel> AcademicDetailModels { get; set; }
        public virtual DbSet<TermModel> TermModels { get; set; }
        public virtual DbSet<AssesmentModel> AssesmentModels { get; set; }
        public virtual DbSet<HouseModel> HouseModels { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }












        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MainDatabasesModel>()
                  .ToTable("ta_MainDatabases");

            //modelBuilder.Entity<ClientDetailModel>()
            //      .ToTable("ta_ClientDetails");

            //modelBuilder.Entity<PageModel>()
            //      .ToTable("ta_Pages");

            //modelBuilder.Entity<MenuModel>()
            //     .ToTable("ta_Menus");

            //modelBuilder.Entity<LayoutModel>()
            //   .ToTable("ta_Layout");

            //modelBuilder.Entity<MenuItemModel>()
            //  .ToTable("ta_MenuItem");

            modelBuilder.Entity<AlbumModel>()
                 .ToTable("ta_Albums");

            modelBuilder.Entity<ImageGalleryModel>()
                 .ToTable("ta_ImageGallerys");

            //modelBuilder.Entity<SiteModel>()
            //     .ToTable("ta_Sites");

            //modelBuilder.Entity<NotificationModel>()
            //    .ToTable("ta_Notifications");



            modelBuilder.Entity<StudentModel>()
                .ToTable("ta_d_Student_Master")
                .Property(e=>e.StudentModelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                ;
            modelBuilder.Entity<AttendanceModel>()
               .ToTable("ta_d_Attendance");
            modelBuilder.Entity<LedgerDetailModel>()
               .ToTable("ta_d_Ledger_Detail");
            modelBuilder.Entity<LedgerSumaryModel>()
               .ToTable("ta_d_Ledger_Summary");
            modelBuilder.Entity<AcademicModel>()
               .ToTable("ta_d_Academic");
            modelBuilder.Entity<SchoolModel>()
              .ToTable("ta_d_Schools");
            modelBuilder.Entity<NotificationScheduleModel>()
            .ToTable("NotificationSchedules");

            modelBuilder.Entity<BlogTypeModel>()
                 .ToTable("ta_BlogTypes");

            modelBuilder.Entity<BlogModel>()
                .ToTable("ta_Blogs");

            modelBuilder.Entity<ClassModel>()
             .ToTable("ta_d_m_Classes")
              .Property(e => e.ClassModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<SubjectModel>()
            .ToTable("ta_d_m_Subjects").Property(e => e.SubjectModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<SectionModel>()
            .ToTable("ta_d_m_Sections").Property(e => e.SectionModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<BranchModel>()
         .ToTable("ta_d_m_Branchs").Property(e => e.BranchModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<TCSSRelModel>()
            .ToTable("TCSSRel");

            modelBuilder.Entity<FeedbackModel>()
           .ToTable("Feedbacks");

            modelBuilder.Entity<SettingsModel>()
                 .ToTable("ta_Settingss");

            modelBuilder.Entity<AcademicDetailModel>()
                .ToTable("ta_AcademicDetail");
            modelBuilder.Entity<TermModel>()
                            .ToTable("ta_TermMaster").Property(e => e.TermModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<AssesmentModel>()
                            .ToTable("ta_AssesmentMaster").Property(e => e.AssesmentModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<HouseModel>()
                            .ToTable("ta_HouseMaster").Property(e => e.HouseModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<VehicleModel>()
                            .ToTable("ta_Vehicle").Property(e => e.VehicleModelid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}

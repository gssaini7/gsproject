namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ta_d_Academic",
                c => new
                    {
                        AcademicModelID = c.Int(nullable: false, identity: true),
                        strTotalMarks = c.String(),
                        strTotalGrade = c.String(),
                        StudentModelID = c.Int(nullable: false),
                        TermModelid = c.Int(nullable: false),
                        SubjectModelid = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AcademicModelID)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Subjects", t => t.SubjectModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_TermMaster", t => t.TermModelid, cascadeDelete: true)
                .Index(t => t.StudentModelID)
                .Index(t => t.TermModelid)
                .Index(t => t.SubjectModelid);
            
            CreateTable(
                "dbo.ta_d_Student_Master",
                c => new
                    {
                        StudentModelID = c.Int(nullable: false),
                        strAdmissionNo = c.String(),
                        strAcNo = c.String(),
                        strStudentName = c.String(),
                        strFatherName = c.String(),
                        strMotherName = c.String(),
                        strClass = c.Int(nullable: false),
                        strAddress = c.String(),
                        strSex = c.String(),
                        numMobileNoForSms = c.String(),
                        SectionModelid = c.Int(nullable: false),
                        BranchModelid = c.Int(nullable: false),
                        strRollNo = c.String(),
                        HouseModelid = c.Int(nullable: false),
                        dob_student = c.DateTime(nullable: false),
                        dob_father = c.DateTime(nullable: false),
                        dob_mother = c.DateTime(nullable: false),
                        dob_anniversary = c.DateTime(nullable: false),
                        VehicleModelid = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.StudentModelID)
                .ForeignKey("dbo.ta_d_m_Branchs", t => t.BranchModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Classes", t => t.strClass, cascadeDelete: true)
                .ForeignKey("dbo.ta_HouseMaster", t => t.HouseModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Sections", t => t.SectionModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_Vehicle", t => t.VehicleModelid, cascadeDelete: true)
                .Index(t => t.strClass)
                .Index(t => t.SectionModelid)
                .Index(t => t.BranchModelid)
                .Index(t => t.HouseModelid)
                .Index(t => t.VehicleModelid);
            
            CreateTable(
                "dbo.ta_d_m_Branchs",
                c => new
                    {
                        BranchModelid = c.Int(nullable: false),
                        BranchName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.BranchModelid);
            
            CreateTable(
                "dbo.ta_d_m_Classes",
                c => new
                    {
                        ClassModelid = c.Int(nullable: false),
                        ClassName = c.String(),
                        ClassDetail = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.ClassModelid);
            
            CreateTable(
                "dbo.ta_HouseMaster",
                c => new
                    {
                        HouseModelid = c.Int(nullable: false),
                        HouseName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.HouseModelid);
            
            CreateTable(
                "dbo.ta_d_m_Sections",
                c => new
                    {
                        SectionModelid = c.Int(nullable: false),
                        SectionName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.SectionModelid);
            
            CreateTable(
                "dbo.ta_Vehicle",
                c => new
                    {
                        VehicleModelid = c.Int(nullable: false),
                        VehicleNumber = c.String(),
                        VehicleType = c.String(),
                        DriverName = c.String(),
                        ContactNumber = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.VehicleModelid);
            
            CreateTable(
                "dbo.ta_d_m_Subjects",
                c => new
                    {
                        SubjectModelid = c.Int(nullable: false),
                        SubjectName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.SubjectModelid);
            
            CreateTable(
                "dbo.ta_TermMaster",
                c => new
                    {
                        TermModelid = c.Int(nullable: false),
                        TermName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.TermModelid);
            
            CreateTable(
                "dbo.ta_AcademicSummary",
                c => new
                    {
                        AcademicSummaryModelid = c.Int(nullable: false, identity: true),
                        strMarks = c.String(),
                        strGrade = c.String(),
                        StudentModelID = c.Int(nullable: false),
                        TermModelid = c.Int(nullable: false),
                        SubjectModelid = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AcademicSummaryModelid)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Subjects", t => t.SubjectModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_TermMaster", t => t.TermModelid, cascadeDelete: true)
                .Index(t => t.StudentModelID)
                .Index(t => t.TermModelid)
                .Index(t => t.SubjectModelid);
            
            CreateTable(
                "dbo.ta_Albums",
                c => new
                    {
                        AlbumModelid = c.Guid(nullable: false),
                        AlbumName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AlbumModelid);
            
            CreateTable(
                "dbo.ta_AssesmentMaster",
                c => new
                    {
                        AssesmentModelid = c.Int(nullable: false),
                        AssesmentName = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AssesmentModelid);
            
            CreateTable(
                "dbo.ta_d_Attendance",
                c => new
                    {
                        AttendanceModelID = c.Int(nullable: false, identity: true),
                        attendancedate = c.DateTime(nullable: false),
                        status = c.String(),
                        StudentModelID = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AttendanceModelID)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .Index(t => t.StudentModelID);
            
            CreateTable(
                "dbo.ta_Blogs",
                c => new
                    {
                        BlogModelid = c.Guid(nullable: false),
                        title = c.String(),
                        description = c.String(),
                        imagepath = c.String(),
                        filetype = c.String(),
                        BlogSubType = c.String(),
                        BlogTypeModelid = c.Guid(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.BlogModelid)
                .ForeignKey("dbo.ta_BlogTypes", t => t.BlogTypeModelid, cascadeDelete: true)
                .Index(t => t.BlogTypeModelid);
            
            CreateTable(
                "dbo.ta_BlogTypes",
                c => new
                    {
                        BlogTypeModelid = c.Guid(nullable: false),
                        BlogTypeName = c.String(),
                        BlogTypeDisplayName = c.String(),
                        BlogTypeProperty = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.BlogTypeModelid);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackModelid = c.Guid(nullable: false),
                        FeedbackType = c.String(),
                        StudentModelID = c.Int(nullable: false),
                        FeedbackDate = c.DateTime(nullable: false),
                        FeedbackOpenDate = c.DateTime(nullable: false),
                        FeedbackMessage = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.FeedbackModelid)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .Index(t => t.StudentModelID);
            
            CreateTable(
                "dbo.ta_ImageGallerys",
                c => new
                    {
                        ImageGalleryModelid = c.Guid(nullable: false),
                        ImageName = c.String(),
                        AlbumModelid = c.Guid(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.ImageGalleryModelid)
                .ForeignKey("dbo.ta_Albums", t => t.AlbumModelid, cascadeDelete: true)
                .Index(t => t.AlbumModelid);
            
            CreateTable(
                "dbo.ta_d_Ledger_Detail",
                c => new
                    {
                        LedgerDetailModelID = c.Int(nullable: false, identity: true),
                        strPeriod = c.String(),
                        numTotalDue = c.Decimal(nullable: false, storeType: "money"),
                        numTotalPaid = c.Decimal(nullable: false, storeType: "money"),
                        numTotalBalance = c.Decimal(nullable: false, storeType: "money"),
                        StudentModelID = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.LedgerDetailModelID)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .Index(t => t.StudentModelID);
            
            CreateTable(
                "dbo.ta_d_Ledger_Summary",
                c => new
                    {
                        LedgerSumaryModelID = c.Int(nullable: false, identity: true),
                        TotalDue = c.Decimal(nullable: false, storeType: "money"),
                        TotalPaid = c.Decimal(nullable: false, storeType: "money"),
                        TotalBalance = c.Decimal(nullable: false, storeType: "money"),
                        StudentModelID = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.LedgerSumaryModelID)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .Index(t => t.StudentModelID);
            
            CreateTable(
                "dbo.ta_MainDatabases",
                c => new
                    {
                        MainDatabasesModelid = c.Guid(nullable: false),
                        IDataSource = c.String(),
                        IInitialCatalog = c.String(),
                        IUsername = c.String(),
                        IPassword = c.String(),
                        IName = c.String(),
                        IUIDCode = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.MainDatabasesModelid);
            
            CreateTable(
                "dbo.NotificationSchedules",
                c => new
                    {
                        NotificationScheduleModelid = c.Guid(nullable: false),
                        notificationsentdate = c.DateTime(nullable: false),
                        classname = c.String(),
                        BlogModelid = c.Guid(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.NotificationScheduleModelid)
                .ForeignKey("dbo.ta_Blogs", t => t.BlogModelid, cascadeDelete: true)
                .Index(t => t.BlogModelid);
            
            CreateTable(
                "dbo.ta_d_Schools",
                c => new
                    {
                        SchoolModelID = c.Int(nullable: false, identity: true),
                        strSchoolName = c.String(),
                        strAddress = c.String(),
                        strDistrict = c.String(),
                        strState = c.String(),
                        strAffiliatedTo = c.String(),
                        strAffiliationNo = c.String(),
                        strEmail = c.String(),
                        strWebsite = c.String(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.SchoolModelID);
            
            CreateTable(
                "dbo.ta_Settingss",
                c => new
                    {
                        SettingsModelid = c.Guid(nullable: false),
                        SettingsType = c.String(),
                        SettingsContent = c.String(),
                    })
                .PrimaryKey(t => t.SettingsModelid);
            
            CreateTable(
                "dbo.TCSSRel",
                c => new
                    {
                        TCSSRelModelid = c.Guid(nullable: false),
                        Teacherid = c.Guid(nullable: false),
                        ClassModelid = c.Int(nullable: false),
                        SectionModelid = c.Int(nullable: false),
                        SubjectModelid = c.Int(nullable: false),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.TCSSRelModelid)
                .ForeignKey("dbo.ta_d_m_Classes", t => t.ClassModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Sections", t => t.SectionModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Subjects", t => t.SubjectModelid, cascadeDelete: true)
                .Index(t => t.ClassModelid)
                .Index(t => t.SectionModelid)
                .Index(t => t.SubjectModelid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TCSSRel", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.TCSSRel", "SectionModelid", "dbo.ta_d_m_Sections");
            DropForeignKey("dbo.TCSSRel", "ClassModelid", "dbo.ta_d_m_Classes");
            DropForeignKey("dbo.NotificationSchedules", "BlogModelid", "dbo.ta_Blogs");
            DropForeignKey("dbo.ta_d_Ledger_Summary", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_d_Ledger_Detail", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_ImageGallerys", "AlbumModelid", "dbo.ta_Albums");
            DropForeignKey("dbo.Feedbacks", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_Blogs", "BlogTypeModelid", "dbo.ta_BlogTypes");
            DropForeignKey("dbo.ta_d_Attendance", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_AcademicSummary", "TermModelid", "dbo.ta_TermMaster");
            DropForeignKey("dbo.ta_AcademicSummary", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.ta_AcademicSummary", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_d_Academic", "TermModelid", "dbo.ta_TermMaster");
            DropForeignKey("dbo.ta_d_Academic", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.ta_d_Academic", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_d_Student_Master", "VehicleModelid", "dbo.ta_Vehicle");
            DropForeignKey("dbo.ta_d_Student_Master", "SectionModelid", "dbo.ta_d_m_Sections");
            DropForeignKey("dbo.ta_d_Student_Master", "HouseModelid", "dbo.ta_HouseMaster");
            DropForeignKey("dbo.ta_d_Student_Master", "strClass", "dbo.ta_d_m_Classes");
            DropForeignKey("dbo.ta_d_Student_Master", "BranchModelid", "dbo.ta_d_m_Branchs");
            DropIndex("dbo.TCSSRel", new[] { "SubjectModelid" });
            DropIndex("dbo.TCSSRel", new[] { "SectionModelid" });
            DropIndex("dbo.TCSSRel", new[] { "ClassModelid" });
            DropIndex("dbo.NotificationSchedules", new[] { "BlogModelid" });
            DropIndex("dbo.ta_d_Ledger_Summary", new[] { "StudentModelID" });
            DropIndex("dbo.ta_d_Ledger_Detail", new[] { "StudentModelID" });
            DropIndex("dbo.ta_ImageGallerys", new[] { "AlbumModelid" });
            DropIndex("dbo.Feedbacks", new[] { "StudentModelID" });
            DropIndex("dbo.ta_Blogs", new[] { "BlogTypeModelid" });
            DropIndex("dbo.ta_d_Attendance", new[] { "StudentModelID" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "SubjectModelid" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "TermModelid" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "StudentModelID" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "VehicleModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "HouseModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "BranchModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "SectionModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "strClass" });
            DropIndex("dbo.ta_d_Academic", new[] { "SubjectModelid" });
            DropIndex("dbo.ta_d_Academic", new[] { "TermModelid" });
            DropIndex("dbo.ta_d_Academic", new[] { "StudentModelID" });
            DropTable("dbo.TCSSRel");
            DropTable("dbo.ta_Settingss");
            DropTable("dbo.ta_d_Schools");
            DropTable("dbo.NotificationSchedules");
            DropTable("dbo.ta_MainDatabases");
            DropTable("dbo.ta_d_Ledger_Summary");
            DropTable("dbo.ta_d_Ledger_Detail");
            DropTable("dbo.ta_ImageGallerys");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.ta_BlogTypes");
            DropTable("dbo.ta_Blogs");
            DropTable("dbo.ta_d_Attendance");
            DropTable("dbo.ta_AssesmentMaster");
            DropTable("dbo.ta_Albums");
            DropTable("dbo.ta_AcademicSummary");
            DropTable("dbo.ta_TermMaster");
            DropTable("dbo.ta_d_m_Subjects");
            DropTable("dbo.ta_Vehicle");
            DropTable("dbo.ta_d_m_Sections");
            DropTable("dbo.ta_HouseMaster");
            DropTable("dbo.ta_d_m_Classes");
            DropTable("dbo.ta_d_m_Branchs");
            DropTable("dbo.ta_d_Student_Master");
            DropTable("dbo.ta_d_Academic");
        }
    }
}

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class academicdetailrenamed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ta_AcademicSummary", "StudentModelID", "dbo.ta_d_Student_Master");
            DropForeignKey("dbo.ta_AcademicSummary", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.ta_AcademicSummary", "TermModelid", "dbo.ta_TermMaster");
            DropForeignKey("dbo.ta_AcademicSummary", "AssesmentModelid", "dbo.ta_AssesmentMaster");
            DropIndex("dbo.ta_AcademicSummary", new[] { "StudentModelID" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "TermModelid" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "SubjectModelid" });
            DropIndex("dbo.ta_AcademicSummary", new[] { "AssesmentModelid" });
            CreateTable(
                "dbo.ta_AcademicDetail",
                c => new
                    {
                        AcademicDetailModelid = c.Int(nullable: false, identity: true),
                        strMarks = c.String(),
                        strGrade = c.String(),
                        StudentModelID = c.Int(nullable: false),
                        TermModelid = c.Int(nullable: false),
                        SubjectModelid = c.Int(nullable: false),
                        AssesmentModelid = c.Int(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AcademicDetailModelid)
                .ForeignKey("dbo.ta_d_Student_Master", t => t.StudentModelID, cascadeDelete: true)
                .ForeignKey("dbo.ta_d_m_Subjects", t => t.SubjectModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_TermMaster", t => t.TermModelid, cascadeDelete: true)
                .ForeignKey("dbo.ta_AssesmentMaster", t => t.AssesmentModelid)
                .Index(t => t.StudentModelID)
                .Index(t => t.TermModelid)
                .Index(t => t.SubjectModelid)
                .Index(t => t.AssesmentModelid);
            
            DropTable("dbo.ta_AcademicSummary");
        }
        
        public override void Down()
        {
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
                        AssesmentModelid = c.Int(),
                        createdate = c.DateTime(),
                        LastUpdatedate = c.DateTime(),
                        isPublished = c.Boolean(),
                        createdBy = c.Guid(),
                        createdByName = c.String(),
                        remarks = c.String(),
                        istrash = c.Boolean(),
                    })
                .PrimaryKey(t => t.AcademicSummaryModelid);
            
            DropForeignKey("dbo.ta_AcademicDetail", "AssesmentModelid", "dbo.ta_AssesmentMaster");
            DropForeignKey("dbo.ta_AcademicDetail", "TermModelid", "dbo.ta_TermMaster");
            DropForeignKey("dbo.ta_AcademicDetail", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.ta_AcademicDetail", "StudentModelID", "dbo.ta_d_Student_Master");
            DropIndex("dbo.ta_AcademicDetail", new[] { "AssesmentModelid" });
            DropIndex("dbo.ta_AcademicDetail", new[] { "SubjectModelid" });
            DropIndex("dbo.ta_AcademicDetail", new[] { "TermModelid" });
            DropIndex("dbo.ta_AcademicDetail", new[] { "StudentModelID" });
            DropTable("dbo.ta_AcademicDetail");
            CreateIndex("dbo.ta_AcademicSummary", "AssesmentModelid");
            CreateIndex("dbo.ta_AcademicSummary", "SubjectModelid");
            CreateIndex("dbo.ta_AcademicSummary", "TermModelid");
            CreateIndex("dbo.ta_AcademicSummary", "StudentModelID");
            AddForeignKey("dbo.ta_AcademicSummary", "AssesmentModelid", "dbo.ta_AssesmentMaster", "AssesmentModelid");
            AddForeignKey("dbo.ta_AcademicSummary", "TermModelid", "dbo.ta_TermMaster", "TermModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_AcademicSummary", "SubjectModelid", "dbo.ta_d_m_Subjects", "SubjectModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_AcademicSummary", "StudentModelID", "dbo.ta_d_Student_Master", "StudentModelID", cascadeDelete: true);
        }
    }
}

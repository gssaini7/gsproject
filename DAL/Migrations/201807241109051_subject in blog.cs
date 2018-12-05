namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectinblog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ta_Blogs", "SubjectModelid", c => c.Int());
            CreateIndex("dbo.ta_Blogs", "SubjectModelid");
            AddForeignKey("dbo.ta_Blogs", "SubjectModelid", "dbo.ta_d_m_Subjects", "SubjectModelid");
            DropColumn("dbo.ta_Blogs", "BlogSubType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ta_Blogs", "BlogSubType", c => c.String());
            DropForeignKey("dbo.ta_Blogs", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropIndex("dbo.ta_Blogs", new[] { "SubjectModelid" });
            DropColumn("dbo.ta_Blogs", "SubjectModelid");
        }
    }
}

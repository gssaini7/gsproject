namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowednullvalues : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ta_d_Student_Master", "BranchModelid", "dbo.ta_d_m_Branchs");
            DropForeignKey("dbo.ta_d_Student_Master", "HouseModelid", "dbo.ta_HouseMaster");
            DropForeignKey("dbo.ta_d_Student_Master", "SectionModelid", "dbo.ta_d_m_Sections");
            DropForeignKey("dbo.ta_d_Student_Master", "VehicleModelid", "dbo.ta_Vehicle");
            DropForeignKey("dbo.TCSSRel", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropIndex("dbo.ta_d_Student_Master", new[] { "SectionModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "BranchModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "HouseModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "VehicleModelid" });
            DropIndex("dbo.TCSSRel", new[] { "SubjectModelid" });
            AlterColumn("dbo.ta_d_Student_Master", "SectionModelid", c => c.Int());
            AlterColumn("dbo.ta_d_Student_Master", "BranchModelid", c => c.Int());
            AlterColumn("dbo.ta_d_Student_Master", "HouseModelid", c => c.Int());
            AlterColumn("dbo.ta_d_Student_Master", "VehicleModelid", c => c.Int());
            AlterColumn("dbo.TCSSRel", "SubjectModelid", c => c.Int());
            CreateIndex("dbo.ta_d_Student_Master", "SectionModelid");
            CreateIndex("dbo.ta_d_Student_Master", "BranchModelid");
            CreateIndex("dbo.ta_d_Student_Master", "HouseModelid");
            CreateIndex("dbo.ta_d_Student_Master", "VehicleModelid");
            CreateIndex("dbo.TCSSRel", "SubjectModelid");
            AddForeignKey("dbo.ta_d_Student_Master", "BranchModelid", "dbo.ta_d_m_Branchs", "BranchModelid");
            AddForeignKey("dbo.ta_d_Student_Master", "HouseModelid", "dbo.ta_HouseMaster", "HouseModelid");
            AddForeignKey("dbo.ta_d_Student_Master", "SectionModelid", "dbo.ta_d_m_Sections", "SectionModelid");
            AddForeignKey("dbo.ta_d_Student_Master", "VehicleModelid", "dbo.ta_Vehicle", "VehicleModelid");
            AddForeignKey("dbo.TCSSRel", "SubjectModelid", "dbo.ta_d_m_Subjects", "SubjectModelid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TCSSRel", "SubjectModelid", "dbo.ta_d_m_Subjects");
            DropForeignKey("dbo.ta_d_Student_Master", "VehicleModelid", "dbo.ta_Vehicle");
            DropForeignKey("dbo.ta_d_Student_Master", "SectionModelid", "dbo.ta_d_m_Sections");
            DropForeignKey("dbo.ta_d_Student_Master", "HouseModelid", "dbo.ta_HouseMaster");
            DropForeignKey("dbo.ta_d_Student_Master", "BranchModelid", "dbo.ta_d_m_Branchs");
            DropIndex("dbo.TCSSRel", new[] { "SubjectModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "VehicleModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "HouseModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "BranchModelid" });
            DropIndex("dbo.ta_d_Student_Master", new[] { "SectionModelid" });
            AlterColumn("dbo.TCSSRel", "SubjectModelid", c => c.Int(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "VehicleModelid", c => c.Int(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "HouseModelid", c => c.Int(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "BranchModelid", c => c.Int(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "SectionModelid", c => c.Int(nullable: false));
            CreateIndex("dbo.TCSSRel", "SubjectModelid");
            CreateIndex("dbo.ta_d_Student_Master", "VehicleModelid");
            CreateIndex("dbo.ta_d_Student_Master", "HouseModelid");
            CreateIndex("dbo.ta_d_Student_Master", "BranchModelid");
            CreateIndex("dbo.ta_d_Student_Master", "SectionModelid");
            AddForeignKey("dbo.TCSSRel", "SubjectModelid", "dbo.ta_d_m_Subjects", "SubjectModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_d_Student_Master", "VehicleModelid", "dbo.ta_Vehicle", "VehicleModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_d_Student_Master", "SectionModelid", "dbo.ta_d_m_Sections", "SectionModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_d_Student_Master", "HouseModelid", "dbo.ta_HouseMaster", "HouseModelid", cascadeDelete: true);
            AddForeignKey("dbo.ta_d_Student_Master", "BranchModelid", "dbo.ta_d_m_Branchs", "BranchModelid", cascadeDelete: true);
        }
    }
}

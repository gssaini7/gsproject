namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentemaildistrictpin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ta_d_Student_Master", "strDistrict", c => c.String());
            AddColumn("dbo.ta_d_Student_Master", "strZipCode", c => c.String());
            AddColumn("dbo.ta_d_Student_Master", "strEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ta_d_Student_Master", "strEmail");
            DropColumn("dbo.ta_d_Student_Master", "strZipCode");
            DropColumn("dbo.ta_d_Student_Master", "strDistrict");
        }
    }
}

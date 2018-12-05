namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ta_d_Student_Master", "dob_student", c => c.DateTime());
            AlterColumn("dbo.ta_d_Student_Master", "dob_father", c => c.DateTime());
            AlterColumn("dbo.ta_d_Student_Master", "dob_mother", c => c.DateTime());
            AlterColumn("dbo.ta_d_Student_Master", "dob_anniversary", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ta_d_Student_Master", "dob_anniversary", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "dob_mother", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "dob_father", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ta_d_Student_Master", "dob_student", c => c.DateTime(nullable: false));
        }
    }
}

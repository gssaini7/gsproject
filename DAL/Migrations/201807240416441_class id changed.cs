namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classidchanged : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ta_d_Student_Master", name: "strClass", newName: "ClassModelid");
            RenameIndex(table: "dbo.ta_d_Student_Master", name: "IX_strClass", newName: "IX_ClassModelid");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ta_d_Student_Master", name: "IX_ClassModelid", newName: "IX_strClass");
            RenameColumn(table: "dbo.ta_d_Student_Master", name: "ClassModelid", newName: "strClass");
        }
    }
}

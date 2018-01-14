namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeincomeDatedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblIncomes", "incomeDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.tblIncomes", "incomeMonth");
            DropColumn("dbo.tblIncomes", "incomeYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblIncomes", "incomeYear", c => c.Int(nullable: false));
            AddColumn("dbo.tblIncomes", "incomeMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.tblIncomes", "incomeDate", c => c.Int(nullable: false));
        }
    }
}

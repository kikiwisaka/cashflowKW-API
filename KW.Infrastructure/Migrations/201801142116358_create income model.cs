namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createincomemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblIncomes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        incomeName = c.String(),
                        definition = c.String(),
                        incomeDate = c.Int(nullable: false),
                        incomeMonth = c.Int(nullable: false),
                        incomeYear = c.Int(nullable: false),
                        createdBy = c.Int(),
                        createdDate = c.DateTime(),
                        updatedBy = c.Int(),
                        updatedDate = c.DateTime(),
                        isDeleted = c.Boolean(),
                        budgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblBudgets", t => t.budgetId)
                .Index(t => t.budgetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblIncomes", "budgetId", "dbo.tblBudgets");
            DropIndex("dbo.tblIncomes", new[] { "budgetId" });
            DropTable("dbo.tblIncomes");
        }
    }
}

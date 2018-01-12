namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createBudgetdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBudgets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        budgetName = c.String(),
                        definition = c.String(),
                        createdBy = c.Int(),
                        createdDate = c.DateTime(),
                        updatedBy = c.Int(),
                        updatedDate = c.DateTime(),
                        isDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
        }
        
        public override void Down()
        {
            DropTable("dbo.tblBudgets");
        }
    }
}

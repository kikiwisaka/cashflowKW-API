namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createExpenditureandExpenditureDetailtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblExpenditureDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expenditureName = c.String(),
                        expenditureDefinition = c.String(),
                        price = c.Double(nullable: false),
                        createdBy = c.Int(),
                        createdDate = c.DateTime(),
                        updatedBy = c.Int(),
                        updatedDate = c.DateTime(),
                        isDeleted = c.Boolean(),
                        expenditureId = c.Int(nullable: false),
                        budgetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblBudgets", t => t.budgetId)
                .Index(t => t.budgetId);
            
            CreateTable(
                "dbo.tblExpenditures",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        expenditureDate = c.DateTime(nullable: false),
                        total = c.Double(nullable: false),
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
            DropForeignKey("dbo.tblExpenditureDetails", "budgetId", "dbo.tblBudgets");
            DropIndex("dbo.tblExpenditureDetails", new[] { "budgetId" });
            DropTable("dbo.tblExpenditures");
            DropTable("dbo.tblExpenditureDetails");
        }
    }
}

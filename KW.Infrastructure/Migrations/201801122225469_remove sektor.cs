namespace KW.Infrastructure
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removesektor : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tblSektors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblSektors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        namaSektor = c.String(),
                        minimum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        maximum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        definisi = c.String(),
                        createBy = c.Int(),
                        createDate = c.DateTime(),
                        updateBy = c.Int(),
                        updateDate = c.DateTime(),
                        isDelete = c.Boolean(),
                        deleteDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}

namespace Fakultet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredAttributeOnJMBGNastavnika : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik");
            DropIndex("dbo.Predmet", new[] { "JMBGNastavnika" });
            AlterColumn("dbo.Predmet", "JMBGNastavnika", c => c.String(nullable: true, maxLength: 13));
            CreateIndex("dbo.Predmet", "JMBGNastavnika");
            AddForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik", "JMBG");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik");
            DropIndex("dbo.Predmet", new[] { "JMBGNastavnika" });
            AlterColumn("dbo.Predmet", "JMBGNastavnika", c => c.String(nullable: false, maxLength: 13));
            CreateIndex("dbo.Predmet", "JMBGNastavnika");
            AddForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik", "JMBG", cascadeDelete: true);
        }
    }
}

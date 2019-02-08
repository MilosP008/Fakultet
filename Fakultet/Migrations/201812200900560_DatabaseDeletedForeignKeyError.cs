namespace Fakultet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseDeletedForeignKeyError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nastavnik", "Predmet_Naziv", "dbo.Predmet");
            DropIndex("dbo.Nastavnik", new[] { "Predmet_Naziv" });
            CreateIndex("dbo.Predmet", "JMBGNastavnika");
            AddForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik", "JMBG", cascadeDelete: true);
            DropColumn("dbo.Nastavnik", "Predmet_Naziv");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nastavnik", "Predmet_Naziv", c => c.String(maxLength: 30));
            DropForeignKey("dbo.Predmet", "JMBGNastavnika", "dbo.Nastavnik");
            DropIndex("dbo.Predmet", new[] { "JMBGNastavnika" });
            CreateIndex("dbo.Nastavnik", "Predmet_Naziv");
            AddForeignKey("dbo.Nastavnik", "Predmet_Naziv", "dbo.Predmet", "Naziv");
        }
    }
}

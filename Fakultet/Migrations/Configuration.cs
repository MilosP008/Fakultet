namespace Fakultet.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Fakultet.DatabaseContext;
    using Fakultet.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Fakultet.DatabaseContext.FakultetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Fakultet.DatabaseContext.FakultetContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var nastavnici = new List<Nastavnik>
            {
                new Nastavnik { JMBG = "1234567891000", Ime = "Marko", Prezime = "Nikolic", DatumZaposljavanja = DateTime.Parse("2002-09-21") },
                new Nastavnik { JMBG = "1234567892000", Ime = "Nikola", Prezime = "Savic", DatumZaposljavanja = DateTime.Parse("1989-05-03") },
                new Nastavnik { JMBG = "1234567893000", Ime = "Vuk", Prezime = "Obrenovic", DatumZaposljavanja = DateTime.Parse("2003-03-13") },
                new Nastavnik { JMBG = "1234567894000", Ime = "Nenad", Prezime = "Andrijevic", DatumZaposljavanja = DateTime.Parse("2012-12-22") },
                new Nastavnik { JMBG = "1234567895000", Ime = "Nebojsa", Prezime = "Milosavljevic", DatumZaposljavanja = DateTime.Parse("1997-11-21") }
            };
            nastavnici.ForEach(n => context.Nastavnik.AddOrUpdate(p => p.JMBG, n));
            context.SaveChanges();

            var studenti = new List<Student> {
                new Student { JMBG = "1234567890000", Ime = "Petar", Prezime = "Petrovic", Godine = 18 },
                new Student { JMBG = "1234567890001", Ime = "Uros", Prezime = "Urosevic", Godine = 20 },
                new Student { JMBG = "1234567890002", Ime = "Milan", Prezime = "Ugrenovic", Godine = 20 },
                new Student { JMBG = "1234567890003", Ime = "Aleksandar", Prezime = "Kosic", Godine = 19 },
                new Student { JMBG = "1234567890004", Ime = "Milos", Prezime = "Knezevic", Godine = 18 }
            };
            studenti.ForEach(s => context.Student.AddOrUpdate(p => p.JMBG, s));
            context.SaveChanges();

            var predmeti = new List<Predmet>
            {
                new Predmet { Naziv = "Matematika", Cena = 8000, JMBGNastavnika = "1234567891000", Studenti = new List<Student>() },
                new Predmet { Naziv = "Fizika", Cena = 7000, JMBGNastavnika = "1234567892000", Studenti = new List<Student>() },
                new Predmet { Naziv = "Hemija", Cena = 9000, JMBGNastavnika = "1234567893000", Studenti = new List<Student>() },
                new Predmet { Naziv = "Biologija", Cena = 12000, JMBGNastavnika = "1234567894000", Studenti = new List<Student>() },
                new Predmet { Naziv = "Medicina", Cena = 16000, JMBGNastavnika = "1234567895000", Studenti = new List<Student>() }
            };
            predmeti.ForEach(r => context.Predmet.AddOrUpdate(p => p.Naziv, r));
            context.SaveChanges();

            DodajStudenta(context, "Matematika", "1234567890000");
            DodajStudenta(context, "Matematika", "1234567890001");
            DodajStudenta(context, "Fizika", "1234567890000");
            DodajStudenta(context, "Hemija", "1234567890002");
            DodajStudenta(context, "Biologija", "1234567890004");
            DodajStudenta(context, "Biologija", "1234567890003");
            DodajStudenta(context, "Medicina", "1234567890001");
        }

        void DodajStudenta(FakultetContext context, string naziv, string JMBG)
        {
            var predmet = context.Predmet.SingleOrDefault(p => p.Naziv == naziv);
            var student = context.Student.SingleOrDefault(s => s.JMBG == JMBG);
            if(student == null)
            {
                predmet.Studenti.Add(context.Student.Single(s => s.JMBG == JMBG));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Fakultet.Models;

namespace Fakultet.DatabaseContext
{
    public class FakultetContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
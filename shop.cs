using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace project
{
    public class ShopContext : DbContext
    {
        public DbSet<gemeente> gemeentes { get; set; }
        public DbSet<registratie> registraties { get; set; }
        public DbSet<karper> karpers { get; set; }
        public DbSet<water> waters { get; set; }
        public DbSet<vangst> vangsten { get; set; }
        public DbSet<gebruiker> gebruikers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=project.db");
    }
}

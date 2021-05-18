using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace eindwerk_ontwikkelingomgeving
{
    public class ShopContext : DbContext
    {
        public DbSet<speler> spelers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=spelers.db");
    }
}

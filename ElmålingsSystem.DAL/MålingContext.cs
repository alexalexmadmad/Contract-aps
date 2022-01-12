using ElmålingsSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmålingsSystem.DAL
{
    public class MålingContext : DbContext
    {
        public DbSet<EjerKunde> EjerKunder { get; set; }
        public DbSet<Installation> Installationer { get; set; }
        public DbSet<LejerKunde> LejerKunder { get; set; }
        public DbSet <Måler> Måler { get; set; }
        public DbSet<Måleværdier> Måleværdier { get; set; }

        public MålingContext(DbContextOptions<MålingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

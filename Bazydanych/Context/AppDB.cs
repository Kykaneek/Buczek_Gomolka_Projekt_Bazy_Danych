using Bazydanych.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazydanych.Context
{
    public class AppDB: DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) :base(options) {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Trace> Traces{ get; set; }
        public DbSet<Location> Locations{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Contractor>().ToTable("Contractors");
            modelBuilder.Entity<Trace>().ToTable("Traces");
            modelBuilder.Entity<Location>().ToTable("Location");
        }
    }
}

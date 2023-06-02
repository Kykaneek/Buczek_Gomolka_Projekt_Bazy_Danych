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
        public DbSet<Location> Location{ get; set; }
        public DbSet<Car> Car{ get; set; }
        public DbSet<PlannedTrace> PlannedTraces { get; set; }
        public DbSet<Loading> Loading { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Contractor>().ToTable("Contractors");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Trace>().ToTable("Trace");
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<PlannedTrace>().ToTable("PlannedTraces");
            modelBuilder.Entity<Loading>().ToTable("loading");
        }
    }
}

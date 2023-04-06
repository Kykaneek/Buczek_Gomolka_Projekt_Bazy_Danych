using Bazydanych.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazydanych.Context
{
    public class AppDB: DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) :base(options) {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}

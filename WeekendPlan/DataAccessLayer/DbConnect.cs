using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.DataAccessLayer
{
    public class DbConnect : DbContext
    {
        public DbConnect() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            base.OnModelCreating(modelBuilder);
        }
    }
}
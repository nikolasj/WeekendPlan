using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WeekendPlan.Models;

namespace WeekendPlan.DataAccessLayer
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        //public DbSet<User> Users { get; set; }
    }

    public class DbConnect : DbContext
    {
        public DbConnect() : base("DefaultConnection") { }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentFilm> CommentFilms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("User");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Genres>().ToTable("Genres");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<CommentFilm>().ToTable("CommentFilm");

            base.OnModelCreating(modelBuilder);
        }
    }
}
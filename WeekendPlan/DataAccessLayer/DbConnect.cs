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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CommentFilm> CommentFilms { get; set; }
        public DbSet<CommentPlace> CommentPlaces { get; set; }
        public DbSet<CommentEvent> CommentEvents { get; set; }
        public DbSet<TagEvent> TagEvents { get; set; }
        public DbSet<TagPlace> TagPlaces { get; set; }
        public DbSet<TagFilm> TagFilms { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteOpportunity> RouteOpportunity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("User");
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Genres>().ToTable("Genres");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<CommentFilm>().ToTable("CommentFilm");
            modelBuilder.Entity<CommentPlace>().ToTable("CommentPlace");
            modelBuilder.Entity<CommentEvent>().ToTable("CommentEvent");
            modelBuilder.Entity<TagEvent>().ToTable("TagEvent");
            modelBuilder.Entity<TagPlace>().ToTable("TagPlace");
            modelBuilder.Entity<TagFilm>().ToTable("TagFilm");
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Place>().ToTable("Place");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Show>().ToTable("Show");
            modelBuilder.Entity<Opportunity>().ToTable("Opportunity");
            modelBuilder.Entity<Route>().ToTable("Route");
            modelBuilder.Entity<RouteOpportunity>().ToTable("RouteOpportunity");

            base.OnModelCreating(modelBuilder);
        }
    }
}
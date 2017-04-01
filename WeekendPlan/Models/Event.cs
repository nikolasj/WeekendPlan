using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Event
    {
        [Key]
        [Column("event_id")]
        public Int32 EventId { get; set; }
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }
        //[Column("dates")]
        //public DateRange[] Dates { get; set; }
        [Column("date_start")]
        public String DateStart { get; set; }
        // public DateTime DateSartDT { get { return DateTime.Parse(DateStart); } }
        [Column("date_end")]
        public String DateEnd { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("short_title")]
        public String ShortTitle { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("place")]
        public Int32 Place { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("body_text")]
        public String BodyText { get; set; }
        [Column("location")]
        public String Location { get; set; }
        [Column("categories")]
        public String Categories { get; set; }
        [Column("tagline")]
        public String Tagline { get; set; }
        [Column("age_restriction")]
        public String AgeRestriction { get; set; }
        [Column("price")]
        public String Price { get; set; }
        [Column("is_free")]
        public Boolean IsFree { get; set; }
        [Column("images")]
        public String Images { get; set; }
        [Column("favorites_count")]
        public Int32 FavoritesCount { get; set; }
        [Column("comments_count")]
        public Int32 CommentsCount { get; set; }
        [Column("site_url")]
        public String SiteUrl { get; set; }
        [Column("tags")]
        public String Tags { get; set; }
        [Column("participants")]
        public String Participants { get; set; }

        public List<Tag> TagStr { get; set; }
        public List<Comment> Comments { get; set; }

        public Event()
        {          
            this.Price = "0";
        }

        public Event(Event e)
        {
            this.AgeRestriction = e.AgeRestriction;
            this.DateEnd = e.DateEnd;
            this.DateStart = e.DateStart;
            this.Price = e.Price;            
        }

        public static List<Event> GetEventsForOpportunity(DateTime concreteDate, UserProfile user, List<Tag> tags = null, int? price = null,
            String location = null, String transport = null, int countPersons = 0, int countEvents = 1, bool allWeather = false)
        {
            DbConnect connector = new DbConnect();
            List<Event> events = connector.Events.Where(x => x.Location == user.Location).ToList();
            List<Event> resultEvents = new List<Event>();
            List<Event> locationEvents = new List<Event>();
            foreach (var e in events)
            {
                DateTime dateStart = DateTime.Parse(e.DateStart);
                DateTime dateEnd = DateTime.Parse(e.DateEnd);
                if (concreteDate >= dateStart && concreteDate <= dateEnd)
                {
                    locationEvents.Add(e);
                }
            }
            var tagsByUser = Tag.GetTagsByUser(user.UserId);

            foreach(var e in locationEvents)
            {
                foreach(var t in tagsByUser)
                {
                    if(e.Tags.Contains(t.Text)&&!resultEvents.Any(x=>x.EventId==e.EventId))
                    {
                        resultEvents.Add(e);
                    }
                }
            }
            
            return resultEvents.ToList();
        }

        public static List<Event> GetEvents()
        {
            DbConnect connector = new DbConnect();
            List<Event> events = connector.Events.ToList<Event>();

            return events;
        }

        public static List<Event> GetEventsByTag(int tagId)
        {
            DbConnect connector = new DbConnect();
            List<Event> events = connector.Events.ToList<Event>();
            //---
            List<int> tagEventById = connector.TagEvents.Where(x => x.TagId == tagId).Select(x => x.EventId).ToList();
            events = connector.Events.Where(t => tagEventById.Contains(t.EventId)).ToList();

            return events;
        }

        public List<Comment> GetComments()
        {
            List<Comment> eventComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<CommentEvent> commentsEventById = connector.CommentEvents.Where(x => x.EventId == this.EventId).ToList<CommentEvent>();
            foreach (var c in commentsEventById)
            {
                var comment = connector.Comments.FirstOrDefault(x => x.CommentId == c.CommentId);
                var userId = comment.UserId;
                comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    eventComments.Add(comment);
                }
            }
            Comments = eventComments;
            return eventComments;
        }

        public List<Tag> GetTags()
        {
            List<Tag> eventTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagEvent> tagsEventById = connector.TagEvents.Where(x => x.EventId == this.EventId).ToList<TagEvent>();
            foreach (var c in tagsEventById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId);
                var userId = comment.UserId;
                //comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    eventTags.Add(comment);
                }
            }
            TagStr = eventTags;
            return eventTags;
        }

        public List<Tag> GetTagsCommon()
        {
            List<Tag> placeTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagEvent> tagsPlaceById = connector.TagEvents.Where(x => x.EventId == this.EventId).ToList<TagEvent>();
            foreach (var c in tagsPlaceById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && (x.UserId == null || x.UserId == 0));

                if (comment != null)
                {
                    placeTags.Add(comment);
                }
            }
            TagStr = placeTags;
            return placeTags;
        }

        public List<Tag> GetTagsByUser(int userId)
        {
            List<Tag> eventTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagEvent> tagsPlaceById = connector.TagEvents.Where(x => x.EventId == this.EventId).ToList<TagEvent>();
            foreach (var c in tagsPlaceById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && x.UserId == userId);

                if (comment != null)
                {
                    eventTags.Add(comment);
                }
            }
            TagStr = eventTags;
            return eventTags;
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WeekendPlan.DataAccessLayer;

namespace WeekendPlan.Models
{
    public class Place
    {
        [Key]
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        [Column("id")]
        public Int32 Id { get; set; }
        [Column("title")]
        public String Title { get; set; }
        [Column("short_title")]
        public String ShortTitle { get; set; }
        [Column("slug")]
        public String Slug { get; set; }
        [Column("address")]
        public String Address { get; set; }
        [Column("location")]
        public String Location { get; set; }
        [Column("timetable")]
        public String Timetable { get; set; }
        [Column("phone")]
        public String Phone { get; set; }
        [Column("is_stub")]
        public Boolean? IsStub { get; set; }
        [Column("images")]
        public String Images { get; set; }
        [Column("description")]
        public String Description { get; set; }
        [Column("body_text")]
        public String BodyText { get; set; }
        [Column("site_url")]
        public String SiteUrl { get; set; }
        [Column("foreign_url")]
        public String ForeignUrl { get; set; }
        [Column("coords")]
        public String CoordsStr { get; set; }
        public Coords Coords { get {
                if (String.IsNullOrWhiteSpace(this.CoordsStr)) return new Coords() { Lat = "0", Lon = "0" };
                else
                {
                    var arr = CoordsStr.Split(new char[] { ';' });
                    return new Coords() { Lat = arr[0], Lon = arr[1]};
                }
            } }
        [Column("subway")]
        public String Subway { get; set; }
        [Column("favorites_count")]
        public Int32? FavoritesCount { get; set; }
        [Column("comments_count")]
        public Int32? CommentsCount { get; set; }
        [Column("is_closed")]
        public Boolean? IsClosed { get; set; }
        [Column("categories")]
        public String CategoriesKudaGo { get; set; }
        [Column("tags")]
        public String TagsKudaGo { get; set; }
        [Column("city_id")]
        public Int32 CityId { get; set; }

        public List<City> Cities { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }

        public static List<Place> GetPlacesForOpportunity(DateTime concreteDate, UserProfile user, List<Tag> tags = null, int? price = null,
            String location = null, String transport = null, int countPersons = 0, bool allWeather = false)
        {
            DbConnect connector = new DbConnect();
            List<Place> places = connector.Places.Where(x => x.Location == user.Location).ToList();
            List<Place> resultPlaces = new List<Place>();

            var tagsByUser = tags; // Tag.GetTagsByUser(user.UserId);

            foreach (var e in places)
            {
                foreach (var t in tagsByUser)
                {
                    if (e.TagsKudaGo.Contains(t.Text) && !resultPlaces.Any(x => x.PlaceId == e.PlaceId))
                    {
                        resultPlaces.Add(e);
                    }
                }
            }

            return resultPlaces.ToList();
        }

        public static List<Place> GetPlaces()
        {
            DbConnect connector = new DbConnect();
            List<Place> places = connector.Places.ToList<Place>();

            return places;
        }

        public static Place GetPlaceById(int id)
        {
            DbConnect connector = new DbConnect();
            Place place = connector.Places.Where(x=>x.Id == id).FirstOrDefault();

            return place;
        }

        public List<Comment> GetComments()
        {
            List<Comment> placeComments = new List<Comment>();
            DbConnect connector = new DbConnect();
            List<CommentPlace> commentsPlaceById = connector.CommentPlaces.Where(x => x.PlaceId == this.PlaceId).ToList<CommentPlace>();
            foreach (var c in commentsPlaceById)
            {
                var comment = connector.Comments.FirstOrDefault(x => x.CommentId == c.CommentId);
                var userId = comment.UserId;
                comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    placeComments.Add(comment);
                }
            }
            Comments = placeComments;
            return placeComments;
        }

        public List<Tag> GetTagsCommon()
        {
            List<Tag> placeTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagPlace> tagsPlaceById = connector.TagPlaces.Where(x => x.PlaceId == this.PlaceId).ToList<TagPlace>();
            foreach (var c in tagsPlaceById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && (x.UserId==null||x.UserId==0));
// var userId = comment.UserId;
                //comment.Author = connector.Users.FirstOrDefault(y => y.UserId == userId);

                if (comment != null)
                {
                    placeTags.Add(comment);
                }
            }
            Tags = placeTags;
            return placeTags;
        }

        public List<Tag> GetTagsByUser(int userId)
        {
            List<Tag> placeTags = new List<Tag>();
            DbConnect connector = new DbConnect();
            List<TagPlace> tagsPlaceById = connector.TagPlaces.Where(x => x.PlaceId == this.PlaceId).ToList<TagPlace>();
            foreach (var c in tagsPlaceById)
            {
                var comment = connector.Tags.FirstOrDefault(x => x.TagId == c.TagId && x.UserId == userId);
              
                if (comment != null)
                {
                    placeTags.Add(comment);
                }
            }
            Tags = placeTags;
            return placeTags;
        }

        public static List<Place> GetPlacesByTag(int tagId)
        {
            DbConnect connector = new DbConnect();
            List<Place> places = connector.Places.ToList<Place>();
            //---
            List<int> tagPlaceById = connector.TagPlaces.Where(x => x.TagId == tagId).Select(x => x.PlaceId).ToList();
            places = connector.Places.Where(t => tagPlaceById.Contains(t.PlaceId)).ToList();

            return places;
        }
    }
}
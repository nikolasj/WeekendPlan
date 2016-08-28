using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WeekendPlan.Models
{
    public class Show
    {
        [Key]
        [Column("show_id")]
        public Int32 ShowId { get; set; }
        [Column("kudago_id")]
        public Int32 KudagoId { get; set; }
        [Column("movie_id")]
        public Int32 MovieId { get; set; }
        [Column("place_id")]
        public Int32 PlaceId { get; set; }
        [Column("datetime_id")]
        public DateTime Datetime { get; set; }
        [Column("three_d")]
        public Boolean ThreeD { get; set; }
        [Column("imax")]
        public Boolean Imax { get; set; }
        [Column("four_dx")]
        public Boolean FourDx { get; set; }
        [Column("original_language")]
        public Boolean OriginalLanguage { get; set; }
        [Column("price")]
        public String Price { get; set; }
    }
}
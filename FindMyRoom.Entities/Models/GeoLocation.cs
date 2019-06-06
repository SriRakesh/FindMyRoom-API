using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRGeolocation")]
    public class GeoLocation
    {
        [Key]
        public int GeoId { get; set; }
        [ForeignKey("FMRRooms")]
        public int RoomId { get; set; } //RoomId - ForeignKey.
        public Room FMRRooms { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}

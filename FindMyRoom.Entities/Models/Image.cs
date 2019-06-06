using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRImages")]
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        [ForeignKey("FMRRooms")]
        public int RoomId { get; set; } //RoomId - ForeignKey.
        public Room FMRRooms { get; set; }
        public Byte[] RoomImage { get; set; }
    }
}

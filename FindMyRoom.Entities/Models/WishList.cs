using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRWishLists")]
    public class WishList
    {
        [Key]
        public int WishListId { get; set; } //Primary Key in the database
        [ForeignKey("FMRUsers")]
        public int RenterId { get; set; } // UserId named as RenterID - ForeignKey
        public User FMRUsers { get; set; }
        [ForeignKey("FMRRooms")]
        public int RoomId { get; set; } //RoomId - ForeignKey.
        public Room FMRRooms { get; set; }
    }
}

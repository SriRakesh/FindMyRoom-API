using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRBookingList")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }   //Primary key in Database
        [ForeignKey("FMRUsers")]
        public int RenterId { get; set; } // UserId named as RenterID - ForeignKey
        public User FMRUsers { get; set; }
        [ForeignKey("FMRRooms")]
        public int RoomId { get; set; } //RoomId - ForeignKey.
        public Room FMRRooms { get; set; }
        public string Status { get; set; } //Booked or Not status
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMROwners")]
    public class Owner
    {
        [Key]
        public int OwnerID { get; set; } //Primary Key in the database
        [ForeignKey("FMRUsers")]
        public int PartnerId { get; set; } // UserId named as PartnerID - ForeignKey
        public User FMRUsers { get; set; }
        [ForeignKey("FMRRooms")]
        public int RoomId { get; set; }     //RoomId - ForeignKey.
        public Room FMRRooms { get; set; }
    }
}

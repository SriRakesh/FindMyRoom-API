using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
   public class BookingReceived
    {
        public int RenterId { get; set; } // UserId named as RenterID - ForeignKey
        public int RoomId { get; set; } //RoomId - ForeignKey.
      //  public string Status { get; set; } //Booked or Not status
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
  public  class RoomAlreadyBooked:Exception
    {
        public RoomAlreadyBooked(int roomNo):base("Sorry, RoomNo: "+ roomNo+" is already booked")
        {

        }
    }
}

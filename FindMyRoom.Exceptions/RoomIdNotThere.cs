using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
   public class RoomIdNotThere:Exception
    {
        public RoomIdNotThere():base("Sorry,the following room is not found in records.")
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
   public class AlreadyThereInWishList:Exception
    {
        public AlreadyThereInWishList(int roomId) : base("room: "+roomId+" already there in wishlist.") { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
   public class NotThereInWishList:Exception
    {
        public NotThereInWishList(int roomId,int renterId):base("room: "+roomId+" not there in wishlist of renter: "+renterId)
        {

        }
    }
}

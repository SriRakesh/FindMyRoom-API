using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
    public class WishListDuplicate
    {

        public int WishListId { get; set; }
        public int RenterId { get; set; }

        public int RoomId { get; set; }

        public WishListDuplicate()
        {

        }

        public WishListDuplicate(int WishListId, int RenterId, int RoomId)
        {
            this.WishListId = WishListId;
            this.RenterId = RenterId;
            this.RoomId = RoomId;
        }
    }
}

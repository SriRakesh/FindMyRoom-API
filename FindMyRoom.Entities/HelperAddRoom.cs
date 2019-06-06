using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
        public class HelperAddRoom
        {
            public int PartnerId { set; get; }
            public string Address { get; set; }
            public string Area { get; set; }
            public string city { get; set; }
            public string Description { get; set; }
            public string Furniture { get; set; }
            public int NumberOfRooms { get; set; }
            public int Pincode { get; set; }
            public int RoomCost { get; set; }
            public string RoomType { get; set; }
            public string Latitude { set; get; } 
            public string Longitude { set; get; }

        }
    
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
    public class SearchedRoomData
    {
        public int RoomId { get; set; }
        public int Cost { get; set; }
        public int NoOfBeds { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public string Furniture { get; set; }

    }

    public class FilteredRoomData
    {
        public int RoomId { get; set; }
        public int Cost { get; set; }
        public int NoOfBeds { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string RoomType { get; set; }
        public string Furniture { get; set; }
    }
}

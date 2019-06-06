using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
    public class GetCustomerOfPartners
    {
        public int UserId { get; set; }
        public int PartnerId { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserAddress { get; set; }
    }
}

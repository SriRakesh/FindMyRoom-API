using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
    public class PartnerUpdate
    {
        public int UserId { get; set; }   
        public string UserName { get; set; }  
        public string UserPhoneNumber { get; set; } 
        public string UserAddress { get; set; } 
        public string UserType { get; set; } 
        public string UserStatus { get; set; } 
    }
}

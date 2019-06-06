using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    public class UserModified
    {
        public int UserId { get; set; }   //UserId-PrimaryKey in Database 
     
        public string UserName { get; set; }  //UserName for login 
 
        public string UserEmail { get; set; }   //Email of User
        
        public string UserPhoneNumber { get; set; }   //Phone Number of the User
       
        public string UserAddress { get; set; }  //Address of User

        public string UserType { get; set; }  //UserType should be Renter or Partner or Admin

        public string UserStatus { get; set; }
    }
}

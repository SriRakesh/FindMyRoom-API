using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRUsers")]
    public class User
    {
        [Key]
        public int UserId { get; set; }   //UserId-PrimaryKey in Database 
        [Required]
        public string UserName { get; set; }  //UserName for login 
        [Required]
        public string UserPassword { get; set; }  //Password for login
        [Required]
        public string UserEmail { get; set; }   //Email of User
        [Required]
        public string UserPhoneNumber { get; set; }   //Phone Number of the User
        [Required]
        public string UserAddress { get; set; }  //Address of User

        public string UserType { get; set; }  //UserType should be Renter or Partner or Admin

        public string UserStatus { get; set; } //UserStatus represents Valid or Invalid

    }
}

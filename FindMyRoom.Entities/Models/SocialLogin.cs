using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindMyRoom.Entities.Models
{
    [Table("FMRSociallogin")]
    public class SocialLogin
    {
        [Key]
        public int SocialId { get; set; }
        [ForeignKey("FMRUsers")]
        public int UserId { get; set; } // UserId - ForeignKey
        public User FMRUsers { get; set; }
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }

    }
}

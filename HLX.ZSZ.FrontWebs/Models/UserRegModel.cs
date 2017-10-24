using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HLX.ZSZ.FrontWebs.Models
{
    public class UserRegModel
    {
        [Required]
        [Phone]
        public string PhoneNum { get; set; }
        [Required]
        [StringLength(4,MinimumLength =4)]
        public  string SmsCode { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public  string PassWord2 { get; set; }


    }
}
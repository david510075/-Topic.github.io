using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace WebApplication8.Models
{
    public class User
    {
        
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "帳號長度須介於6-50字元")]
        [DisplayName("帳號")]
        public string Account { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        [DisplayName("密碼")]
        public string Password { get; set; }
        [Required(ErrorMessage = "請輸入Email")]
        [StringLength(200, ErrorMessage = "Email最多200字元")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string Email { get; set; }
        [Required(ErrorMessage = "請輸入姓名")]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
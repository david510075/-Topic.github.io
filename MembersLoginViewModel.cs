using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication8.ViewModel
{
    public class MembersLoginViewModel
    {
        [Required(ErrorMessage = "請輸入會員帳號")]
        [DisplayName("帳號")]
        public string Account { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        [DisplayName("密碼")]
        public string Password { get; set; }
    }
}
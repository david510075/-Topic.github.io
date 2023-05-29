using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [Compare("NewPassword", ErrorMessage = "兩次密碼輸入不一致")]
        public string NewPasswordCheck { get; set; }

    }
}
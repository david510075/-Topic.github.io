using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Message
    {
        public int P_Id { get; set; }
        
        public int M_Id { get; set; }
        [DisplayName(" 留言帳號：")]
        public string Account { get; set; }
        [DisplayName(" 留言內容：")]
        [Required(ErrorMessage = "請輸入留言")]
        public string Content { get; set; }
        [DisplayName(" 留言時間：")]
        public DateTime CreateTime { get; set; }
        public User User { get; set; } = new User();
    }
}
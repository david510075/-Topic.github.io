using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication8.Models;
using WebApplication8.Services;

namespace WebApplication8.ViewModel
{
    public class MessageViewModel
    {
        public List<Message> DataList { get; set; }
        public Message Data { get; set; }
        public ForPaging Paging { get; set; }
        public int P_Id { get; set; }
    }
}
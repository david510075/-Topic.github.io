using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ItemDetailViewModel
    {
        public Item Data { get; set; }
        public List<Message> DataList { get; set; }
    }
}
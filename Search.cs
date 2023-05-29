using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Search
    {
        [DisplayName("性別：")]
        public string Ssex { get; set; }
        [DisplayName("動物：")]
        public string Sanimal { get; set; }
        [DisplayName("地區：")]
        public string Splace { get; set; }
    }
}
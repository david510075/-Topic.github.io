using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName("帳號")]
        public string Account { get; set; }
        [Required(ErrorMessage = "請輸入日期")]
        [DisplayName("看到時間")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "請輸入物種")]
        [DisplayName("動物物種")]
        public string animal { get; set; }
        [Required(ErrorMessage = "請輸入品種")]
        [DisplayName("動物品種")]
        public string Variety { get; set; }
        [Required(ErrorMessage = "請輸入顏色")]
        [DisplayName("動物顏色")]
        public string Color { get; set; }
        [Required(ErrorMessage = "請輸入地點")]
        [DisplayName("出沒地點")]
        public string Place { get; set; }
        [Required(ErrorMessage = "請輸入性別")]
        [DisplayName("動物性別")]
        public string Sex { get; set; }
        [DisplayName("連絡電話")]
        public string Phone { get; set; }
        [DisplayName("備註")]
        public string Remark { get; set; }
        [DisplayName("動物圖片")]
        public string Image { get; set; }
    }
}
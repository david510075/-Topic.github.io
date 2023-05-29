using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication8.Models;
using WebApplication8.Services;

namespace WebApplication8.ViewModel
{
    public class ItemViewModel
    {

        public List<int> IdList { get; set; }
        public List<ItemDetailViewModel> ItemBlock { get; set; }
        public ForPaging Paging { get; set; }
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        [DisplayName("照片:")]
        public HttpPostedFileBase ItemImage { get; set; }
        public string Account { get; set; }
        public Search Search { get; set; }
        public Item NewData { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication8.Models;

namespace WebApplication8.ViewModel
{
    public class ItemCreateViewModel
    {
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        [DisplayName("照片:")]
        public HttpPostedFileBase ItemImage { get; set; }
        public Item NewData { get; set; }

    }
}
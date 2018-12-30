using MyForum.Models;
using MyForum.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyForum.ViewModel
{
    public class AdvertisementView
    {
        [DisplayName("廣告網址")]
        [Url(ErrorMessage = "這不是網址標準格式")]
        public string URL { get; set; }

        [DisplayName("廣告圖片")]
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        public HttpPostedFileBase Upload { get; set; }

        [DisplayName("廣告金額")]
        public int Prize { get; set; }
    }
}
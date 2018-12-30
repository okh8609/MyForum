using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyForum.Models
{
    //定義Album資料表的驗證
    [MetadataType(typeof(AdvertisementMetadata))]
    public partial class Advertisement
    {
        private class AdvertisementMetadata
        {
            [DisplayName("廣告編號")]
            public int AD_ID { get; set; }

            [DisplayName("圖片位置")]
            public string PictPath { get; set; }

            [DisplayName("廣告網址")]
            public string URL { get; set; }

            [DisplayName("有效期限")]
            public System.DateTime EXP { get; set; }

            [DisplayName("花費")]
            public int Price { get; set; }

            [DisplayName("上傳者")]
            public string Account { get; set; }
        }
    }
}
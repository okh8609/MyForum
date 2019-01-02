using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //資料表的驗證

    [MetadataType(typeof(FamListMetadata))]
    public partial class FamList
    {
        public class FamListMetadata
        {
            [DisplayName("家族編號")]
            public int FB_ID { get; set; }

            [DisplayName("家族名稱")]
            [Required(ErrorMessage = "請輸入家族名稱")]
            [StringLength(100, ErrorMessage = "標題長度最多100字元")]
            public string FB_name { get; set; }

            [DisplayName("創建人")]
            public string Account { get; set; }
        }
    }

}
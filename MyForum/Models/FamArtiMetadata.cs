using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //定義Member資料表的驗證
    [MetadataType(typeof(FamArtiMetadata))]
    public partial class FamArti
    {
        public class FamArtiMetadata
        {

            [DisplayName("文章編號")]
            public int FA_ID { get; set; }

            [DisplayName("文章內容")]
            [Required(ErrorMessage = "請輸入文章內容")]
            public string Content { get; set; }

            [DisplayName("發表者")]
            public string Account { get; set; }

            [DisplayName("新增時間")]
            public System.DateTime CreateTime { get; set; }

            [DisplayName("觀看人數")]
            public int Watch { get; set; }

            [DisplayName("所屬看板編號")]
            public int FB_ID { get; set; }

        }
    }
}
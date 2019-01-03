
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //資料表的驗證

    [MetadataType(typeof(NewsArticleMetadata))]
    public partial class NewsArticle
    {
        private class NewsArticleMetadata
        {
            [DisplayName("動態編號")]
            public int NA_ID { get; set; }

            [DisplayName("動態")]
            [DataType(DataType.MultilineText)]
            [Required]
            public string Content { get; set; }

            [DisplayName("作者")]
            public string Account { get; set; }

            [DisplayName("發表時間")]
            public System.DateTime CreateTime { get; set; }

            [DisplayName("點閱數")]
            public int Watch { get; set; }
        }
    }
}
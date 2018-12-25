using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //定義Member資料表的驗證
    [MetadataType(typeof(ArticleMetadata))]
    public partial class Article
    {
        private class ArticleMetadata
        {
            [DisplayName("文章編號")]
            public int A_Id { get; set; }

            [DisplayName("標題")]
            [Required(ErrorMessage = "請輸入標題")]
            [StringLength(100, ErrorMessage = "標題長度最多100字元")]
            public string Title { get; set; }

            [DisplayName("文章內容")]
            [Required(ErrorMessage = "請輸入文章內容")]
            public string Content { get; set; }

            [DisplayName("發表者")]
            public string Account { get; set; }

            [DisplayName("新增時間")]
            public DateTime CreateTime { get; set; }

            [DisplayName("觀看人數")]
            public int Watch { get; set; }
        }
    }
}
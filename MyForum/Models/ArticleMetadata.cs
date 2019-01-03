using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //資料表的驗證
    [MetadataType(typeof(ArticleMetadata))]
    public partial class Article
    {
        private class ArticleMetadata
        {

            [DisplayName("點閱數")]
            public int Watch { get; set; }

            [DisplayName("編號")]
            public int A_Id { get; set; }

            [DisplayName("標題")]
            [Required(ErrorMessage = "請輸入標題")]
            [StringLength(100, ErrorMessage = "標題長度最多100字元")]
            public string Title { get; set; }

            [DisplayName("內文")]
            [Required(ErrorMessage = "請輸入內文")]
            public string Content { get; set; }

            [DisplayName("作者")]
            public string Account { get; set; }

            [DisplayName("發表時間")]
            public DateTime CreateTime { get; set; }

        }
    }
}
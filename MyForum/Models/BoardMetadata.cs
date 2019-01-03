using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //資料表的驗證

    [MetadataType(typeof(BoardMetadata))]
    public partial class Board
    {
        public class BoardMetadata
        {
            [DisplayName("編號")]
            public int B_Id { get; set; }

            [DisplayName("看版")]
            [Required(ErrorMessage = "請輸入看版名稱")]
            [StringLength(100, ErrorMessage = "名稱長度不可超過100字元")]
            public string B_name { get; set; }

            [DisplayName("版主")]
            public string Account { get; set; }
        }
    }

}
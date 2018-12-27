using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.Models
{
    //定義Member資料表的驗證
    [MetadataType(typeof(BoardMetadata))]
    public partial class Board
    {
        public class BoardMetadata
        {
            [DisplayName("看板編號")]
            public int B_Id { get; set; }

            [DisplayName("看版標題")]
            [Required(ErrorMessage = "請輸入看版標題")]
            [StringLength(100, ErrorMessage = "標題長度最多100字元")]
            public string B_name { get; set; }

            [DisplayName("發表者")]
            public string Account { get; set; }
        }
    }

}
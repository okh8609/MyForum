using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FriendshipSearchView
    {
        //搜尋欄位
        [DisplayName("搜尋：")]
        public string Search { get; set; }

        //顯示資料陣列
        public List<Member> DataList { get; set; }

        ////UID
        //public int Account { get; set; }
    }
}
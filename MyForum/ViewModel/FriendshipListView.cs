using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FriendshipListView
    {
        //顯示資料陣列
        public List<Member> DataList { get; set; }

        ////UID
        //public int Account { get; set; }
    }
}
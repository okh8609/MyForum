using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class NewsMessageView
    {
        //顯示資料陣列
        public List<NewsMessage> DataList { get; set; }

        //動態編號
        public int NA_ID { get; set; }

    }
}
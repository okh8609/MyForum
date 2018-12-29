using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class NewsArticleView
    {
        //顯示資料陣列
        public List<NewsArticle> DataList { get; set; }

        //分頁內容
        public ForPaging Paging { get; set; }
    }
}
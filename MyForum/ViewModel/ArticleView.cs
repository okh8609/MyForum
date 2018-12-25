using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    //文章用ViewModel
    public class ArticleView
    {
        //搜尋欄位
        [DisplayName("搜尋：")]
        public string Search { get; set; }
        //顯示資料陣列
        public List<Article> DataList { get; set; }
        //分頁內容
        public ForPaging Paging { get; set; }
        //看板編號
        public int B_Id { get; set; }
    }
}
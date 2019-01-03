using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class ArticleView
    {
        [DisplayName("搜尋：")]
        public string Search { get; set; }
        public List<Article> DataList { get; set; }
        public ForPaging Paging { get; set; }
        public int B_Id { get; set; }
        public string Title { get; set; }
    }
}
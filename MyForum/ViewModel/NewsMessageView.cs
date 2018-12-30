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
        public List<NewsMessage> DataList { get; set; }

        public int NA_ID { get; set; }

    }
}
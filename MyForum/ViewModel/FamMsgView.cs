﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FamMsgView
    {
        //顯示資料陣列
        public List<FamMsg> DataList { get; set; }

        //分頁內容
        public ForPaging Paging { get; set; }

        //文章編號
        public int FA_ID { get; set; }
    }
}
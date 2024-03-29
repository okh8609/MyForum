﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class BoardView
    {
        [DisplayName("搜尋：")]
        public string Search { get; set; }
        public List<Board> DataList { get; set; }
        public ForPaging Paging { get; set; }
    }
}
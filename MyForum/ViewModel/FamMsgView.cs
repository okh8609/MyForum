using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FamMsgView
    {
        public List<FamMsg> DataList { get; set; }

        public ForPaging Paging { get; set; }

        public int FA_ID { get; set; }
    }
}
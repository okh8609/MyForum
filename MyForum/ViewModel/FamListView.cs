using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FamListView
    {
        public List<FamList> DataList { get; set; }

        public ForPaging Paging { get; set; }
    }
}
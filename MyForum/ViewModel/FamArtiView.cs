using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FamArtiView
    {

        public List<FamArti> DataList { get; set; }

        public ForPaging Paging { get; set; }

        public int FB_ID { get; set; }
    }
}
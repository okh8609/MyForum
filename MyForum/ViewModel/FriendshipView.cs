using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class FriendshipView
    {
        [DisplayName("搜尋：")]
        public string Search { get; set; }

        public List<Member> DataList { get; set; }

    }
}
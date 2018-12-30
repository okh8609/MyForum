using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.ViewModel
{
    public class MessageView //留言用ViewModel
    {
        public List<Message> DataList { get; set; }
        public ForPaging Paging { get; set; }
        public int A_Id { get; set; }
    }
}
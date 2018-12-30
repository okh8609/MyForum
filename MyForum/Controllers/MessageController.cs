using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyForum.Services;
using MyForum.ViewModel;
using MyForum.Models;


namespace MyForum.Controllers
{
    public class MessageController : Controller
    {
        private MessageService messageService = new MessageService();

        #region 開始頁面
        [Authorize]
        public ActionResult Index(int Id)
        {
            ViewData["A_Id"] = Id;
            return PartialView();
        }
        #endregion

        #region 留言陣列
        [Authorize]
        public ActionResult DataList(int A_Id, int Page = 1)
        {
            MessageView Data = new MessageView();
            Data.Paging = new ForPaging(Page); 
            Data.A_Id = A_Id; //將傳入值文章編號入頁面模型中

            Data.DataList = messageService.GetDataList(Data.Paging, Data.A_Id);
            return PartialView(Data);
        }
        #endregion

        #region 新增文章
        [Authorize] 
        public ActionResult Create(int A_Id)
        {
            ViewData["A_Id"] = A_Id;
            return PartialView();
        }

        [Authorize]
        [HttpPost] 
        public ActionResult Add(int Id, [Bind(Include = "Content")]Message Data)
        {
            Data.Account = User.Identity.Name;
            Data.A_Id = Id; //設定對應的Article編號


            messageService.Insert(Data);
            return RedirectToAction("Index", new { Id = Id });
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyForum.Services;
using MyForum.ViewModel;
using MyForum.Models;

namespace MyForum.Models
{
    public class NewsMessageController : Controller
    {
        private NewsMessageService messageService = new NewsMessageService();

        #region 開始頁面
        [Authorize]
        public ActionResult Index(int NA_ID)
        {
            ViewData["NA_ID"] = NA_ID;
            return PartialView();
        }
        #endregion

        #region 留言陣列
        [Authorize]
        public ActionResult DataList(int NA_ID, int Page = 1)
        {
            NewsMessageView Data = new NewsMessageView();
            Data.NA_ID = NA_ID; //將傳入值文章編號入頁面模型中

            Data.DataList = messageService.GetDataList(Data.NA_ID);
            return PartialView(Data); 
        }
        #endregion

        #region 新增留言
        [Authorize] 
        public ActionResult Create(int NA_ID)
        {
            ViewData["NA_ID"] = NA_ID;
            return PartialView();
        }

        [Authorize] 
        [HttpPost] 
        public ActionResult Add(int NA_ID, [Bind(Include = "Content")]NewsMessage Data)
        {
            Data.Account = User.Identity.Name;
            Data.NA_ID = NA_ID; //設定對應的Article編號

            messageService.Insert(Data);

            return RedirectToAction("Index", new { NA_ID = NA_ID });
        }
        #endregion
    }
}
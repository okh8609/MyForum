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
    public class FamMsgController : Controller
    {
        private FamMsgService messageService = new FamMsgService();

        #region 開始頁面
        [Authorize]
        public ActionResult Index(int FA_ID)
        {
            ViewData["FA_ID"] = FA_ID;
            return PartialView();
        }
        #endregion

        #region 留言陣列
        [Authorize]
        public ActionResult DataList(int FA_ID)
        {
            FamMsgView Data = new FamMsgView();
            Data.FA_ID = FA_ID; //將傳入值文章編號入頁面模型中
            Data.DataList = messageService.GetDataList(Data.FA_ID);

            return PartialView(Data); 
        }
        #endregion

        #region 新增文章
        [Authorize] 
        public ActionResult Create(int FA_ID)
        {
            ViewData["FA_ID"] = FA_ID;
            return PartialView();
        }

        [Authorize] 
        [HttpPost]
        public ActionResult Add(int FA_ID, [Bind(Include = "Content")]FamMsg Data)
        {
            Data.Account = User.Identity.Name;
            Data.FA_ID = FA_ID; //設定對應的Article編號
            messageService.Insert(Data);
            return RedirectToAction("Index", new { FA_ID = FA_ID });
        }
        #endregion
    }
}
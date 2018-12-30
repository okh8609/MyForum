using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyForum.Models;
using MyForum.Services;
using MyForum.ViewModel;

namespace MyForum.Controllers
{
    public class FamListController : Controller
    {

        FamListService boardService = new FamListService();

        #region 功能選單
        [Authorize]
        public ActionResult Menu()
        {
            return View();
        }
        #endregion

        #region 開始頁面
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 看板列表
        [Authorize]
        public ActionResult List(int Page = 1)
        {
            FamListView Data = new FamListView();
            Data.Paging = new ForPaging(Page);
            Data.DataList = boardService.GetDataList(Data.Paging);

            return PartialView(Data);
        }
        #endregion

        #region 新增看板
        [Authorize] 
        public ActionResult Create()
        {
            return PartialView();
        }

        
        [Authorize] 
        [HttpPost]
        public ActionResult Add([Bind(Include = "FB_name")]FamList Data)
        {
            Data.Account = User.Identity.Name;
            boardService.Insert(Data);
            return RedirectToAction("Index");
        }
        #endregion
    }
}

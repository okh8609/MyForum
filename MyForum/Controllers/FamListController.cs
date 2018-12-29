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
        //將Page(頁數)預設為1
        [Authorize]
        public ActionResult List(int Page = 1)
        {
            //宣告一個新頁面模型
            FamListView Data = new FamListView();
            //新增頁面模型中的分頁
            Data.Paging = new ForPaging(Page);
            //從Service中取得頁面所需陣列資料
            Data.DataList = boardService.GetDataList(Data.Paging);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增看板
        //新增文章一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create()
        {
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add([Bind(Include = "FB_name")]FamList Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            //使用Service來新增一筆資料
            boardService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index");
        }
        #endregion
    }
}

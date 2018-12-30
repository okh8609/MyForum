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
    public class NewsArticleController : Controller
    {
        NewsArticleService naService = new NewsArticleService();

        #region 開始頁面
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 看板文章列表
        [Authorize]
        public ActionResult List(int Page = 1)
        {
            NewsArticleView Data = new NewsArticleView();
            Data.Paging = new ForPaging(Page); 

            Data.DataList = naService.GetDataList(User.Identity.Name);
            return PartialView(Data); 
        }
        #endregion

        #region 新增文章
        [Authorize] 
        public ActionResult Create()
        {
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] 
        [HttpPost] 
        public ActionResult Add([Bind(Include = "Content")]NewsArticle Data)
        {
            Data.Account = User.Identity.Name;

            naService.Insert(Data);

            return RedirectToAction("Index");
        }
        #endregion


        #region 文章頁面
        //文章頁面要根據傳入編號來決定要顯示的資料
        public ActionResult Article(int NA_ID)
        {
            NewsArticle Data = naService.GetDataById(NA_ID);
            naService.Watch(NA_ID); //加點擊數
            naService.Coin(NA_ID); //加台科幣
            return View(Data);
        }
        #endregion

    }
}
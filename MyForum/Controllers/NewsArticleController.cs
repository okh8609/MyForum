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
            NewsArticleView Data = new NewsArticleView();//宣告一個新頁面模型
            Data.Paging = new ForPaging(Page); //新增頁面模型中的分頁

            //從Service中取得頁面所需陣列資料
            Data.DataList = naService.GetDataList(User.Identity.Name);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增文章
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
        public ActionResult Add([Bind(Include = "Content")]NewsArticle Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            //使用Service來新增一筆資料
            naService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index");
        }
        #endregion


        #region 文章頁面
        //文章頁面要根據傳入編號來決定要顯示的資料
        public ActionResult Article(int NA_ID)
        {
            //取得頁面所需資料，藉由Service取得
            NewsArticle Data = naService.GetDataById(NA_ID);
            naService.Watch(NA_ID); //將資料庫內資料加一觀看人數
            naService.Coin(NA_ID); //將資料庫內資料加一觀看人數
            return View(Data); //將資料傳入View中
        }
        #endregion

    }
}
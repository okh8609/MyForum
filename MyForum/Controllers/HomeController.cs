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
    public class HomeController : Controller
    {
        ArticleService articleService = new ArticleService();

        //#region 開始頁面
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //#endregion

        //#region 文章列表
        //public ActionResult List(string Search, int Page = 1)
        //{
        //    ArticleView Data = new ArticleView();
        //    Data.Search = Search;
        //    Data.Paging = new ForPaging(Page);
        //    Data.DataList = articleService.GetDataList(Data.Paging, Data.Search);
        //    return PartialView(Data);
        //}
        //#endregion

        #region 看板文章列表
        public ActionResult List(int B_Id, int Page = 1)
        {
            ArticleView Data = new ArticleView();
            Data.Paging = new ForPaging(Page);
            Data.B_Id = B_Id; //將傳入值文章編號入頁面模型中
            ViewData["B_Id"] = B_Id;
            Data.DataList = articleService.GetDataList(Data.Paging, Data.B_Id);
            return View(Data); 
        }
        #endregion

        #region 文章頁面
        public ActionResult Article(int Id)
        {
            Article Data = articleService.GetDataById(Id);
            articleService.Watch(Id); //加點擊數
            articleService.Coin(Id); //加台科幣
            return View(Data);
        }
        #endregion

        #region 新增文章
        [Authorize] 
        public ActionResult Create(int id)
        {
            ViewData["B_Id"] = id;
            return PartialView();
        }
        //[Authorize]
        //public ActionResult Create()
        //{
        //    //ViewData["B_Id"] = id;
        //    ViewData["B_Id"] = 2;
        //    return PartialView();
        //}

        [Authorize] 
        [HttpPost] 
        public ActionResult Add(int B_Id, [Bind(Include = "Title,Content")]Article Data)
        {
            Data.Account = User.Identity.Name;
            Data.B_Id = B_Id;

            articleService.Insert(Data);
            return RedirectToAction("List", new { B_Id = B_Id });
        }
        #endregion

        #region 修改文章
        //修改文章頁面要根據傳入編號來決定要修改的資料
        [Authorize] 
        public ActionResult Edit(int Id)
        {
            Article Data = articleService.GetDataById(Id);
            return PartialView(Data);
        }

        [Authorize] 
        [HttpPost] 
        public ActionResult Edit(int Id, FormCollection FormValues)
        {
            //修改資料的是否可修改判斷
            //if (articleService.CheckUpdate(Id))
            //{
                Article Data = articleService.GetDataById(Id);
                //使用Controller內建UpdateModel方法來修改資料
                //並限定修改的欄位屬性
                UpdateModel(Data, new string[] { "Content" });
                articleService.Save();
            //}
            return RedirectToAction("Article", new { Id = Id });
        }
        #endregion

        #region 刪除文章
        [Authorize(Roles = "Admin")] //設定此Action只有Admin角色才可使用
        public ActionResult Delete(int Id)
        {
            articleService.Delete(Id); 
            return RedirectToAction("Index"); 
        }
        #endregion

        #region 顯示人氣
        public ActionResult ShowPopularity()
        {
            ArticleView Data = new ArticleView();
            Data.DataList = articleService.GetPopularityDataList();
            return View(Data);
        }
        #endregion
    }
}
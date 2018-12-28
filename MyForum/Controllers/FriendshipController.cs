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
    public class FriendshipController : Controller
    {
        FriendshipService fsService = new FriendshipService();

        #region 開始頁面
        [Authorize]
        public ActionResult Index(string Account)
        {
            ViewData["UID"] = Account;
            return PartialView();
        }
        #endregion

        #region 搜尋好友
        //將Page(頁數)預設為
        [Authorize]
        public ActionResult SearchList(string Search)
        {
            //宣告一個新頁面模型
            FriendshipSearchView Data = new FriendshipSearchView();
            //將傳入值Search(搜尋)放入頁面模型中
            Data.Search = Search;
            //從Service中取得頁面所需陣列資料
            Data.DataList = fsService.GetSearchFriendList(Data.Search);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 好友列表
        //將Page(頁數)預設為
        [Authorize]
        public ActionResult MyFriendList(string Account)
        {
            //宣告一個新頁面模型
            FriendshipSearchView Data = new FriendshipSearchView();
            //將傳入值Search(搜尋)放入頁面模型中
            Data.Search = Account;
            //從Service中取得頁面所需陣列資料
            Data.DataList = fsService.GetSearchFriendList(Data.Search);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion


        #region 新增好友
        //新增好友一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create()
        {
            return PartialView();
        }

        //新增好友傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        public ActionResult Add(String p2)
        {
            //使用Service來新增一筆資料
            fsService.Add(User.Identity.Name, p2);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index");
        }
        #endregion
    }
}
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
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 真．好友列表
        [Authorize]
        public ActionResult FriendList(string Search)
        {
            FriendshipView Data = new FriendshipView();
            Data.Search = Search;
            if(String.IsNullOrEmpty(Data.Search))
            {
                Data.DataList = fsService.GetMyFriendList(User.Identity.Name);
            }
            else
            {
                Data.DataList = fsService.GetSearchFriendList(Data.Search);
            }
            return PartialView(Data); 
        }
        #endregion

        #region 新增好友
        //新增好友一開始載入頁面
        [Authorize] 
        public ActionResult Create()
        {
            return PartialView();
        }

        //新增好友傳入資料時的Action
        [Authorize]
        [HttpGet] 
        public ActionResult Add(string p2)
        {
            fsService.Add(User.Identity.Name, p2);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
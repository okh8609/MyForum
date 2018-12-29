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
        //將Page(頁數)預設為1
        public ActionResult DataList(int NA_ID, int Page = 1)
        {
            NewsMessageView Data = new NewsMessageView();//宣告一個新頁面模型
            Data.NA_ID = NA_ID; //將傳入值文章編號入頁面模型中
            //從Service中取得頁面所需陣列資料
            Data.DataList = messageService.GetDataList(Data.NA_ID);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增文章
        //新增文章一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create(int NA_ID)
        {
            ViewData["NA_ID"] = NA_ID;
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add(int NA_ID, [Bind(Include = "Content")]NewsMessage Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            Data.NA_ID = NA_ID; //設定對應的Article編號
            //使用Service來新增一筆資料
            messageService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index", new { NA_ID = NA_ID });
        }
        #endregion
    }
}
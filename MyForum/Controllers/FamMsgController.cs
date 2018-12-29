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
        //將Page(頁數)預設為1
        public ActionResult DataList(int FA_ID)
        {
            FamMsgView Data = new FamMsgView();//宣告一個新頁面模型
            Data.FA_ID = FA_ID; //將傳入值文章編號入頁面模型中
            //從Service中取得頁面所需陣列資料
            Data.DataList = messageService.GetDataList(Data.FA_ID);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增文章
        //新增文章一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create(int FA_ID)
        {
            ViewData["FA_ID"] = FA_ID;
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add(int FA_ID, [Bind(Include = "Content")]FamMsg Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            Data.FA_ID = FA_ID; //設定對應的Article編號
            //使用Service來新增一筆資料
            messageService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index", new { FA_ID = FA_ID });
        }
        #endregion
    }
}
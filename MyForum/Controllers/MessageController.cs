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
    public class MessageController : Controller
    {
        //宣告Guestbooks資料表的Service物件
        private MessageService messageService = new MessageService();

        #region 開始頁面
        public ActionResult Index(int Id)
        {
            ViewData["A_Id"] = Id;
            return PartialView();
        }
        #endregion

        #region 留言陣列
        //將Page(頁數)預設為1
        public ActionResult DataList(int A_Id, int Page = 1)
        {
            MessageView Data = new MessageView();//宣告一個新頁面模型
            Data.Paging = new ForPaging(Page); //新增頁面模型中的分頁
            Data.A_Id = A_Id; //將傳入值文章編號入頁面模型中
            //從Service中取得頁面所需陣列資料
            Data.DataList = messageService.GetDataList(Data.Paging, Data.A_Id);
            return PartialView(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增文章
        //新增文章一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create(int A_Id)
        {
            ViewData["A_Id"] = A_Id;
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add(int Id, [Bind(Include = "Content")]Message Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            Data.A_Id = Id; //設定對應的Article編號
            //使用Service來新增一筆資料
            messageService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index", new { Id = Id });
        }
        #endregion
    }
}
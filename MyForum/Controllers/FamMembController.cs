using System;
using System.Collections.Generic;
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
    public class FamMembController : Controller
    {
        FamMembService fmService = new FamMembService();

        // GET: FamMemb

        public ActionResult Index()
        {
            FamMembView Data = new FamMembView();
            Data.DataList = fmService.GetDataList(User.Identity.Name);
            return View(Data);
        }

        //新增好友傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpGet] //設定此Action只接受頁面POST資料傳入
        public ActionResult Add(int FB_ID)
        {
            //使用Service來新增一筆資料
            fmService.Add(User.Identity.Name, FB_ID);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index", "FamList");
        }

    }
}
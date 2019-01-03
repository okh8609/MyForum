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

        public ActionResult Index()
        {
            FamMembView Data = new FamMembView();
            Data.DataList = fmService.GetDataList(User.Identity.Name);
            return View(Data);
        }

        //新增好友傳入資料時的Action
        [Authorize] 
        [HttpGet] 
        public ActionResult Add(int FB_ID)
        {
            fmService.Add(User.Identity.Name, FB_ID);
            return RedirectToAction("Index", "FamList");
        }

    }
}
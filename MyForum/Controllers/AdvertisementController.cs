using MyForum.Models;
using MyForum.Services;
using MyForum.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyForum.Controllers
{
    public class AdvertisementController : Controller
    {
        AdvertisementService AdService = new AdvertisementService();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        //public ActionResult Buy([Bind(Include = "upload")]AdvertisementView File)
        public ActionResult Buy(AdvertisementView File)
        {
            //檢查是否有上傳檔案
            if (File.Upload == null)
                return RedirectToAction("Error");

            //將檔案和伺服器上路徑合併
            string fileName = DateTime.Now.Millisecond.ToString() + Path.GetExtension(File.Upload.FileName);
            string url = Path.Combine(Server.MapPath("~/Upload/"), fileName);
            //將檔案儲存於伺服器上
            File.Upload.SaveAs(url);
            //藉由Service將檔案資料存入資料庫
            AdService.UploadFile(fileName, url, 10, User.Identity.Name);
            //重新導向頁面至開始頁面
            return RedirectToAction("Welcome", "Board");
        }

        public ActionResult Show()
        {
            return View();
        }

        [Authorize]
        public ActionResult Error()
        {
            return View();
        }
    }
}
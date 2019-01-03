using MyForum.Models;
using MyForum.Services;
using MyForum.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Drawing;


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
        public ActionResult Buy(AdvertisementView Obj)
        {
            //欄位驗證沒有過
            //if (ModelState.IsValid == false)
            //{
            //    TempData["Msg"] = "驗證不通過，謝謝!!";
            //    return RedirectToAction("Index");
            //}

            //判斷網址是否存在
            try
            {
                WebRequest request = WebRequest.Create(Obj.URL);
                request.GetResponse();
            }
            catch
            {
                TempData["Msg"] = "網址異常!!";
                return RedirectToAction("Result");
            }

            //判斷金額
            if (Obj.Price < 0 || Obj.Price > 1000)
            {
                TempData["Msg"] = "金額異常!!";
                return RedirectToAction("Result");
            }

            //判斷餘額
            if (AdService.BuyAd(User.Identity.Name, Obj.Price) == false)
            {
                TempData["Msg"] = "台科幣 餘額不足，謝謝!!";
                return RedirectToAction("Result");
            }

            //檢查是否有上傳檔案
            if (Obj.Upload == null)
            {
                TempData["Msg"] = "檔案不存在，請確認，謝謝!!";
                return RedirectToAction("Result");
            }

            //檢查副檔名
            if (Path.GetExtension(Obj.Upload.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(Obj.Upload.FileName).ToLower() != ".png")
            {
                TempData["Msg"] = "副檔名必須是 .jpg .png !!";
                return RedirectToAction("Result");
            }

            //將檔案和伺服器上路徑合併
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" +
                DateTime.Now.Millisecond.ToString() + Path.GetExtension(Obj.Upload.FileName);
            string url = Path.Combine(Server.MapPath("~/Ad-Uploads/"), fileName);
            //將檔案儲存於伺服器上
            Obj.Upload.SaveAs(url);

            //檢查是否真的是圖片檔，否則刪除
            try
            {
                Bitmap img = new Bitmap(url, true);
                if (img.Width > 515 || img.Height > 515)
                {
                    TempData["Msg"] = "圖片尺寸過大!!";
                    return RedirectToAction("Result");
                }
            }
            catch
            {
                //刪除
                System.IO.File.Delete(url);
                TempData["Msg"] = "非圖檔 !!";
                return RedirectToAction("Result");
            }

            //藉由Service將檔案資料存入資料庫
            AdService.UploadFile(fileName, Obj.URL, Obj.Price, User.Identity.Name);
            TempData["Msg"] = "購買成功!!";
            //重新導向頁面至開始頁面
            return RedirectToAction("Result");
        }

        [Authorize]
        public ActionResult Result()
        {
            return View();
        }

        //顯示在網頁底端的廣告頁面
        public ActionResult Show()
        {
            AdvertisementShowView data = new AdvertisementShowView();
            data.URLs = AdService.GetAd();
            return PartialView(data);
        }
    }
}
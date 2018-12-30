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
    public class BoardController : Controller
    {
        BoardService boardService = new BoardService();

        #region 歡迎畫面
        public ActionResult Welcome()
        {
            if (boardService.TryConnectServerByPort("122.116.151.243", 61433) == true)
                //ViewData["DBstatus"] = "資料庫目前連線正常!!";
                ViewData["DBstatus"] = "1";

            else
                //ViewData["DBstatus"] = "資料庫目前無法連線!!";
                ViewData["DBstatus"] = "0";

            return View();
        }
        #endregion

        #region 開始頁面
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 看板列表
        [Authorize]
        public ActionResult List(string Search, int Page = 1)
        {
            BoardView Data = new BoardView();
            Data.Search = Search;
            Data.Paging = new ForPaging(Page);
            Data.DataList = boardService.GetDataList(Data.Paging, Data.Search);

            return PartialView(Data);
        }
        #endregion

        #region 新增看板
        [Authorize] 
        public ActionResult Create()
        {
            return PartialView();
        }

        [Authorize] 
        [HttpPost] 
        public ActionResult Add([Bind(Include = "B_name")]Board Data)
        {
            Data.Account = User.Identity.Name;
            boardService.Insert(Data);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyForum.Models;
using MyForum.Services;
using MyForum.ViewModel;


namespace MyForum.Controllers
{
    public class FamArtiController : Controller
    {
        FamArtiService articleService = new FamArtiService();

        #region 看板文章列表
        [Authorize]
        public ActionResult List(int FB_ID, int Page = 1)
        {
            FamArtiView Data = new FamArtiView();
            Data.Paging = new ForPaging(Page);
            Data.FB_ID = FB_ID; //將傳入值文章編號入頁面模型中
            ViewData["FB_ID"] = FB_ID;
            Data.DataList = articleService.GetDataList(Data.Paging, Data.FB_ID);

            return View(Data); 
        }
        #endregion

        #region 新增文章
        [Authorize] 
        public ActionResult Create(int FB_ID)
        {
            ViewData["FB_ID"] = FB_ID;
            return PartialView();
        }

        [Authorize] 
        [HttpPost]
        public ActionResult Add(int FB_ID, [Bind(Include = "Content")]FamArti Data)
        {
            Data.Account = User.Identity.Name;
            Data.FB_ID = FB_ID;
            articleService.Insert(Data);

            return RedirectToAction("List", new { FB_ID = FB_ID });
        }
        #endregion



        #region 文章頁面
        [Authorize]
        public ActionResult Article(int FA_ID)
        {
            FamArti Data = articleService.GetDataById(FA_ID);
            articleService.Watch(FA_ID); //加點擊數
            articleService.Coin(FA_ID); //加台科幣
            return View(Data); 
        }
        #endregion
    }
}
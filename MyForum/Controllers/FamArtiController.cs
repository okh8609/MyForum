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
            FamArtiView Data = new FamArtiView();//宣告一個新頁面模型
            Data.Paging = new ForPaging(Page); //新增頁面模型中的分頁
            Data.FB_ID = FB_ID; //將傳入值文章編號入頁面模型中
            ViewData["FB_ID"] = FB_ID;
            //從Service中取得頁面所需陣列資料
            Data.DataList = articleService.GetDataList(Data.Paging, Data.FB_ID);
            return View(Data); //將頁面資料傳入View中
        }
        #endregion

        #region 新增文章
        //新增文章一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult Create(int FB_ID)
        {
            ViewData["FB_ID"] = FB_ID;
            return PartialView();
        }

        //新增文章傳入資料時的Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        //使用Bind的Inculde來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add(int FB_ID, [Bind(Include = "Content")]FamArti Data)
        {
            //設定新增文章的新增者為登入者
            Data.Account = User.Identity.Name;
            Data.FB_ID = FB_ID;
            //使用Service來新增一筆資料
            articleService.Insert(Data);
            //重新導向頁面至開始頁面
            return RedirectToAction("List", new { FB_ID = FB_ID });
        }
        #endregion



        #region 文章頁面
        //文章頁面要根據傳入編號來決定要顯示的資料
        [Authorize]
        public ActionResult Article(int FA_ID)
        {
            //取得頁面所需資料，藉由Service取得
            FamArti Data = articleService.GetDataById(FA_ID);
            articleService.Watch(FA_ID); //將資料庫內資料加一觀看人數
            return View(Data); //將資料傳入View中
        }
        #endregion
    }
}
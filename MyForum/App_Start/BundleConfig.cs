//using System.Web;
//using System.Web.Optimization;

//namespace MyForum
//{
//    public class BundleConfig
//    {
//        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
//        public static void RegisterBundles(BundleCollection bundles)
//        {
//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                        "~/Scripts/jquery-{version}.js"));

//            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
//                        "~/Scripts/jquery.validate*"));

//            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
//            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
//            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
//                        "~/Scripts/modernizr-*"));

//            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
//                      "~/Scripts/bootstrap.js"));

//            bundles.Add(new StyleBundle("~/Content/css").Include(
//                      "~/Content/bootstrap.css",
//                      "~/Content/site.css"));
//        }
//    }
//}

using System.Web;
using System.Web.Optimization;

namespace MyForum
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/additional-methods.js",
                        "~/Scripts/jquery.unobtrusive-ajax*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // 自訂義的 CSS 與 JS
            bundles.Add(new StyleBundle("~/Content/accountinfo").Include(
                      "~/Content/styles/accountinfo.css"));

            bundles.Add(new StyleBundle("~/Content/advertise").Include(
                      "~/Content/styles/advertise.css"));

            bundles.Add(new StyleBundle("~/Content/article").Include(
                      "~/Content/styles/article.css"));

            bundles.Add(new StyleBundle("~/Content/articlelist").Include(
                      "~/Content/styles/articlelist.css"));

            bundles.Add(new StyleBundle("~/Content/boardlist").Include(
                      "~/Content/styles/boardlist.css"));

            bundles.Add(new StyleBundle("~/Content/card").Include(
                      "~/Content/styles/card.css"));

            bundles.Add(new StyleBundle("~/Content/changePW").Include(
                      "~/Content/styles/changePW.css"));

            bundles.Add(new StyleBundle("~/Content/famarti").Include(
                      "~/Content/styles/famarti.css"));

            bundles.Add(new StyleBundle("~/Content/famartilist").Include(
                      "~/Content/styles/famartilist.css"));

            bundles.Add(new StyleBundle("~/Content/famlist").Include(
                      "~/Content/styles/famlist.css"));

            bundles.Add(new StyleBundle("~/Content/fammsg").Include(
                      "~/Content/styles/fammsg.css"));

            bundles.Add(new StyleBundle("~/Content/firendlist").Include(
                      "~/Content/styles/firendlist.css"));

            bundles.Add(new StyleBundle("~/Content/homepage").Include(
                 "~/Content/styles/homepage.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/styles/login.css"));

            bundles.Add(new StyleBundle("~/Content/newsarticle").Include(
                      "~/Content/styles/newsarticle.css"));

            bundles.Add(new StyleBundle("~/Content/newslist").Include(
                      "~/Content/styles/newslist.css"));

            bundles.Add(new StyleBundle("~/Content/popular").Include(
                 "~/Content/styles/popular.css"));

            bundles.Add(new StyleBundle("~/Content/register").Include(
                      "~/Content/styles/register.css"));
        }
    }
}


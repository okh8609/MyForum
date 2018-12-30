using MyForum.Services;
using MyForum.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyForum.Models;


namespace MyForum.Controllers
{
    public class MemberController : Controller
    {
        //宣告Members資料表的Service物件
        private MemberService memberService = new MemberService();
        //宣告寄信用的Service物件
        private MailService mailService = new MailService();

        #region 登入
        //登入一開始載入畫面
        public ActionResult Login()
        {
            //判斷使用者是否已經過登入驗證
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Board"); //已登入則重新導向

            return View();//否則進入登入畫面
        }
        //傳入登入資料的Action
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        public ActionResult Login(MemberLoginView LoginMember)
        {
            //使用Service裡的方法來驗證登入的帳號密碼
            string ValidateStr = memberService.LoginCheck(LoginMember.UserName, LoginMember.Password);
            //判斷驗證後結果是否有錯誤訊息
            if (String.IsNullOrEmpty(ValidateStr))
            {
                //無錯誤訊息，則登入，先藉由Service取得登入者角色資料
                string RoleData = memberService.GetRole(LoginMember.UserName);

                //新增一個登入用Ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    LoginMember.UserName, //使用者名稱
                    DateTime.Now, //起始時間
                    DateTime.Now.AddMinutes(30), //到期時間，這裡設定為30分鐘後
                    false, //設定是否以Cookie存取
                    RoleData, //使用者資料，這裡存入角色資料
                    FormsAuthentication.FormsCookiePath); //設定儲存路徑，使用預設

                //將資料加密成字串
                string enTicket = FormsAuthentication.Encrypt(ticket);
                //將資料存入Cookies中
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
                //重新導向頁面
                return RedirectToAction("Index", "Board");
            }
            else
            {
                //有驗證錯誤訊息，加入頁面模型中
                ModelState.AddModelError("", ValidateStr);
                //將資料回填至View中
                return View(LoginMember);
            }
        }
        #endregion

        #region 註冊
        //註冊一開始顯示頁面
        public ActionResult Register()
        {
            //判斷使用者是否已經過登入驗證
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Board"); //已登入則重新導向 
            return View();//否則進入註冊畫面
        }

        //傳入註冊資料的Action
        [HttpPost] //設定此Action只接受頁面POST資料傳入
        public ActionResult Register(MemberRegisterView RegisterMember)
        {
            //判斷頁面資料是否都經過驗證
            if (ModelState.IsValid)
            {
                //將頁面資料中的密碼欄位填入
                RegisterMember.newMember.Password = RegisterMember.Password;
                //取得信箱驗證碼
                string AuthCode = mailService.GetValidateCode();
                //將信箱驗證碼填入
                RegisterMember.newMember.AuthCode = AuthCode;
                //呼叫Serrvice註冊新會員
                memberService.Register(RegisterMember.newMember);
                //取得寫好的驗證信範本內容
                string TempMail = System.IO.File.ReadAllText(
                    Server.MapPath("~/Views/Shared/RegisterEmailTemplate.html"));
                //宣告Email驗證用的Url
                UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("EmailValidate", "Member",
                            new
                            {
                                UserName = RegisterMember.newMember.Account,
                                AuthCode = AuthCode
                            })
                };
                //藉由Service將使用者資料填入驗證信範本中
                string MailBody =
                    mailService.GetRegisterMailBody(TempMail
                        , RegisterMember.newMember.Name
                        , ValidateUrl.ToString().Replace("%3F", "?"));
                //呼叫Service寄出驗證信
                mailService.SendRegisterMail(MailBody, RegisterMember.newMember.Email);
                //用TempData儲存註冊訊息
                TempData["RegisterState"] = "註冊成功，請至信箱收信，以驗證Email";
                //重新導向頁面
                return RedirectToAction("RegisterResult");
            }
            //未驗證成功 清空密碼相關欄位
            RegisterMember.Password = null;
            RegisterMember.PasswordCheck = null;
            //將資料回填至View中
            return View(RegisterMember);
        }

        //註冊結果顯示頁面
        public ActionResult RegisterResult()
        {
            return View();
        }

        //判斷註冊帳號是否已被註冊過Action
        public JsonResult AccountCheck(MemberRegisterView RegisterMember)
        {
            //呼叫Service來判斷，並回傳結果
            return Json(memberService.AccountCheck(RegisterMember.newMember.Account)
                , JsonRequestBehavior.AllowGet);
        }

        //接收驗證信連結傳進來的Action
        public ActionResult EmailValidate(string UserName, string AuthCode)
        {
            //用ViewData儲存，使用Service進行信箱驗證後的結果訊息
            ViewData["EmailValidate"] = memberService.EmailValidate(UserName, AuthCode);
            return View();
        }
        #endregion

        #region 修改密碼
        //修改密碼一開始載入頁面
        [Authorize] //設定此Action須登入
        public ActionResult ChangePassword()
        {
            return View();
        }

        //修改密碼傳入資料Action
        [Authorize] //設定此Action須登入
        [HttpPost] //設定此Action接受頁面POST資料傳入
        public ActionResult ChangePassword(MemberChangePasswordView ChangeData)
        {
            //判斷頁面資料是否都經過驗證
            if (ModelState.IsValid)
            {
                ViewData["ChangeState"] =
                    memberService.ChangePassword(User.Identity.Name
                    , ChangeData.Password, ChangeData.NewPassword);
            }
            return View();
        }
        #endregion

        #region 登出
        //登出Action
        [Authorize] //設定此Action須登入
        public ActionResult Logout()
        {
            //使用者登出
            FormsAuthentication.SignOut();
            //重新導向至登入Action
            return RedirectToAction("Login");
        }
        #endregion

        #region 抽卡
        [Authorize]
        public ActionResult Card()
        {
            CardView Data = new CardView();
            Data.Person = memberService.GetRandomPerson();
            return View(Data);
        }
        #endregion

        #region 抽卡
        [Authorize]
        public ActionResult ProfilePage()
        {
            Member Data = memberService.GetProfileData(User.Identity.Name);
            return View(Data);
        }
        #endregion
    }
}
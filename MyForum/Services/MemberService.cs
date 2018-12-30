using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using MyForum.Models;


namespace MyForum.Services
{
    public class MemberService
    {
        private MyForumDBEntities db = new MyForumDBEntities();

        #region 註冊新會員
        public bool Register(Member newMember)
        {

            try
            {
                newMember.Password = HashPassword(newMember.Password); //將密碼Hash過

                db.Member.Add(newMember);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 登入確認
        public string LoginCheck(string UserName, string Password)
        {
            Member LoginMember = db.Member.Find(UserName);

            if (LoginMember != null)//判斷是否有此會員
            {
                if (String.IsNullOrWhiteSpace(LoginMember.AuthCode)) //判斷是否有經過信箱驗證(驗證碼欄位會被清空)
                {
                    if (PasswordCheck(LoginMember, Password))//進行帳號密碼確認
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼輸入錯誤";
                    }
                }
                else
                {
                    return "此帳號尚未經過Email驗證，請去收信";
                }
            }
            else
            {
                return "無此會員帳號，請去註冊";
            }
        }
        #endregion

        #region 密碼確認
        public bool PasswordCheck(Member CheckMember, string Password)
        {
            return CheckMember.Password.Equals(HashPassword(Password));
        }
        #endregion

        #region 信箱驗證碼驗證
        public string EmailValidate(string UserName, string AuthCode)
        {
            Member ValidateMember = db.Member.Find(UserName);

            string ValidateStr = string.Empty; //驗證後訊息字串

            if (ValidateMember != null)
            {
                if (ValidateMember.AuthCode == AuthCode) //判斷驗證碼是否符合
                {
                    ValidateMember.AuthCode = string.Empty;//將資料庫中的驗證碼設為空

                    db.SaveChanges();

                    ValidateStr = "帳號信箱驗證成功，現在可以登入了";
                }
                else
                {
                    ValidateStr = "驗證碼錯誤，請重新確認或再註冊";
                }
            }
            else
            {
                ValidateStr = "傳送資料錯誤，請重新確認或再註冊";
            }

            return ValidateStr;
        }
        #endregion

        #region 帳號註冊重複確認
        public bool AccountCheck(string Account)
        {
            Member Search = db.Member.Find(Account);
            return Search == null; //true代表可以註冊
        }
        #endregion

        #region 變更密碼
        public string ChangePassword(string UserName, string Password, string newPassword)
        {
            Member LoginMember = db.Member.Find(UserName);

            if (PasswordCheck(LoginMember, Password))//確認舊密碼正確性
            {
                LoginMember.Password = HashPassword(newPassword);

                db.SaveChanges();

                return "密碼修改成功";
            }
            else
            {
                return "舊密碼輸入錯誤";
            }
        }

        #endregion

        #region Hash密碼
        public string HashPassword(string Password)
        {
            //加鹽
            string salt = "AEhFFc7ApFaHQBG4Uegha593N4hwvv5f";
            string saltAndPassword = String.Concat(salt, Password);

            //SHA1
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();

            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);//換成byte資料
            byte[] HashDate = sha1Hasher.ComputeHash(PasswordData);

            string Hashresult = ""; //換回string資料
            for (int i = 0; i < HashDate.Length; i++)
            {
                Hashresult += HashDate[i].ToString("x2");
            }

            return Hashresult;
        }
        #endregion

        #region 取得會員的權限角色
        public string GetRole(string UserName)
        {
            //宣告初始角色字串
            string Role = "User";
            
            Member LoginMember = db.Member.Find(UserName);
            if (LoginMember.IsAdmin) //添加Admin
                Role += ",Admin";

            return Role;
        }
        #endregion

        #region 隨機取得一個使用者資料(抽卡用)
        public Member GetRandomPerson()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            List<Member> Data = db.Member.ToList();

            Member Person = Data.ElementAt(random.Next(0, Data.Count()));

            return Person;
        }
        #endregion

        #region 取得個人資料
        public Member GetProfileData(string Account)
        {
            //return db.Member.Where(p => p.Account == Account).ToArray().ElementAt(0);
            return db.Member.Find(Account);

        }
        #endregion
    }
}
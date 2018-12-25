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
        //宣告資料庫實體模型物件
        private MyForumDBEntities db = new MyForumDBEntities();

        #region 註冊
        //註冊新會員方法
        public void Register(Member newMember)
        {
            //將密碼Hash過
            newMember.Password = HashPassword(newMember.Password);
            //將資料加入資料庫實體
            db.Member.Add(newMember);
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion

        #region 登入確認
        //登入帳密確認方法，並回傳驗證後訊息
        public string LoginCheck(string UserName, string Password)
        {
            //取得傳入帳號的會員資料
            Member LoginMember = db.Member.Find(UserName);
            //判斷是否有此會員
            if (LoginMember != null)
            {
                //判斷是否有經過信箱驗證，有經驗證驗證碼欄位會被清空
                if (String.IsNullOrWhiteSpace(LoginMember.AuthCode))
                {
                    //進行帳號密碼確認
                    if (PasswordCheck(LoginMember, Password))
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
        //進行密碼確認方法
        public bool PasswordCheck(Member CheckMember, string Password)
        {
            //判斷資料庫裡的密碼資料與傳入密碼資料Hash後是否一樣
            bool result = CheckMember.Password.Equals(HashPassword(Password));
            //回傳結果
            return result;
        }
        #endregion

        #region 信箱驗證
        //信箱驗證碼驗證方法
        public string EmailValidate(string UserName, string AuthCode)
        {
            //取得傳入帳號的會員資料
            Member ValidateMember = db.Member.Find(UserName);
            //宣告驗證後訊息字串
            string ValidateStr = string.Empty;
            if (ValidateMember != null)
            {
                //判斷傳入驗證碼與資料庫中是否相同
                if (ValidateMember.AuthCode == AuthCode)
                {
                    //將資料庫中的驗證碼設為空
                    ValidateMember.AuthCode = string.Empty;
                    //儲存資料庫變更
                    db.SaveChanges();
                    //設定驗證訊息
                    ValidateStr = "帳號信箱驗證成功，現在可以登入了";
                }
                else
                {
                    //設定驗證訊息
                    ValidateStr = "驗證碼錯誤，請重新確認或再註冊";
                }
            }
            else
            {
                ValidateStr = "傳送資料錯誤，請重新確認或再註冊";
            }
            //回傳驗證訊息
            return ValidateStr;
        }
        #endregion

        #region 帳號註冊重複確認
        //確認要註冊帳號是否有被註冊過的方法
        public bool AccountCheck(string Account)
        {
            //藉由傳入帳號取得會員資料
            Member Search = db.Member.Find(Account);
            //判斷是否有查詢到會員
            bool result = (Search == null);
            //回傳結果
            return result;
        }
        #endregion

        #region 變更密碼
        //變更會員密碼方法，並回傳最後訊息
        public string ChangePassword(string UserName, string Password, string newPassword)
        {
            //取得傳入帳號的會員資料
            Member LoginMember = db.Member.Find(UserName);
            //確認舊密碼正確性
            if (PasswordCheck(LoginMember, Password))
            {
                //將新密碼Hash後寫入資料庫中
                LoginMember.Password = HashPassword(newPassword);
                //儲存資料庫變更
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
        //Hash密碼用的方法
        public string HashPassword(string Password)
        {
            //宣告Hash時所添加的無意義亂數值
            string saltkey = "sE7T8E4Wr81sd9qWQG3Jjgio8tj3kg";
            //將剛剛宣告的字串與密碼結合
            string saltAndPassword = String.Concat(Password, saltkey);
            //定義SHA1的HASH物件
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            //取得密碼轉換成byte資料
            byte[] PasswordData = Encoding.Default.GetBytes(saltAndPassword);
            //取得Hash後byte資料
            byte[] HashDate = sha1Hasher.ComputeHash(PasswordData);
            //將Hash後byte資料轉換成string
            string Hashresult = "";
            for (int i = 0; i < HashDate.Length; i++)
            {
                Hashresult += HashDate[i].ToString("x2");
            }
            //回傳Hash後結果
            return Hashresult;
        }
        #endregion

        #region 取得角色
        //取得會員的權限角色資料
        public string GetRole(string UserName)
        {
            //宣告初始角色字串
            string Role = "User";
            //取得傳入帳號的會員資料
            Member LoginMember = db.Member.Find(UserName);
            //判斷資料庫欄位，用以確認是否為Admon
            if (LoginMember.IsAdmin)
                Role += ",Admin"; //添加Admin
            //回傳最後結果
            return Role;
        }
        #endregion

    }
}
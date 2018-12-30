using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;


namespace MyForum.Services
{
    public class MailService
    {
        private string gmail_account = "turtalk1111@gmail.com"; //Gmail帳號
        private string gmail_password = "Zxcv*963"; //Gmail密碼
        private string gmail_mail = "turtalk1111@gmail.com"; //Gmail信箱

        #region 寄會員驗證信
        //產生驗證碼
        public string GetValidateCode()
        {
            //設定驗證碼字元的陣列
            string[] Code ={ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L","M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                             "a", "b", "c", "d","e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                             "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            //驗證碼字串
            string ValidateCode = string.Empty;

            Random rd = new Random();
            for (int i = 0; i < 20; i++)
                ValidateCode += Code[rd.Next(Code.Count())];

            return ValidateCode;
        }

        //將使用者資料填入驗證信範本中
        public string GetRegisterMailBody(string TempString, string UserName, string ValidateUrl)
        {
            TempString = TempString.Replace("{{UserName}}", UserName);
            TempString = TempString.Replace("{{ValidateUrl}}", ValidateUrl);

            return TempString;
        }

        //寄驗證信
        public void SendRegisterMail(string MailBody, string ToEmail)
        {
            //建立寄信用Smtp物件
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587; 
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            SmtpServer.EnableSsl = true; //開啟SSL 

            //信件
            MailMessage mail = new MailMessage(); 
            mail.From = new MailAddress(gmail_mail); //來源信箱
            mail.To.Add(ToEmail); //收信者
            mail.Subject = "TurTalk-會員註冊確認信"; //主旨
            mail.Body = MailBody; //設定信件內容 
            mail.IsBodyHtml = true; //設定信件內容為HTML格式 

            SmtpServer.Send(mail); //送出信件
        }
        #endregion
    }
}
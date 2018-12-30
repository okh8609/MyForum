using MyForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyForum.Services
{
    public class AdvertisementService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢廣告內容
        //傳入所選取的價位
        //隨機選出一筆廣告
        public Advertisement RandomChoice(int Price)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            var Data = db.Advertisement.Where(p => p.Price == Price).ToList();

            return Data.ElementAt(random.Next(0, Data.Count()));
        }
        #endregion

        #region 上傳檔案
        //加入廣告內容
        public void UploadFile(string PictPath, string URL, int Price, string Account)
        {
            Advertisement newFile = new Advertisement();
            //設定內容
            newFile.PictPath = PictPath;
            newFile.URL = URL;
            newFile.EXP = DateTime.Now.AddDays(7); //將廣告的期限設為一周後
            newFile.Price = Price;
            newFile.Account = Account;

            //新增至資料庫中
            db.Advertisement.Add(newFile);
            db.SaveChanges();
        }
        #endregion

        #region 購買廣告
        public bool BuyAd(string Account, int Price)
        {
            if (db.Member.Find(Account).Coins < Price)
                return false;

            db.Member.Find(Account).Coins -= Price;
            db.SaveChanges();
            return true;
        }
        #endregion


        #region 取得廣告
        public List<Advertisement> GetAd()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            List<Advertisement> result = new List<Advertisement>();

            var data1 = db.Advertisement.Where(p => p.Price == 50).ToList(); //50元的廣告
            result.Add(data1.ElementAt(random.Next(0, data1.Count())));

            var data2 = db.Advertisement.Where(p => p.Price == 25).ToList(); //25元的廣告
            result.Add(data2.ElementAt(random.Next(0, data2.Count())));
            
            return result;
        }
        #endregion

    }
}
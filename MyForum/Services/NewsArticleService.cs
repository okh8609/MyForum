using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.Services
{
    public class NewsArticleService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢相關
        #region 查詢一筆文章
        public NewsArticle GetDataById(int NA_ID)
        {
            return db.NewsArticle.Find(NA_ID);
        }
        #endregion

        #region 查詢文章陣列資料
        public List<NewsArticle> GetDataList(string Account)
        {
            IEnumerable<NewsArticle> SearchData;
            SearchData = GetAllDataList(Account).AsEnumerable();
            //return RandomShuffle(SearchData).ToList();
            return SearchData.OrderByDescending(p => p.CreateTime).ToList(); //依照時間排序

        }
        private IQueryable<NewsArticle> GetAllDataList(string Account)
        {
            //先找到自己的好友有誰
            IQueryable<Friendship> FriendList = db.Friendship.Where(p => p.Account_a.Equals(Account));
            //依據Accout去找到好友發的動態
            IQueryable<NewsArticle> Data = db.NewsArticle.Where(p => p.Account.Equals(Account));//自己的也要
            //把它union起來
            foreach (Friendship f in FriendList)
            {
                Data = Data.Union(db.NewsArticle.Where(p => p.Account.Equals(f.Account_b)));
            }
            return Data;
        }
        ////Shuffle IEnumerable to array
        //public static void Swap<T>(ref T x, ref T y)
        //{
        //    var temp = x;
        //    x = y;
        //    y = temp;
        //}
        //public static T[] RandomShuffle<T>(this IEnumerable<T> source)
        //{
        //    var result = source.ToArray();
        //    if (result.Length < 2) return result;
        //    var random = new Random();
        //    for (int i = result.Length - 1; i > 0; i--)
        //    {
        //        int pos = random.Next(i + 1);
        //        if (pos != i)
        //            Swap(ref result[pos], ref result[i]);
        //    }
        //    return result;
        //}
        #endregion
        #endregion

        #region 新增動態
        public void Insert(NewsArticle newData)
        {
            newData.CreateTime = DateTime.Now;

            db.NewsArticle.Add(newData);
            db.SaveChanges();
        }
        #endregion

        #region 刪除資料
        public void Delete(int Id)
        {
            NewsArticle DeleteData = db.NewsArticle.Find(Id);
            List<NewsMessage> DeleteMessageList = DeleteData.NewsMessage.ToList();

            //刪除文章裡的留言
            foreach (NewsMessage Delete in DeleteMessageList)
            {
                db.NewsMessage.Remove(Delete);
            }

            db.SaveChanges();
            db.NewsArticle.Remove(DeleteData);
            db.SaveChanges();
        }
        #endregion

        #region 加觀看人數
        public void Watch(int Id)
        {
            NewsArticle LikeData = db.NewsArticle.Find(Id);
            LikeData.Watch += 1;
            db.SaveChanges();
        }
        #endregion

        #region 加台科幣
        public void Coin(int AID)
        {
            NewsArticle LikeData = db.NewsArticle.Find(AID);
            LikeData.Member.Coins += 1;
            db.SaveChanges();
        }
        #endregion

    }
}
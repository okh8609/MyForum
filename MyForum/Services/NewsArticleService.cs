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
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢相關
        #region 查詢一筆文章
        //藉由標號取的單筆資料的方法
        public NewsArticle GetDataById(int NA_ID)
        {
            //回傳根據標號所取得的資料
            return db.NewsArticle.Find(NA_ID);
        }
        #endregion

        #region 查詢文章陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<NewsArticle> GetDataList(string Account)
        {
            IEnumerable<NewsArticle> SearchData;
            SearchData = GetAllDataList(Account).AsEnumerable();
            //return RandomShuffle(SearchData).ToList();
            return SearchData.OrderByDescending(p => p.CreateTime).ToList();//依照時間排序

        }
        //包含搜尋值的搜尋資料方法
        private IQueryable<NewsArticle> GetAllDataList(string Account)
        {
            //先找到自己的好友有誰
            IQueryable<Friendship> FriendList = db.Friendship.Where(p => p.Account_a.Equals(Account));
            //依據Accout去找到好友發的動態
            IQueryable<NewsArticle> Data = db.NewsArticle.Where(p => p.Account.Equals("NULL_qPDdUDCKYh8WQHY2"));//回傳空的列表
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
        //新增文章方法
        public void Insert(NewsArticle newData)
        {
            //設定新增時間為現在
            newData.CreateTime = DateTime.Now;
            //將資料加入資料庫實體
            db.NewsArticle.Add(newData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion

        #region 刪除資料
        //刪除文章方法
        public void Delete(int Id)
        {
            //根據Id取得要刪除的資料
            NewsArticle DeleteData = db.NewsArticle.Find(Id);
            List<NewsMessage> DeleteMessageList = DeleteData.NewsMessage.ToList();
            //刪除文章裡的留言
            foreach (NewsMessage Delete in DeleteMessageList)
            {
                db.NewsMessage.Remove(Delete);
            }
            //先儲存變更，以便能刪除文章
            db.SaveChanges();
            //從資料庫實體中刪除資料
            db.NewsArticle.Remove(DeleteData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion

        #region 加觀看人數
        public void Watch(int Id)
        {
            //根據Id取得要刪除的資料
            NewsArticle LikeData = db.NewsArticle.Find(Id);
            //資料庫內觀看人數加1
            LikeData.Watch += 1;
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion
    }
}
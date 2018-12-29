using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.Services
{
    public class ArticleService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢相關
        #region 查詢一筆文章
        //藉由標號取的單筆資料的方法
        public Article GetDataById(int Id)
        {
            //回傳根據標號所取得的資料
            return db.Article.Find(Id);
        }
        #endregion

        #region 查詢文章陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Article> GetDataList(ForPaging Paging, string Search)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<Article> SearchData;
            //判斷搜尋是否為空或Null，以決定要呼叫設定搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.A_Id)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<Article> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<Article> Data = db.Article;
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        //包含搜尋值的搜尋資料方法
        public IQueryable<Article> GetAllDataList(ForPaging Paging, string Search)
        {
            //根據搜尋值來搜尋資料
            IQueryable<Article> Data = db.Article
             .Where(p => p.Member.Name.Contains(Search) || p.Content.Contains(Search) || p.Title.Contains(Search));
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                 Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        #endregion
        #region 根據看板ID找尋文章列表
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Article> GetDataList(ForPaging Paging, int B_Id)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<Article> SearchData = GetAllDataList(Paging, B_Id);
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.A_Id)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        //搜尋全部資料方法
        public IQueryable<Article> GetAllDataList(ForPaging Paging, int B_Id)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<Article> Data = db.Article.Where(model => model.B_Id == B_Id);
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        #endregion
        #endregion

        #region 存檔
        public void Save()
        {
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion

        #region 新增文章
        //新增文章方法
        public void Insert(Article newData)
        {
            //設定新增時間為現在
            newData.CreateTime = DateTime.Now;
            //將資料加入資料庫實體
            db.Article.Add(newData);
            Save();//儲存資料庫變更
        }
        #endregion

        #region 刪除資料
        //刪除文章方法
        public void Delete(int Id)
        {
            //根據Id取得要刪除的資料
            Article DeleteData = db.Article.Find(Id);
            List<Message> DeleteMessageList = DeleteData.Message.ToList();
            //刪除文章裡的留言
            foreach (Message Delete in DeleteMessageList)
            {
                db.Message.Remove(Delete);
            }
            //先儲存變更，以便能刪除文章
            Save();
            //從資料庫實體中刪除資料
            db.Article.Remove(DeleteData);
            Save();//儲存資料庫變更
        }
        #endregion

        #region 修改檢查
        //修改文章檢查判斷的方法
        public bool CheckUpdate(int Id)
        {
            //根據Id取得要修改的資料
            Article Data = db.Article.Find(Id);
            //判斷並回傳(判斷是否有資料以及是否有回覆)
            return (Data != null && Data.Message.Count == 0);
        }
        #endregion

        #region 人氣相關
        #region 人氣資料
        public List<Article> GetPopularityDataList()
        {
            //回傳人氣資料根據Like欄位遞減排序，並取得前5筆資料
            return db.Article.OrderByDescending(p => p.Watch).Take(5).ToList();
        }
        #endregion

        #region 加觀看人數
        public void Watch(int Id)
        {
            //根據Id取得要刪除的資料
            Article LikeData = db.Article.Find(Id);
            //資料庫內觀看人數加1
            LikeData.Watch += 1;
            //儲存資料庫變更
            Save();
        }
        #endregion
        #endregion
    }
}
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
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢相關
        #region 查詢一筆文章
        //取單筆資料
        public Article GetDataById(int Id)
        {
            return db.Article.Find(Id);
        }
        #endregion

        #region 查詢文章陣列資料
        //取得資料陣列
        public List<Article> GetDataList(ForPaging Paging, string Search)
        {
            IQueryable<Article> SearchData;

            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }
            //分頁回傳部分資料陣列
            return SearchData.OrderByDescending(p => p.A_Id)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        //無搜尋值
        public IQueryable<Article> GetAllDataList(ForPaging Paging)
        {
            IQueryable<Article> Data = db.Article;

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        //包含搜尋值
        public IQueryable<Article> GetAllDataList(ForPaging Paging, string Search)
        {
            IQueryable<Article> Data = db.Article
             .Where(p => p.Member.Name.Contains(Search) || p.Content.Contains(Search) || p.Title.Contains(Search));

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion

        #region 根據看板ID找尋文章列表
        public List<Article> GetDataList(ForPaging Paging, int B_Id)
        {
            IQueryable<Article> SearchData = GetAllDataList(Paging, B_Id);
            return SearchData.OrderByDescending(p => p.A_Id)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        public IQueryable<Article> GetAllDataList(ForPaging Paging, int B_Id)
        {
            IQueryable<Article> Data = db.Article.Where(model => model.B_Id == B_Id);

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion
        #endregion

        #region 根據看板ID找尋文章列表
        public string GetBoardTitle(int B_Id)
        {
            var a = db.Board.Find(B_Id);
            if (a == null)
                return "";
            else
                return a.B_name;
        }
        #endregion

        #region 儲存資料庫變更
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion

        #region 新增文章
        public void Insert(Article newData)
        {
            newData.CreateTime = DateTime.Now;
            db.Article.Add(newData);
            Save();
        }
        #endregion

        #region 刪除資料
        public void Delete(int Id)
        {
            Article DeleteData = db.Article.Find(Id);
            List<Message> DeleteMessageList = DeleteData.Message.ToList();

            //刪除文章裡的留言
            foreach (Message Delete in DeleteMessageList)
            {
                db.Message.Remove(Delete);
            }

            Save();
            db.Article.Remove(DeleteData);
            Save();
        }
        #endregion

        #region 修改文章檢查判斷
        public bool CheckUpdate(int Id)
        {
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
            Article LikeData = db.Article.Find(Id);
            //資料庫內觀看人數加1
            LikeData.Watch += 1;
            Save();
        }
        #endregion
        #endregion

        #region 加台科幣
        public void Coin(int AID)
        {
            Article LikeData = db.Article.Find(AID);
            LikeData.Member.Coins += 1;
            db.SaveChanges();
        }
        #endregion
    }
}
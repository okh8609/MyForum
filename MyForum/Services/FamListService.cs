using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Sockets;


namespace MyForum.Services
{
    public class FamListService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢一個文章
        //藉由標號取的單筆資料的方法
        public FamList GetDataById(int Id)
        {
            //回傳根據標號所取得的資料
            return db.FamList.Find(Id);
        }
        #endregion

        #region 查詢文章陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<FamList> GetDataList(ForPaging Paging)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<FamList> SearchData;
            SearchData = GetAllDataList(Paging);
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.FB_ID)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<FamList> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<FamList> Data = db.FamList;
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        #endregion

        #region 新增家族
        //新增文章方法
        public void Insert(FamList newData)
        {
            //將資料加入資料庫實體
            db.FamList.Add(newData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion

        #region 刪除資料
        //刪除文章方法
        public void Delete(int Id)
        {
            //根據Id取得要刪除的資料
            FamList DeleteData = db.FamList.Find(Id);
            List<FamArti> DeleteArticleList = DeleteData.FamArti.ToList();
            //刪除文章裡的留言
            FamArtiService articleService = new FamArtiService();
            foreach (FamArti aa in DeleteArticleList)
            {
                articleService.Delete(aa.FA_ID);
            }
            //先儲存變更，以便能刪除文章
            db.SaveChanges();
            //從資料庫實體中刪除資料
            db.FamList.Remove(DeleteData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion


    }
}
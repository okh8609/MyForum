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
        public FamList GetDataById(int Id)
        {
            return db.FamList.Find(Id);
        }
        #endregion

        #region 查詢文章陣列資料
        public List<FamList> GetDataList(ForPaging Paging)
        {
            IQueryable<FamList> SearchData;

            SearchData = GetAllDataList(Paging);

            return SearchData.OrderByDescending(p => p.FB_ID)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        public IQueryable<FamList> GetAllDataList(ForPaging Paging)
        {
            IQueryable<FamList> Data = db.FamList;

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion

        #region 新增家族
        public void Insert(FamList newData)
        {
            db.FamList.Add(newData);
            db.SaveChanges();
        }
        #endregion

        #region 刪除資料
        public void Delete(int Id)
        {
            FamList DeleteData = db.FamList.Find(Id);
            List<FamArti> DeleteArticleList = DeleteData.FamArti.ToList();

            //刪除文章裡的留言
            FamArtiService articleService = new FamArtiService();
            foreach (FamArti aa in DeleteArticleList)
            {
                articleService.Delete(aa.FA_ID);
            }

            db.SaveChanges();
            db.FamList.Remove(DeleteData);
            db.SaveChanges();
        }
        #endregion


    }
}
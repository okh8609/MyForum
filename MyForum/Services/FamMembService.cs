using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.Services
{
    public class FamMembService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 新增訂閱(加入)的家族
        public void Add(string Account, int FB_ID)
        {
            FamMemb famMemb = new FamMemb();
            famMemb.Account = Account;
            famMemb.FB_ID = FB_ID;
            db.FamMemb.Add(famMemb);
            db.SaveChanges();
        }
        #endregion

        #region 查詢訂閱的家族的文章
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<FamArti> GetDataList(string Account)
        {
            IEnumerable<FamArti> SearchData;
            SearchData = GetAllDataList(Account).AsEnumerable();
            //return RandomShuffle(SearchData).ToList();
            return SearchData.OrderByDescending(p => p.CreateTime).ToList();//依照時間排序

        }
        //包含搜尋值的搜尋資料方法
        private IQueryable<FamArti> GetAllDataList(string Account)
        {
            //查到你有訂閱那些家族
            IQueryable<FamMemb> BookingRecord = db.FamMemb.Where(p => p.Account.Equals(Account));

            //取得所有訂閱的看板
            IQueryable<FamList> BookingBoard = db.FamList.Where(p => p.FB_ID.Equals(-1));
            foreach (FamMemb b in BookingRecord)
            {
                BookingBoard = BookingBoard.Union(db.FamList.Where(p => p.FB_ID.Equals(b.FB_ID)));
            }

            //取得所有訂閱的文章
            IQueryable<FamArti> BookingArticle = db.FamArti.Where(p => p.FB_ID.Equals(-1));
            foreach (FamList b in BookingBoard)
            {
                BookingArticle = BookingArticle.Union(db.FamArti.Where(p => p.FB_ID.Equals(b.FB_ID)));
            }
            
            return BookingArticle;
        }
        #endregion

    }
}
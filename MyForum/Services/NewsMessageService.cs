using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;

namespace MyForum.Services
{
    public class NewsMessageService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢留言陣列資料
        public List<NewsMessage> GetDataList(int NA_Id)
        {
            IQueryable<NewsMessage> SearchData = db.NewsMessage.Where(model => model.NA_ID == NA_Id);
            return SearchData.OrderBy(p => p.NA_ID).ToList();
        }
        #endregion

        #region 新增文章留言
        public void Insert(NewsMessage newData)
        {
            newData.CreateTime = DateTime.Now;
            db.NewsMessage.Add(newData); 
            db.SaveChanges();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;


namespace MyForum.Services
{
    public class FamMsgService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢留言陣列資料
        public List<FamMsg> GetDataList(int FA_ID)
        {
            IQueryable<FamMsg> SearchData = db.FamMsg.Where(model => model.FA_ID == FA_ID);
            return SearchData.OrderBy(p => p.FA_ID).ToList();
        }
        #endregion

        #region 新增文章留言
        public void Insert(FamMsg newData)
        {
            newData.CreateTime = DateTime.Now;
            db.FamMsg.Add(newData); 
            db.SaveChanges();
        }
        #endregion
    }
}
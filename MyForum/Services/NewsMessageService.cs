using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;

namespace MyForum.Services
{
    public class NewsMessageService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢留言陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<NewsMessage> GetDataList(int NA_Id)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<NewsMessage> SearchData = db.NewsMessage.Where(model => model.NA_ID == NA_Id);
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.NA_ID).ToList();
        }
        #endregion

        #region 新增文章留言
        //新增文章留言方法
        public void Insert(NewsMessage newData)
        {
            //設定新增時間為現在
            newData.CreateTime = DateTime.Now;
            db.NewsMessage.Add(newData); //將資料加入資料庫實體
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion
    }
}
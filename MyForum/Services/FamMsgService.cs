using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;


namespace MyForum.Services
{
    public class FamMsgService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢留言陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<FamMsg> GetDataList(ForPaging Paging, int A_Id)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<FamMsg> SearchData = GetAllDataList(Paging, A_Id);
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.FM_Id)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        //搜尋全部資料方法
        public IQueryable<FamMsg> GetAllDataList(ForPaging Paging, int FA_ID)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<FamMsg> Data = db.FamMsg.Where(model => model.FA_ID == FA_ID);
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        #endregion

        #region 新增文章留言
        //新增文章留言方法
        public void Insert(FamMsg newData)
        {
            //設定新增時間為現在
            newData.CreateTime = DateTime.Now;
            db.FamMsg.Add(newData); //將資料加入資料庫實體
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion
    }
}
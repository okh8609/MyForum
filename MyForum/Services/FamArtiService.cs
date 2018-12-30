using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.Services
{
    public class FamArtiService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢一筆文章
        //藉由標號取的單筆資料的方法
        public FamArti GetDataById(int Id)
        {
            //回傳根據標號所取得的資料
            return db.FamArti.Find(Id);
        }
        #endregion

        #region 根據看板ID找尋文章列表
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<FamArti> GetDataList(ForPaging Paging, int FB_ID)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<FamArti> SearchData = GetAllDataList(Paging, FB_ID);
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.CreateTime)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        //搜尋全部資料方法
        public IQueryable<FamArti> GetAllDataList(ForPaging Paging, int FB_ID)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<FamArti> Data = db.FamArti.Where(model => model.FB_ID == FB_ID);
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
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
        public void Insert(FamArti newData)
        {
            //設定新增時間為現在
            newData.CreateTime = DateTime.Now;
            //將資料加入資料庫實體
            db.FamArti.Add(newData);
            Save();//儲存資料庫變更
        }
        #endregion

        #region 刪除資料
        //刪除文章方法
        public void Delete(int Id)
        {
            //根據Id取得要刪除的資料
            FamArti DeleteData = db.FamArti.Find(Id);
            List<FamMsg> DeleteMessageList = DeleteData.FamMsg.ToList();
            //刪除文章裡的留言
            foreach (FamMsg Delete in DeleteMessageList)
            {
                db.FamMsg.Remove(Delete);
            }
            //先儲存變更，以便能刪除文章
            Save();
            //從資料庫實體中刪除資料
            db.FamArti.Remove(DeleteData);
            Save();//儲存資料庫變更
        }
        #endregion

        #region 修改檢查
        //修改文章檢查判斷的方法
        public bool CheckUpdate(int Id)
        {
            //根據Id取得要修改的資料
            FamArti Data = db.FamArti.Find(Id);
            //判斷並回傳(判斷是否有資料以及是否有回覆)
            return (Data != null && Data.FamMsg.Count == 0);
        }
        #endregion

        #region 加觀看人數
        public void Watch(int Id)
        {
            FamArti LikeData = db.FamArti.Find(Id);
            LikeData.Watch += 1;
            Save();
        }
        #endregion

        #region 加台科幣
        public void Coin(int AID)
        {
            FamArti LikeData = db.FamArti.Find(AID);
            LikeData.Member.Coins += 1;
            Save();
        }
        #endregion
    }
}
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
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢一筆文章
        public FamArti GetDataById(int Id)
        {
            return db.FamArti.Find(Id);
        }
        #endregion

        #region 根據看板ID找尋文章列表
        public List<FamArti> GetDataList(ForPaging Paging, int FB_ID)
        {
            IQueryable<FamArti> SearchData = GetAllDataList(Paging, FB_ID);
            return SearchData.OrderByDescending(p => p.CreateTime)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        public IQueryable<FamArti> GetAllDataList(ForPaging Paging, int FB_ID)
        {
            IQueryable<FamArti> Data = db.FamArti.Where(model => model.FB_ID == FB_ID);

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion

        #region 根據看板ID找尋文章列表
        public string GetBoardTitle(int B_Id)
        {
            var a = db.FamList.Find(B_Id);
            if (a == null)
                return "";
            else
                return a.FB_name;
        }
        #endregion

        #region 存檔
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion

        #region 新增文章
        public void Insert(FamArti newData)
        {
            newData.CreateTime = DateTime.Now;

            db.FamArti.Add(newData);
            Save();
        }
        #endregion

        #region 刪除資料
        public void Delete(int Id)
        {
            FamArti DeleteData = db.FamArti.Find(Id);
            List<FamMsg> DeleteMessageList = DeleteData.FamMsg.ToList();

            //刪除文章裡的留言
            foreach (FamMsg Delete in DeleteMessageList)
            {
                db.FamMsg.Remove(Delete);
            }

            Save();
            db.FamArti.Remove(DeleteData);
            Save();
        }
        #endregion

        #region 修改檢查
        public bool CheckUpdate(int Id)
        {
            FamArti Data = db.FamArti.Find(Id);
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
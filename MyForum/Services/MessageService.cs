using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;

namespace MyForum.Services
{
    public class MessageService
    {
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢留言陣列資料
        public List<Message> GetDataList(ForPaging Paging, int A_Id)
        {
            IQueryable<Message> SearchData = GetAllDataList(Paging, A_Id);

            return SearchData.OrderByDescending(p => p.M_Id)
            .Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        public IQueryable<Message> GetAllDataList(ForPaging Paging, int A_Id)
        {
            IQueryable<Message> Data = db.Message.Where(model => model.A_Id == A_Id);

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling( Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion

        #region 新增文章留言
        public void Insert(Message newData)
        {
            newData.CreateTime = DateTime.Now;

            db.Message.Add(newData);
            db.SaveChanges();
        }
        #endregion
    }
}
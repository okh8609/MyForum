using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;

namespace MyForum.Services
{
    public class FriendshipService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢Account的好友陣列
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Member> GetMyFriendList(string Account)
        {
            IQueryable<Member> SearchData;
            SearchData = GetMyFriend(Account);
            return SearchData.OrderByDescending(p => p.Account).ToList();
        }
        //包含搜尋值的搜尋資料方法
        private IQueryable<Member> GetMyFriend(string Search)
        {
            //先找到自己的好友有誰
            IQueryable<Friendship> FriendList = db.Friendship.Where(p => p.Account_a.Equals(Search));
            //依據Accout去找到人
            IQueryable<Member> Data = db.Member.Where(p => p.Account.Equals("NULL_qPDdUDCKYh8WQHY2"));//回傳空的列表
            //把它union起來
            foreach (Friendship f in FriendList)
            {
                Data = Data.Union(db.Member.Where(p => p.Account.Equals(f.Account_b)));               
            }
            return Data;
        }
        #endregion

        #region 依照名稱搜尋好友資料(搜尋功能)
        //依照名稱搜尋好友資料(搜尋功能)
        public List<Member> GetSearchFriendList(string Search)
        {
            //判斷搜尋是否為空或Null，以決定要呼叫設定搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                return new List<Member>();
            }
            else
            {
                //設定要接受全部搜尋資料的物件
                IQueryable<Member> SearchData;
                SearchData = db.Member.Where(p => p.Account.Contains(Search) || p.Email.Contains(Search));//找Account或Email包含字串的東西
                return SearchData.OrderByDescending(p => p.Account).ToList();
            }
        }
        #endregion

        #region 新增好友
        //單向(類似追蹤)
        //傳入兩個UID
        public void Add(string p1, string p2)
        {
            if (db.Member.Where(p => p.Account.Equals(p2)).Any())
            {
                Friendship friendship = new Friendship();
                friendship.Account_a = p1;
                friendship.Account_b = p2;
                //將資料加入資料庫實體
                db.Friendship.Add(friendship);
                //儲存資料庫變更
                db.SaveChanges();
            }
        }
        #endregion
    }
}
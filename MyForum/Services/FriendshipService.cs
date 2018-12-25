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

        #region 查詢好友陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Member> GetDataList(string Search)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<Member> SearchData;
            //判斷搜尋是否為空或Null，以決定要呼叫設定搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList();
            }
            else
            {
                SearchData = GetAllDataList(Search);
            }
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.Account).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<Member> GetAllDataList()
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<Member> Data = db.Member;
            //回傳搜尋資料
            return Data;
        }
        //包含搜尋值的搜尋資料方法
        public IQueryable<Member> GetAllDataList(string Search)
        {
            //根據搜尋值來搜尋資料
            IQueryable<Member> Data = db.Member
             .Where(p => p.Account.Contains(Search) || p.Email.Contains(Search));
            //回傳搜尋資料
            return Data;
        }
        #endregion

        #region 新增好友
        //新增文章方法
        public void Add(Member p1, Member p2)
        {
            Friendship friendship = new Friendship();
            friendship.Account_a = p1.Account;
            friendship.Account_b = p2.Account;
            //將資料加入資料庫實體
            db.Friendship.Add(friendship);
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion
    }
}
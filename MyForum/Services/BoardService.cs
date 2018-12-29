using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyForum.Models;
using MyForum.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Sockets;


namespace MyForum.Services
{
    public class BoardService
    {
        //宣告資料庫實體模型物件
        MyForumDBEntities db = new MyForumDBEntities();

        #region 查詢資料庫是否存在
        //public bool DBexist()
        //{
        //    //var s = ConfigurationManager.ConnectionStrings["MyForumDBEntities"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection())
        //    {
        //        try
        //        {
        //            con.Open();
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool TryConnectServerByPort(string hostIP, int port)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostIP);
                IPAddress ip = host.AddressList[0];
                TcpClient tcp = new TcpClient();
                tcp.Connect(ip, port);
                tcp.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 查詢一個文章
        //藉由標號取的單筆資料的方法
        public Board GetDataById(int Id)
        {
            //回傳根據標號所取得的資料
            return db.Board.Find(Id);
        }
        #endregion

        #region 查詢文章陣列資料
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Board> GetDataList(ForPaging Paging, string Search)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<Board> SearchData;
            //判斷搜尋是否為空或Null，以決定要呼叫設定搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }
            //先排序再根據分頁來回傳所需的部分資料陣列
            return SearchData.OrderByDescending(p => p.B_Id)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<Board> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
            IQueryable<Board> Data = db.Board;
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        //包含搜尋值的搜尋資料方法
        public IQueryable<Board> GetAllDataList(ForPaging Paging, string Search)
        {
            //根據搜尋值來搜尋資料
            IQueryable<Board> Data = db.Board
             .Where(p => p.Member.Name.Contains(Search) || p.B_name.Contains(Search));
            //計算所需的總頁面
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                 Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            //回傳搜尋資料
            return Data;
        }
        #endregion

        #region 新增文章
        //新增文章方法
        public void Insert(Board newData)
        {
            //將資料加入資料庫實體
            db.Board.Add(newData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion

        #region 刪除資料
        //刪除文章方法
        public void Delete(int Id)
        {
            //根據Id取得要刪除的資料
            Board DeleteData = db.Board.Find(Id);
            List<Article> DeleteArticleList = DeleteData.Article.ToList();
            //刪除文章裡的留言
            ArticleService articleService = new ArticleService();
            foreach (Article aa in DeleteArticleList)
            {
                articleService.Delete(aa.A_Id);
            }
            //先儲存變更，以便能刪除文章
            db.SaveChanges();
            //從資料庫實體中刪除資料
            db.Board.Remove(DeleteData);
            db.SaveChanges();//儲存資料庫變更
        }
        #endregion





    }
}
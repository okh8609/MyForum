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

        #region 查詢一個看板
        public Board GetDataById(int Id)
        {
            return db.Board.Find(Id);
        }
        #endregion

        #region 查詢看板陣列資料
        public List<Board> GetDataList(ForPaging Paging, string Search)
        {
            IQueryable<Board> SearchData;

            if (String.IsNullOrEmpty(Search))
            {
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging, Search);
            }

            return SearchData.OrderByDescending(p => p.B_Id)
                .Skip((Paging.NowPage - 1) * Paging.ItemNum)
                .Take(Paging.ItemNum).ToList();
        }
        public IQueryable<Board> GetAllDataList(ForPaging Paging)
        {
            IQueryable<Board> Data = db.Board;

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        public IQueryable<Board> GetAllDataList(ForPaging Paging, string Search)
        {
            IQueryable<Board> Data = db.Board
             .Where(p => p.Member.Name.Contains(Search) || p.B_name.Contains(Search));

            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            Paging.SetRightPage();

            return Data;
        }
        #endregion

        #region 新增文章
        public void Insert(Board newData)
        {
            db.Board.Add(newData);
            db.SaveChanges();
        }
        #endregion

        #region 刪除資料
        public void Delete(int Id)
        {
            Board DeleteData = db.Board.Find(Id);
            List<Article> DeleteArticleList = DeleteData.Article.ToList();

            //刪除看板裡的文章
            ArticleService articleService = new ArticleService();
            foreach (Article aa in DeleteArticleList)
            {
                articleService.Delete(aa.A_Id);
            }

            db.SaveChanges();
            db.Board.Remove(DeleteData);
            db.SaveChanges();
        }
        #endregion
    }
}
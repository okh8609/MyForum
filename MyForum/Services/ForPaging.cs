using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyForum.Services
{
    //分頁內容類別
    public class ForPaging
    {
        
        public int NowPage { get; set; }//當前頁數
        public int MaxPage { get; set; }//最大頁數


        //每一頁的文章個數
        public int ItemNum
        {
            get
            {
                return 15;
            }
        }

        //建構式
        public ForPaging()
        {
            this.NowPage = 1;//預設當前頁數為1
        }
        //建構式，傳入頁數
        public ForPaging(int Page)
        {
            this.NowPage = Page;//設定當前頁數
        }

        //校正頁數
        public void SetRightPage()
        {
            if (this.NowPage < 1)
            {
                this.NowPage = 1;
            }
            else if (this.NowPage > this.MaxPage)
            {
                this.NowPage = this.MaxPage;
            }
            if (this.MaxPage.Equals(0))
            {
                this.NowPage = 1;
            }
        }
    }
}
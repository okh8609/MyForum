﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyForum.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyForumDBEntities : DbContext
    {
        public MyForumDBEntities()
            : base("name=MyForumDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Friendship> Friendship { get; set; }
        public virtual DbSet<NewsArticle> NewsArticle { get; set; }
        public virtual DbSet<NewsMessage> NewsMessage { get; set; }
        public virtual DbSet<FamArti> FamArti { get; set; }
        public virtual DbSet<FamList> FamList { get; set; }
        public virtual DbSet<FamMemb> FamMemb { get; set; }
        public virtual DbSet<FamMsg> FamMsg { get; set; }
        public virtual DbSet<Advertisement> Advertisement { get; set; }
    }
}

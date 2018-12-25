using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MyForum.Models
{
    [MetadataType(typeof(FriendshipMetadata))]
    public partial class Friendship
    {
        public class FriendshipMetadata
        {
            [DisplayName("好友關係編號")]
            public int id { get; set; }

            [DisplayName("帳號")]
            [StringLength(30, MinimumLength = 6, ErrorMessage = "帳號長度須介於6-30字元")]
            public string Account_a { get; set; }

            [DisplayName("帳號")]
            [StringLength(30, MinimumLength = 6, ErrorMessage = "帳號長度須介於6-30字元")]
            public string Account_b { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyForum.Models;

namespace MyForum.ViewModel
{
    public class MemberRegisterView
    {
        public Member newMember { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [Compare("Password", ErrorMessage = "兩次密碼輸入不一致")]
        public string PasswordCheck { get; set; }
    }
}
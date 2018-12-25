using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyForum.ViewModel
{
    //修改密碼用ViewModel
    public class MemberChangePasswordView
    {
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string NewPassword { get; set; }

        [DisplayName("新密碼確認")]
        [Required(ErrorMessage = "請輸入密碼")]
        [Compare("NewPassword", ErrorMessage = "兩次密碼輸入不一致")]
        public string NewPasswordCheck { get; set; }
    }
}
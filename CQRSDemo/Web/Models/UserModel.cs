using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CQRSDemo.Models
{

    public class RegisterModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "密码长度必须大于6.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码不一致.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }



    public class LoginModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }

    public class UserInfoModel
    {
        public User UserInfo { get; set; }

        public List<BorrowRecordView> BorrowRecords { get; set; }
    }

    public class EditUserModel
    {
        [Required]
        public System.Guid AggregateRootId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Address_Country { get; set; }
        public string Address_State { get; set; }
        public string Address_Street { get; set; }
        public string Address_City { get; set; }
        public string Address_Zip { get; set; }
    }
}
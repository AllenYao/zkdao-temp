using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using zic_dotnet;

namespace zkdao.Web.UserServiceReference {

    public partial class UserData {

        public static string GetHashPassword(string value) {
            return Encrypt.EncryptUserPassword(value);
        }
    }

    public class UserRegister {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email 电子邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 最少需要 {2} 位字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "{0} 不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class UserLogOn {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email 电子邮箱")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string LogOnPassword { get; set; }

        public bool RememberMe { get; set; }
    }

    public class UserChangePassword {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email 电子邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 最少需要 {2} 位字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
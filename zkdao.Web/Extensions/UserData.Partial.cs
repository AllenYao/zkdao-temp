using System.ComponentModel.DataAnnotations;
using zic_dotnet;

namespace zkdao.Web.UserServiceReference {
    public class UserDataMetadata {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact name")]
        public string Contact { get; set; }

    }

    [MetadataType(typeof(UserDataMetadata))]
    public partial class UserData {
        public static string GetHashPassword(string value) {
            return Encrypt.EncryptUserPassword(value);
        }
    }
}

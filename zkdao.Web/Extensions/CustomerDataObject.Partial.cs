using System.ComponentModel.DataAnnotations;

namespace zkdao.Web.CustomerServiceReference
{
    public class CustomerDataObjectMetadata
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact name")]
        public string Contact { get; set; }

    }

    [MetadataType(typeof(CustomerDataObjectMetadata))]
    public partial class CustomerDataObject
    {
    }
}

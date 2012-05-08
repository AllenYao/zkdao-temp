using System.ComponentModel.DataAnnotations;

namespace zkdao.Web.CustomerServiceReference
{
    public class AddressDataObjectMetadata
    {
        [Required]
        [Display(Name = "Address - Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Address - State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Address - City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address - Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Address - Zip code")]
        public string Zip { get; set; }
    }

    [MetadataType(typeof(AddressDataObjectMetadata))]
    partial class AddressDataObject
    {

    }
}

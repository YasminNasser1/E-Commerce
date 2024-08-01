using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User : IdentityUser<long>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public UserRole Role { get; set; } // Enum for roles

        // Vendor-specific attributes
        public string VendorCertificateNumber { get; set; }

        public byte[] VendorCertificateImage { get; set; }
        public VendorStatus VendorStatus { get; set; } = VendorStatus.Pending;

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User : IdentityUser<long>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Order> orders { get; set; }= new HashSet<Order>();
        // One-to-one relationship

    }
}

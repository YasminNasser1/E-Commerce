using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class ProductType:BaseEntity
	{
        public string Name { get; set; }
        public virtual ICollection<Product> products { get; set; } = new HashSet<Product>();

    }
}

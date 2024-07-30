//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer.Models
//{
//	public class Cart
//	{
//        public int Id { get; set; }
//        public decimal TotalPrice { get; set; }
//		public bool IsEmpty {  get; set; }
//        public long  UserId { get; set; }
//        [ForeignKey("UserId")]
//        public User User { get; set; } 
//        //// Navigational property
//        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
//     //   public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();


//    }
//}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsEmpty { get; set; }
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        // Navigational property for CartItems
       // [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}

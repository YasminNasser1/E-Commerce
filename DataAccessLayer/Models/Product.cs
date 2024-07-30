//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;
//namespace DataAccessLayer.Models
//{
//	public class Product
//	{
//        public int Id { get; set; }
//        public string Name { get; set; }
//		public string Description { get; set; }
//		public string PictureUrl { get; set; }
//        public decimal Price { get; set; } = 0;
//		public int? Rate { get; set; } // Update [optinal]
//        public Target Target { get; set; }
//        public string Color { get; set; }
//        public string Size { get; set; }// Update
//        public decimal? Discount { get; set; } // Update[optinal]

//        [ForeignKey(nameof(ProductBrand))]
//        public int ProductBrandId { get; set; }

//        public ProductBrand ProductBrand { get; set; }

//        [ForeignKey(nameof(ProductType))]

//        public int ProductTypeId { get; set; }

//        public decimal Quantity { get; set; } = 0; // Update=
//        public ProductType ProductType { get; set; }

//        // Navigational property
//        [JsonIgnore]
//        public virtual ICollection<Cart> Carts { get; set; }= new HashSet<Cart>();
//        [JsonIgnore]
//        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();


//    }
//}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public int? Rate { get; set; }
        public Target Target { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? Discount { get; set; }

        [ForeignKey(nameof(ProductBrand))]
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public decimal Quantity { get; set; } = 0;
        // Navigational properties
        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}

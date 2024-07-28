using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    internal class CustmomerBasket
    {
        public string Id { get; set; }
        //public List<BasketItem> Items { get; set; }
        public CustmomerBasket(string id)
        {
            Id = id;
        }

    }

}

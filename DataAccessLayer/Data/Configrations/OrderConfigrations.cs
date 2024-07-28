using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Data.Configrations
{
    internal class OrderConfigrations
    {
        public void Configure(EntityTypeConfiguration<Order> builder)
        {

            builder.Property(O => O.TotalPrice)
                .HasColumnType("decimal(18,2)");
                
        }
    }
}

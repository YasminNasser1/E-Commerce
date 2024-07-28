using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Data.Configrations
{
    internal class ProductTypeConfigrations
    {
        public void Configure(EntityTypeConfiguration<ProductType> builder)
        {
            builder.Property(x => x.Name).IsRequired();
        }
    }
}

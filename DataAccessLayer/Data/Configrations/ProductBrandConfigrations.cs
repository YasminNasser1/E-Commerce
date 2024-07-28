using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class ProductBrandConfigrations
    {
        public void Configure(EntityTypeConfiguration<ProductBrand> builder)
        {
            builder.Property(N => N.Name).IsRequired();

        }
    }
}


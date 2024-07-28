using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class CartConfigrations : EntityTypeConfiguration<Cart>
    {

        public void Configure()
        {
            Property(T => T.TotalPrice)
                            .HasColumnType("decimal(18,2)");
            Property(I => I.IsEmpty).IsRequired();

        }
    }

}

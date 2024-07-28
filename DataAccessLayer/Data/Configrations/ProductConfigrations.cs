using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class ProductConfigrations 
    { 
        public void Configure(EntityTypeConfiguration<Product> builder)
        {
           

            builder.HasKey(P => P.Id);

            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(P => P.Description)
                .IsRequired();

            builder.Property(P => P.PictureUrl)
                .IsRequired();

            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");


        }
    }
}

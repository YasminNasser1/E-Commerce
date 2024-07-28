using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configrations
{
    internal class UserConfigrations : EntityTypeConfiguration<User>
    {
        public UserConfigrations()
        {
            // Configure properties
            Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            
        }
    }
    
}

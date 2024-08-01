using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Repositories
{
    public class VendorRepository: IVendorRepository
    {
        private readonly E_CommerceDbContext _context;

        public VendorRepository(E_CommerceDbContext context)
        {
            _context = context;
        }

        public async Task RequestVendorApproval(long userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (user.Role != UserRole.Vendor)
            {
                throw new Exception("Only users with the role of Vendor can request approval.");
            }

            if (user.VendorStatus == VendorStatus.Pending)
            {
                throw new Exception("Vendor request is already pending.");
            }

            user.VendorStatus = VendorStatus.Pending;
            await _context.SaveChangesAsync();
        }


    }
}

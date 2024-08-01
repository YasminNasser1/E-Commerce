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


    public class AdminRepository : IAdminService

    {
        private readonly E_CommerceDbContext _context;

        public AdminRepository(E_CommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetTotalProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.ProductBrand)   // If you need to include related entities
                    .Include(p => p.ProductType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                throw new Exception("An error occurred while retrieving the products.", ex);
            }
        }

        // Vendor Management

        public async Task AcceptVendorAsync(long vendorId)
        {
            var vendor = await _context.Users.SingleOrDefaultAsync(u => u.Id == vendorId && u.Role == UserRole.Vendor);

            if (vendor == null)
            {
                throw new Exception("Vendor not found.");
            }

            if (vendor.VendorStatus == VendorStatus.Approved)
            {
                throw new Exception("Vendor is already approved.");
            }

            vendor.VendorStatus = VendorStatus.Approved;
            await _context.SaveChangesAsync();
        }

        public async Task RejectVendorAsync(long vendorId)
        {
            var vendor = await _context.Users.SingleOrDefaultAsync(u => u.Id == vendorId && u.Role == UserRole.Vendor);

            if (vendor == null)
            {
                throw new Exception("Vendor not found.");
            }

            _context.Users.Remove(vendor);
            await _context.SaveChangesAsync();
        }

        // Reporting Methods

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                                 .Include(p => p.ProductBrand)
                                 .Include(p => p.ProductType)
                                 .ToListAsync();
        }

        public async Task<List<Product>> GetProductSalesAsync()
        {
            return await _context.Products
                                 .Include(p => p.Orders)
                                 .ToListAsync();
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Orders.SumAsync(o => o.TotalPrice);
        }

        public async Task<List<string>> GetProductCategoriesAsync()
        {
            return await _context.ProductTypes.Select(pt => pt.Name).ToListAsync();
        }

        public async Task<List<Product>> GetTopSellingProductsAsync(int count)
        {
            return await _context.Products
                                 .Include(p => p.Orders)
                                 .OrderByDescending(p => p.QuantitySold)
                                 .Take(count)
                                 .ToListAsync();
        }
       
    }
}
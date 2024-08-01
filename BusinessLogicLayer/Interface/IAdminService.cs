using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interface
{
    public interface IAdminService
    {
        Task AcceptVendorAsync(long vendorId);
        Task RejectVendorAsync(long vendorId);
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductSalesAsync();
        Task<int> GetTotalOrdersAsync();
        Task<decimal> GetTotalSalesAsync();
        Task<List<string>> GetProductCategoriesAsync();
        Task<List<Product>> GetTopSellingProductsAsync(int count);

        Task<IEnumerable<Product>> GetTotalProductsAsync();
    }
}


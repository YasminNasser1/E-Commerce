using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersByUserIdAsync(long userId);

        Task CancelOrderAsync(long userId, int orderId);
    }
}

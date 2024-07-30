using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using BusinessLogicLayer.Interface;

namespace BusinessLogicLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly E_CommerceDbContext _context;

        public OrderRepository(E_CommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(long userId)
        {
            return await _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .Include(o => o.User)
                                 .Include(o => o.Products)
                                 .ToListAsync();
        }


        public async Task CancelOrderAsync(long userId, int orderId)
        {
            try
            {
                var order = await _context.Orders
                                          .Include(o => o.Products)
                                          .SingleOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);

                if (order == null)
                {
                    throw new Exception("Order not found or does not belong to the specified user.");
                }

                if (order.Status != OrderStatus.Pending)
                {
                    throw new Exception("Only orders with status 'Pending' can be canceled.");
                }

                // Update the order status to Canceled
                order.Status = OrderStatus.Cancelled;

                // Restore the product quantities
                foreach (var product in order.Products)
                {
                    var originalProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);
                    if (originalProduct != null)
                    {
                        originalProduct.Quantity += product.Quantity;
                    }
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                // Handle the exception as needed
                throw new Exception("An error occurred while canceling the order.", ex);
            }
        }

    }
}

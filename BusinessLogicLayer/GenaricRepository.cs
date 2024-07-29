using BusinessLogicLayer.Repo;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer
{

    public class GenaricRepository<T> : IGenaricrepository<T> where T : BaseEntity
    {
        private readonly E_CommerceDbContext _dbContext;

        public GenaricRepository(E_CommerceDbContext dbContext ) 
        { 
            _dbContext= dbContext;
        }
        public async Task CreateAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
          => await _dbContext.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
          => await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddProductToCartAsync(int productId, long userId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            using (var context = new E_CommerceDbContext())
            {
                // Retrieve the cart for the user or create a new one if it doesn't exist
                var cart = await context.Carts
                                        .Include(c => c.Products)
                                        .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart { UserId = userId, IsEmpty = false, TotalPrice = 0 };
                    context.Carts.Add(cart);
                }

                // Retrieve the product
                var product = await context.Products.SingleOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    throw new Exception("Product not found.");
                }

                // Check if the product is already in the cart
                var existingProduct = cart.Products.SingleOrDefault(p => p.Id == productId);

                if (existingProduct != null)
                {
                    // Update the quantity of the existing product
                    existingProduct.Quantity += quantity;
                }
                else
                {
                    // Add the new product to the cart
                    // Ensure you have a mechanism to add Quantity in Product
                    cart.Products.Add(new Product
                    {
                        Id = product.Id,
                        Quantity = quantity,
                        Price = product.Price
                        // Set other properties if needed
                    });
                }

                // Update the cart's total price
                cart.TotalPrice = cart.Products.Sum(p => p.Quantity * p.Price);
                cart.IsEmpty = false;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartAsync(int productId, long userId, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
            }

            using (var context = new E_CommerceDbContext())
            {
                var cart = await context.Carts
                                        .Include(c => c.Products)
                                        .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                var existingProduct = cart.Products.SingleOrDefault(p => p.Id == productId);

                if (existingProduct == null)
                {
                    throw new Exception("Product not found in cart.");
                }

                existingProduct.Quantity = quantity;

                if (existingProduct.Quantity == 0)
                {
                    cart.Products.Remove(existingProduct);
                }

                cart.TotalPrice = cart.Products.Sum(p => p.Quantity * p.Price);
                cart.IsEmpty = !cart.Products.Any();

                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteFromCartAsync(int productId, long userId)
        {
            using (var context = new E_CommerceDbContext())
            {
                var cart = await context.Carts
                                        .Include(c => c.Products)
                                        .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                var existingProduct = cart.Products.SingleOrDefault(p => p.Id == productId);

                if (existingProduct != null)
                {
                    cart.Products.Remove(existingProduct);
                }

                cart.TotalPrice = cart.Products.Sum(p => p.Quantity * p.Price);
                cart.IsEmpty = !cart.Products.Any();

                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task CheckoutAsync(long userId, string address)
        {
            using (var context = new E_CommerceDbContext())
            {
                var cart = await context.Carts
                                        .Include(c => c.Products)
                                        .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || cart.IsEmpty)
                {
                    throw new Exception("Cart is empty or not found.");
                }

                var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                var order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTimeOffset.Now,
                    Status = OrderStatus.Pending,
                    Address = address,
                    TotalPrice = cart.TotalPrice,
                    Products = new HashSet<Product>(cart.Products),
                    User = user
                };

                context.Orders.Add(order);

                // Clear the cart
                cart.Products.Clear();
                cart.TotalPrice = 0;
                cart.IsEmpty = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task CancelOrderAsync(int orderId, long userId)
        {
            using (var context = new E_CommerceDbContext())
            {
                var order = await context.Orders
                                         .SingleOrDefaultAsync(o => o.OrderId == orderId && o.UserId == userId);

                if (order == null)
                {
                    throw new Exception("Order not found.");
                }

                if (order.Status == OrderStatus.Cancelled)
                {
                    throw new Exception("Order is already cancelled.");
                }

                order.Status = OrderStatus.Cancelled;
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}

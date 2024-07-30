using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using BusinessLogicLayer.Interface;
using System.Runtime.Remoting.Contexts;

namespace BusinessLogicLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly E_CommerceDbContext _context;

        public CartRepository(E_CommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems.Select(ci => ci.Product))
                                 .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart> GetCartByUserIdAsync(long userId)
        {
            return await _context.Carts
                                 .Include(c => c.CartItems)                      // Include CartItems
                                 .Include(c => c.CartItems.Select(ci => ci.Product))  // Include Product details within CartItems
                                 .SingleOrDefaultAsync(c => c.UserId == userId);
        }


        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts
                                 .Include(c => c.CartItems.Select(ci => ci.Product))
                                 .ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(int cartId, int productId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));
            }

            using (var context = new E_CommerceDbContext())
            {
                // Find the cart with the specified cartId
                var cart = await context.Carts
                                        .Include(c => c.CartItems)
                                        .SingleOrDefaultAsync(c => c.Id == cartId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                // Find the specific cart item to update
                var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null)
                {
                    throw new Exception("Product not found in cart.");
                }

                // Update the quantity of the cart item
                cartItem.Quantity = newQuantity;

                // Update the cart's total price
                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);

                // Save the changes to the database
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCartItemAsync(int cartId, int productId)
        {
            try
            {
                // Find the cart with the specified cartId
                var cart = await _context.Carts
                                        .Include(c => c.CartItems)
                                        .SingleOrDefaultAsync(c => c.Id == cartId);

                if (cart == null)
                {
                    throw new Exception("Cart not found.");
                }

                // Find the specific cart item to remove
                var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null)
                {
                    throw new Exception("Product not found in cart.");
                }

                // Remove the cart item
                _context.CartItems.Remove(cartItem);

                // Update the cart's total price
                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);

                // Check if the cart is empty
                cart.IsEmpty = !cart.CartItems.Any();

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
                // Rethrow or handle the exception as needed
                throw new Exception("An error occurred while removing the product from the cart.", ex);
            }
        }


        public async Task AddProductToCartAsync(long userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            var cart = await _context.Carts
                                     .Include(c => c.CartItems.Select(ci => ci.Product))
                                     .SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // If the cart does not exist, create a new one for the user
                cart = new Cart
                {
                    UserId = userId,
                    IsEmpty = true,
                    TotalPrice = 0,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                cart.CartItems.Add(cartItem);
            }

            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);
            cart.IsEmpty = cart.CartItems.Count == 0;

            await _context.SaveChangesAsync();
        }

        public async Task CheckoutAsync(long userId, string address)
        {
            try
            {
                // Fetch the user's cart
                var cart = await _context.Carts
                                        .Include(c => c.CartItems.Select(ci => ci.Product)) // Include products
                                        .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || cart.IsEmpty)
                {
                    throw new Exception("Cart is empty or not found.");
                }

                // Fetch the user
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                // Create the new order
                var order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTimeOffset.Now,
                    Status = OrderStatus.Pending,
                    Address = address,
                    TotalPrice = cart.TotalPrice,
                    Products = new List<Product>()
                };

                foreach (var cartItem in cart.CartItems)
                {
                    // Deduct the checked-out quantity from the product's quantity in the database
                    var product = cartItem.Product;
                    if (product.Quantity < cartItem.Quantity)
                    {
                        throw new Exception($"Insufficient stock for product {product.Name}");
                    }

                    product.Quantity -= cartItem.Quantity;

                    // Add the product to the order
                    order.Products.Add(product);
                }

                // Add the order to the database
                _context.Orders.Add(order);

                // Clear the cart
                cart.CartItems.Clear();
                cart.TotalPrice = 0;
                cart.IsEmpty = true;

                // Save changes to the database
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
                throw new Exception("An error occurred during checkout.", ex);
            }
        }


    }



}

using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interface
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> GetCartByUserIdAsync(long userId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task AddCartAsync(Cart cart);
        Task UpdateCartItemAsync(int cartId,int productId, int newQuantity);
        Task DeleteCartItemAsync(int cartId, int productId);
        Task AddProductToCartAsync(long userId, int productId, int quantity);

        Task CheckoutAsync(long userId, string address);
    }
}

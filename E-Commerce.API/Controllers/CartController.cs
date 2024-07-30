using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer;
using BusinessLogicLayer.Repo;
using System.Collections.Generic;
using System;



//namespace E_Commerce.API.Controllers
//{
//    public class CartController : ApiController
//    {
//        [HttpPost]
//        [Route("AddProductToCart/{productId}/{userId}")]
//        public async Task<IActionResult> AddProductToCart(int productId, long userId, [FromBody] int quantity)
//        {
//            try
//            {
//                await _cartService.AddProductToCartAsync(productId, userId, quantity);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpPut]
//        [Route("UpdateCart/{productId}/{userId}")]
//        public async Task<IActionResult> UpdateCart(int productId, long userId, [FromBody] int quantity)
//        {
//            try
//            {
//                await _cartService.UpdateCartAsync(productId, userId, quantity);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpDelete]
//        [Route("DeleteFromCart/{productId}/{userId}")]
//        public async Task<IActionResult> DeleteFromCart(int productId, long userId)
//        {
//            try
//            {
//                await _cartService.DeleteFromCartAsync(productId, userId);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpDelete]
//        [Route("DeleteFromCart/{productId}/{userId}")]
//        public async Task<IActionResult> DeleteFromCart(int productId, long userId)
//        {
//            try
//            {
//                await _cartService.DeleteFromCartAsync(productId, userId);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer.Repo;
using BusinessLogicLayer;
using System.Collections.Generic;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Repositories;


namespace E_Commerce.API.Controllers
{
    public class CartController : ApiController
    {
        private readonly ICartRepository _cartRepo;

        public CartController(CartRepository cartRepo)
        {
            _cartRepo = cartRepo ?? throw new ArgumentNullException(nameof(cartRepo));
        }



        #region AddProductToCart

        //[HttpPost]
        //[Route("AddProductToCart/{productId}/{userId}")]
        //public async Task<IHttpActionResult> AddProductToCart(int productId, long userId, [FromBody] int? Quantity)
        //{
        //    try
        //    {
        //        Console.WriteLine($"Quantity received: {Quantity}");
        //        if (Quantity == null || Quantity <= 0)
        //        {
        //            return BadRequest("Quantity must be greater than zero.");
        //        }

        //        await _cartRepo.AddProductToCartAsync(productId, userId, Quantity.Value);
        //        return Ok("Product added to cart successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpGet]
        [Route("GetCart/{userId}")]
        public async Task<IHttpActionResult> GetCart(long userId)
        {
            try
            {
                var cart = await _cartRepo.GetCartByUserIdAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return InternalServerError(ex); // Return a 500 status code for server errors
            }
        }

        #endregion



        #region AddProductToCart

        [HttpPost]
        [Route("AddProductToCart/{userId}/{productId}")]
        public async Task<IHttpActionResult> AddProductToCartAsync(long userId, int productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero.");
                }

                await _cartRepo.AddProductToCartAsync(userId, productId, quantity);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception details if possible
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest("An error occurred while adding the product to the cart.");
            }
        }

        #endregion

        #region UpdateCart

        [HttpPut]
        [Route("UpdateCart/{cartId}/{productId}")]
        public async Task<IHttpActionResult> UpdateCart(int cartId, int productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero.");
                }

                await _cartRepo.UpdateCartItemAsync(cartId, productId, quantity);
                return Ok("Cart updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest("An error occurred while updating the cart.");
            }
        }

        #endregion

        #region DeleteProductFromCart

        [HttpDelete]
        [Route("DeleteFromCart/{cartId}/{productId}")]
        public async Task<IHttpActionResult> DeleteFromCart(int cartId, int productId)
        {
            try
            {
                await _cartRepo.DeleteCartItemAsync(cartId, productId);
                return Ok("Product removed from cart successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest("An error occurred while removing the product from the cart.");
            }
        }

        [HttpPost]
        [Route("Checkout/{userId}")]
        public async Task<IHttpActionResult> Checkout(long userId,  string address)
        {
            try
            {
                await _cartRepo.CheckoutAsync(userId, address);
                return Ok("Order placed successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return BadRequest("An error occurred during checkout.");
            }
        }


        #endregion
        #region ClearCart

        //[HttpDelete]
        //[Route("ClearCart/{userId}")]
        //public async Task<IHttpActionResult> ClearCart(long userId)
        //{
        //    try
        //    {
        //        await _cartRepo.ClearCartAsync(userId);
        //        return Ok("Cart cleared successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        #endregion
    }
}

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


namespace E_Commerce.API.Controllers
{
    public class CartController : ApiController
    {
        private readonly IGenaricrepository<Cart> _cartRepo;

        public CartController(GenaricRepository<Cart> cartRepo)
        {
            _cartRepo = cartRepo;
        }



        #region AddProductToCart

        [HttpPost]
        [Route("AddProductToCart/{productId}/{userId}")]
        public async Task<IHttpActionResult> AddProductToCart(int productId, long userId, [FromBody] int quantity)
        {
            try
            {
                
                await _cartRepo.AddProductToCartAsync(productId, userId, quantity);
                return Ok("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region UpdateCart

        [HttpPut]
        [Route("UpdateCart/{productId}/{userId}")]
        public async Task<IHttpActionResult> UpdateCart(int productId, long userId, [FromBody] int quantity)
        {
            try
            {
                await _cartRepo.UpdateCartAsync(productId, userId, quantity);
                return Ok("Cart updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region DeleteProductFromCart

        [HttpDelete]
        [Route("DeleteFromCart/{productId}/{userId}")]
        public async Task<IHttpActionResult> DeleteFromCart(int productId, long userId)
        {
            try
            {
                await _cartRepo.DeleteFromCartAsync(productId, userId);
                return Ok("Product removed from cart successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

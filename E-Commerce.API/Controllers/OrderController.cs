using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;

namespace YourNamespace.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IHttpActionResult> GetOrdersByUser(long userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
                if (orders == null || orders.Count == 0)
                {
                    return NotFound(); // Return 404 if no orders found
                }
                return Ok(orders); // Return 200 with the list of orders
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

        [HttpPut]
        [Route("cancel/{userId}/{OrderId}")]
        public async Task<IHttpActionResult> CancelOrder(long userId, int OrderId)
        {
            try
            {
                await _orderRepository.CancelOrderAsync(userId, OrderId);
                return Ok("Order canceled successfully.");
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
    }

 
}

using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer;
using System.Collections.Generic;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Repositories;
using System;
using BusinessLogicLayer.Repo;
using System.Web.Http;

namespace E_Commerce.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminService _adminRepo;

        public AdminController(AdminRepository adminService)
        {
            _adminRepo = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }
        [HttpGet]
        [Route("orders")]
        public async Task<IHttpActionResult> GetTotalOrders()
        {
            try
            {
                var orders = await _adminRepo.GetTotalOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("products")]
        public async Task<IHttpActionResult> GetTotalProducts()
        {
            try
            {
                var products = await _adminRepo.GetTotalProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("sales")]
        public async Task<IHttpActionResult> GetTotalSales()
        {
            try
            {
                var sales = await _adminRepo.GetTotalSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IHttpActionResult> GetProductCategories()
        {
            try
            {
                var categories = await _adminRepo.GetProductCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("top-selling-products")]
        public async Task<IHttpActionResult> GetTopSellingProducts([FromUri] int topN)
        {
            try
            {
                var topSellingProducts = await _adminRepo.GetTopSellingProductsAsync(topN);
                return Ok(topSellingProducts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("approve-vendor/{vendorId}")]
        public async Task<IHttpActionResult> ApproveVendor(long vendorId)
        {
            try
            {
                await _adminRepo.AcceptVendorAsync(vendorId);
                return Ok("Vendor approved successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("reject-vendor/{vendorId}")]
        public async Task<IHttpActionResult> RejectVendor(long vendorId)
        {
            try
            {
                await _adminRepo.RejectVendorAsync(vendorId);
                return Ok("Vendor rejected and removed from the database.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

}

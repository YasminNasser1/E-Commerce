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
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        private readonly IVendorRepository _vendorRepo;

        public VendorController(VendorRepository vendorRepository)
        {
            _vendorRepo = vendorRepository ?? throw new ArgumentNullException(nameof(vendorRepository));
        }

        [HttpPost]
        [Route("request-approval/{userId}")]
        public async Task<IHttpActionResult> RequestVendorApproval(long userId)
        {
            try
            {
                await _vendorRepo.RequestVendorApproval(userId);
                return Ok("Vendor request sent successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
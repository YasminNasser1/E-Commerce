using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogicLayer.Repo;
using BusinessLogicLayer;
using System.Collections.Generic;
namespace E_Commerce.API.Controllers

{
    public class ProductController : ApiController
    {
        private readonly IGenaricrepository<Product> _productRepo;       
        public ProductController(GenaricRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        //ParameterLess Constractour

        #region Get

        [HttpGet]

        [Route("products")]
        public async Task<IHttpActionResult> GetProducts()
        {
            var Products = await _productRepo.GetAllAsync();
            return Ok(Products);
        }

        #endregion

        #region GetProductByID
        [HttpGet]
        [Route("GetProductByID/{id}")]

        public Task<Product> Getbyid(int id)
        => _productRepo.GetByIdAsync(id);

        #endregion

        #region Add

        // POST: api/Products
        [HttpPost]
        [Route("AddProducts")]

        public void AddProduct(Product product)
        =>_productRepo.CreateAsync(product);


        #endregion

        #region UPdate
        // PUT: api/Products/5
        [HttpPut]
        [Route("UpdateProducts/{id}")]

        public async void UpdateProduct(int id, Product product)
        {
            if (id == product.Id)
            {
                var Product = await _productRepo.GetByIdAsync(id);
                if (Product != null)
                {
                    Product.Description = product.Description;
                    Product.Name = product.Name;
                    Product.Price = product.Price;
                    Product.Rate = product.Rate;
                    Product.Color= product.Color;
                    Product.PictureUrl= product.PictureUrl;
                    Product.ProductBrand = product.ProductBrand;
                    Product.Carts = product.Carts;
                    Product.Discount= product.Discount;
                    Product.ProductType= product.ProductType;
                    Product.Size = product.Size;
                    
                   await _productRepo.UpdateAsync(Product);
                }
            }
        }
      
        #endregion

        #region DELETE

        [HttpDelete]

        [Route("DeleteProduct/{id}")]
        public async void DeleteByID(int id)
        =>  await _productRepo.DeleteAsync(id);

        #endregion


    }
}

using BusinessLogicLayer;
using BusinessLogicLayer.Repo;
using DataAccessLayer.Models;
using E_Commerce.API.Controllers;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace E_Commerce.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();
			//container.RegisterType<IGenaricrepository<Product>, GenaricRepository<Product>>();
			//container.RegisterType<IGenaricrepository<ProductType>, GenaricRepository<ProductType>>();
			//container.RegisterType<IGenaricrepository<ProductBrand>, GenaricRepository<ProductBrand>>();
			//container.RegisterType<IProductRepository, ProductRepository>();

			


			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
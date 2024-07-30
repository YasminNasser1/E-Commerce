using DataAccessLayer.Data.Configrations;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Reflection;

namespace DataAccessLayer.Context
{
	public class E_CommerceDbContext: DbContext
	{
		public E_CommerceDbContext() : base("name=DefaultConnection")
		{

		}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }  // Ensure this is defined


    }
}

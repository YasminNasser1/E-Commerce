﻿//using DataAccessLayer.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer.Repo
//{
//    public interface IGenaricrepository <T> where T : class
//    {

//        Task<IEnumerable<T>> GetAllAsync();


//        Task<T> GetByIdAsync(int id);

//        Task UpdateAsync(T entity);

//        Task DeleteAsync(int id);

//        Task CreateAsync(T entity);
//        Task AddProductToCartAsync(int productId, long userId, int quantity);

//        Task UpdateCartAsync(int productId, long userId, int quantity);

//        Task DeleteFromCartAsync(int productId, long userId);

//        Task CheckoutAsync(long userId, string address);

//      //  Task<IEnumerable<Order>> GetOrdersAsync(long userId);

//        Task<Cart> GetCartAsync(long userId);
//    }
//}
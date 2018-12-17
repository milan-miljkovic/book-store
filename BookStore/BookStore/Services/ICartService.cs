using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICartService
    {
        /// <summary>
        /// Adds new book to cart
        /// </summary>
        /// <param name="book"></param>
        /// <param name="quantity"></param>
        /// <param name="cartId"></param>
        Task AddToCartAsync(Book book, int quantity, string cartId);

        /// <summary>
        /// Removes book from cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="id"></param>
        Task<int> RemoveFromCartAsync(string cartId, int id);

        /// <summary>
        /// Removes book from cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns>Updated quantity</returns>
        Task<int> UpdateCartQuantityAsync(string cartId, int id, int quantity);

        /// <summary>
        /// Empties cart
        /// </summary>
        /// <param name="cartId"></param>
        Task EmptyCartAsync(string cartId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>List of cart items</returns>
        Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId);

        /// <summary>
        /// Counts cart items
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>Number of cart items</returns>
        Task<int> GetCountAsync(string cartId);

        /// <summary>
        /// Calculates total cart amount
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>Total of cart</returns>
        Task<double> GetTotalAsync(string cartId);

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="cartId"></param>
        /// <returns>Order ID</returns>
        Task<int> CreateOrderAsync(Order order, string cartId);

        /// <summary>
        /// Migrates cart to specified user
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="userName"></param>
        Task MigrateCartAsync(string cartId, string userName);
    }
}

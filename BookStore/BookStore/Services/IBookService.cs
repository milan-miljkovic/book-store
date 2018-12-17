using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookService
    {
        /// <summary>
        /// Adds new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task<Book> AddBookAsync(Book book);

        /// <summary>
        /// Updates existing book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task<Book> UpdateBookAsync(Book book);

        /// <summary>
        /// Deletes existing book
        /// </summary>
        /// <param name="book"></param>
        Task DeleteBookAsync(Book book);

        /// <summary>
        /// Gets books with pagination
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksWithPaginationAsync(int categoryId = 0, int pageSize = 10, int page = 1);

        /// <summary>
        /// Gets all books
        /// <param name="categoryId"></param>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksAsync(int categoryId = 0);

        /// <summary>
        /// Gets total number of books
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<int> GetTotalNumberOfBooksAsync(int categoryId = 0);

        /// <summary>
        /// Gets book by id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<Book> GetBookAsync(int bookId);
    }
}

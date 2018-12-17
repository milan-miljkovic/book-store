using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Adds new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<Category> AddCategoryAsync(Category category);

        /// <summary>
        /// Updates existing category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<Category> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Deletes existing category
        /// </summary>
        /// <param name="category"></param>
        Task DeleteCategoryAsync(Category category);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategoriesAsync();

        /// <summary>
        /// Gets category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Category> GetCategoryAsync(int id);
    }
}

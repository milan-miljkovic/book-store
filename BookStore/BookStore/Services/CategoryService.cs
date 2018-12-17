using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess;
using BookStore.Models;

namespace BookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var newCategory = await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return newCategory;
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _repository.Delete(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _repository.FindAllAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _repository.FindOneAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var updatedCategory = _repository.Update(category);
            await _unitOfWork.CommitAsync();
            return updatedCategory;
        }
    }
}

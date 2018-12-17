using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess;
using BookStore.Models;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IRepository<Book> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var addedBook = await _repository.AddAsync(book);
            await _unitOfWork.CommitAsync();

            return addedBook;
        }

        public async Task DeleteBookAsync(Book book)
        {
            _repository.Delete(book);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int categoryId = 0)
        {
            return await _repository.FindManyAsync(p => categoryId == 0 || p.CategoryId == categoryId, c => c.Category);
        }

        public async Task<IEnumerable<Book>> GetBooksWithPaginationAsync(int categoryId = 0, int pageSize = 10, int page = 1)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;
            return await _repository.FindManyWithPaginationAsync(p => categoryId == 0 || p.CategoryId == categoryId, pageSize, page);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var updatedBook = _repository.Update(book);
            await _unitOfWork.CommitAsync();
            return updatedBook;
        }

        public async Task<int> GetTotalNumberOfBooksAsync(int categoryId = 0)
        {
            return await _repository.CountAsync(p => categoryId == 0 || p.CategoryId == categoryId);
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _repository.FindOneAsync(bookId);
        }
    }
}

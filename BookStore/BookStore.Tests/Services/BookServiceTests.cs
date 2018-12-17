using BookStore.DataAccess;
using BookStore.Models;
using BookStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IRepository<Book>> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public BookServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Book>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        #region ctor tests

        [Fact]
        public void Initialize_Throws_ArgumentNullException_When_repository_Is_Null()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new BookService(null, _unitOfWorkMock.Object));
        }

        [Fact]
        public void Initialize_Throws_ArgumentNullException_When_unitOfWork_Is_Null()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new BookService(_repositoryMock.Object, null));
        }

        #endregion ctor tests

        #region AddBookAsync tests

        [Fact]
        public async Task AddBookAsync_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };

            _repositoryMock.Setup(p => p.AddAsync(book))
                .Returns(Task.FromResult(book));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var addedBook = await bookService.AddBookAsync(book);

            //assert
            Assert.NotNull(book);
            Assert.Equal(book.Author, addedBook.Author);
            Assert.Equal(book.CategoryId, addedBook.CategoryId);
            Assert.Equal(book.Description, addedBook.Description);
            Assert.Equal(book.ImageUrl, addedBook.ImageUrl);
            Assert.Equal(book.Price, addedBook.Price);
            Assert.Equal(book.Title, addedBook.Title);
            _repositoryMock.Verify(p => p.AddAsync(book), Times.Once);
            _unitOfWorkMock.Verify(p => p.CommitAsync(), Times.Once);
        }

        #endregion AddBookAsync tests

        #region DeleteBook tests

        [Fact]
        public async Task DeleteBook_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            await bookService.DeleteBookAsync(book);

            //assert
            _repositoryMock.Verify(p => p.Delete(book), Times.Once);
            _unitOfWorkMock.Verify(p => p.CommitAsync(), Times.Once);
        }

        #endregion DeleteBook tests

        #region GetBooksAsync tests

        [Fact]
        public async Task GetBooksAsync_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };
            _repositoryMock.Setup(p => p.FindManyAsync(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Expression<Func<Book, object>>[]>()))
                .Returns(Task.FromResult(new List<Book> { book }.AsEnumerable()));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetBooksAsync();

            //assert
            Assert.Single(result);
            _repositoryMock.Verify(p => p.FindManyAsync(It.IsAny<Expression<Func<Book, bool>>>(), 
                It.IsAny<Expression<Func<Book, object>>[]>()), Times.Once);
        }

        #endregion GetBooksAsync tests

        #region GetBooksWithPaginationAsync tests

        [Fact]
        public async Task GetBooksWithPaginationAsync_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };
            int page = 3;
            int pageSize = 6;

            _repositoryMock.Setup(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(), 
                pageSize, page, It.IsAny<Expression<Func<Book, object>>[]>()))
                .Returns(Task.FromResult(new List<Book> { book }.AsEnumerable()));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetBooksWithPaginationAsync(0, pageSize, page);

            //assert
            Assert.Single(result);
            _repositoryMock.Verify(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(),
                pageSize, page, It.IsAny<Expression<Func<Book, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetBooksWithPaginationAsync_With_Invalid_Page()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };
            int page = 0;
            int pageSize = 6;

            _repositoryMock.Setup(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(),
                pageSize, 1, It.IsAny<Expression<Func<Book, object>>[]>()))
                .Returns(Task.FromResult(new List<Book> { book }.AsEnumerable()));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetBooksWithPaginationAsync(0, pageSize, page);

            //assert
            Assert.Single(result);
            _repositoryMock.Verify(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(),
                pageSize, 1, It.IsAny<Expression<Func<Book, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetBooksWithPaginationAsync_With_Invalid_PageSize()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };
            int page = 1;
            int pageSize = 0;

            _repositoryMock.Setup(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(),
                10, page, It.IsAny<Expression<Func<Book, object>>[]>()))
                .Returns(Task.FromResult(new List<Book> { book }.AsEnumerable()));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetBooksWithPaginationAsync(0, pageSize, page);

            //assert
            Assert.Single(result);
            _repositoryMock.Verify(p => p.FindManyWithPaginationAsync(It.IsAny<Expression<Func<Book, bool>>>(),
                10, page, It.IsAny<Expression<Func<Book, object>>[]>()), Times.Once);
        }

        #endregion GetBooksWithPaginationAsync tests

        #region UpdateBookAsync tests

        [Fact]
        public async Task UpdateBookAsync_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };

            _repositoryMock.Setup(p => p.Update(book))
                .Returns(book);

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var updatedBook = await bookService.UpdateBookAsync(book);

            //assert
            Assert.NotNull(book);
            Assert.Equal(book.Author, updatedBook.Author);
            Assert.Equal(book.CategoryId, updatedBook.CategoryId);
            Assert.Equal(book.Description, updatedBook.Description);
            Assert.Equal(book.ImageUrl, updatedBook.ImageUrl);
            Assert.Equal(book.Price, updatedBook.Price);
            Assert.Equal(book.Title, updatedBook.Title);
            _repositoryMock.Verify(p => p.Update(book), Times.Once);
            _unitOfWorkMock.Verify(p => p.CommitAsync(), Times.Once);
        }

        #endregion UpdateBookAsync tests

        #region GetTotalNumberOfBooksAsync tests

        [Fact]
        public async Task GetTotalNumberOfBooksAsync_Success()
        {
            //arrange
            _repositoryMock.Setup(p => p.CountAsync(It.IsAny<Expression<Func<Book, bool>>>()))
                .Returns(Task.FromResult(1));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetTotalNumberOfBooksAsync();

            //assert
            _repositoryMock.Verify(p => p.CountAsync(It.IsAny<Expression<Func<Book, bool>>>()), Times.Once);
            Assert.Equal(1, result);
        }

        #endregion GetTotalNumberOfBooksAsync tests

        #region GetBookAsync tests

        [Fact]
        public async Task GetBookAsync_Success()
        {
            //arrange
            var book = new Book
            {
                Author = "test author",
                CategoryId = 1,
                Description = "test description",
                ImageUrl = "imageurl",
                Price = 10.0,
                Title = "test title"
            };
            _repositoryMock.Setup(p => p.FindOneAsync(1))
                .Returns(Task.FromResult(book));

            var bookService = new BookService(_repositoryMock.Object, _unitOfWorkMock.Object);

            //act
            var result = await bookService.GetBookAsync(1);

            //assert
            _repositoryMock.Verify(p => p.FindOneAsync(1), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(book.Author, result.Author);
            Assert.Equal(book.CategoryId, result.CategoryId);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(book.ImageUrl, result.ImageUrl);
            Assert.Equal(book.Price, result.Price);
            Assert.Equal(book.Title, result.Title);
        }

        #endregion GetBookAsync tests
    }
}

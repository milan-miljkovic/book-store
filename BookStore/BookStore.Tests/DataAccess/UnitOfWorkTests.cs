using BookStore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.DataAccess
{
    public class UnitOfWorkTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            _contextMock = new Mock<ApplicationDbContext>(MockBehavior.Default, new object[] { options });
        }

        #region ctor tests

        [Fact]
        public void Initialize_Throws_ArgumentNullException_When_context_Is_Null()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));
        }

        #endregion ctor tests

        #region CommitAsync tests

        [Fact]
        public async Task CommitAsync_Saves_Context_Changes()
        {
            //arrange
            var unitOfWork = new UnitOfWork(_contextMock.Object);

            //act
            await unitOfWork.CommitAsync();

            //assert
            _contextMock.Verify(p => p.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        #endregion CommitAsync tests
    }
}

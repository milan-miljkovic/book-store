using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DataAccess
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class IndexViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalSize { get; set; }
        public int TotalPages { get; set; }
    }
}

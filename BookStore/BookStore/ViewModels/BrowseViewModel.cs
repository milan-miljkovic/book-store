using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BrowseViewModel
    {
        public CategoryViewModel Category { get; set; }
        public List<BookViewModel> Books { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalSize { get; set; }
        public int TotalPages { get; set; }
    }
}

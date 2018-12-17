using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class SelectCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
    }
}

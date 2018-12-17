using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public string CartTotal { get; set; }
        public int CartCount { get; set; }
        public int DeleteId { get; set; }
    }
}

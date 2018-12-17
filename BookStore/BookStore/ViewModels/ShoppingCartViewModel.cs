using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartViewModel> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}

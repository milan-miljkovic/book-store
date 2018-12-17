using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CartViewModel
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int BookId { get; set; }
        public BookViewModel Book { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Title")]
        [Required]
        public string Title { get; set; }

        [DisplayName("Author")]
        [Required]
        [StringLength(64)]
        public string Author { get; set; }

        [DisplayName("Description")]
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        
        [DisplayName("Price")]
        [Required]
        public double Price { get; set; }

        [DisplayName("Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}

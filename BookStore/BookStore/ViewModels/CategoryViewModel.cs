using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [BindRequired]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}

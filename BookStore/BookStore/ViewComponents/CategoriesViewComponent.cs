using AutoMapper;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesViewComponent(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId = 0)
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var selectCategories = new List<SelectCategoryViewModel>();
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    var selectCategory = _mapper.Map<SelectCategoryViewModel>(category);
                    if (category.CategoryId == categoryId)
                    {
                        selectCategory.Active = "active";
                    }

                    selectCategories.Add(selectCategory);
                }
            }

            return View(selectCategories);
        }
    }
}

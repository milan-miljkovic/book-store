using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private const int _pageSize = 6; 

        public BookStoreController(ICategoryService categoryService, IBookService bookService, IMapper mapper)
        {
            _categoryService = categoryService;
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] int page)
        {
            page = page < 1 ? 1 : page;
            ViewBag.AllCategoriesAcive = "active";
            var books = await _bookService.GetBooksWithPaginationAsync(0, _pageSize, page);
            var indexViewModel = new IndexViewModel();
            indexViewModel.Books = books.Select(b => _mapper.Map<BookViewModel>(b)).ToList();
            indexViewModel.Page = page;
            indexViewModel.TotalSize = await _bookService.GetTotalNumberOfBooksAsync();
            indexViewModel.TotalPages = (int)Math.Ceiling(Decimal.Divide(indexViewModel.TotalSize, _pageSize));
            return View(indexViewModel);
        }

        public async Task<IActionResult> Browse(int id, [FromQuery] int page)
        {
            page = page < 1 ? 1 : page;
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return View("AssetNotFound");
            }
            var browseViewModel = new BrowseViewModel();
            browseViewModel.Category = _mapper.Map<CategoryViewModel>(category);
            var books = await _bookService.GetBooksWithPaginationAsync(category.CategoryId);
            browseViewModel.Books = books.Select(b => _mapper.Map<BookViewModel>(b)).ToList();
            browseViewModel.Page = page;
            browseViewModel.TotalSize = await _bookService.GetTotalNumberOfBooksAsync(category.CategoryId);
            browseViewModel.TotalPages = (int)Math.Ceiling(Decimal.Divide(browseViewModel.TotalSize, _pageSize));
            return View(browseViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return View("AssetNotFound");
            }
            return View(_mapper.Map<BookViewModel>(book));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
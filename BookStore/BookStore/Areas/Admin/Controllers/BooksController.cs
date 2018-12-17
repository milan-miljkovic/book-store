using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;
using BookStore.Models;
using BookStore.Services;
using AutoMapper;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, ICategoryService categoryService, IMapper mapper)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooksAsync();
            return View(books.Select(b => _mapper.Map<BookViewModel>(b)));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<BookViewModel>(book));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories.Select(c => _mapper.Map<CategoryViewModel>(c)), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(_mapper.Map<Book>(book));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories.Select(c => _mapper.Map<CategoryViewModel>(c)), "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories.Select(c => _mapper.Map<CategoryViewModel>(c)), "CategoryId", "Name", book.CategoryId);
            return View(_mapper.Map<BookViewModel>(book));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(_mapper.Map<Book>(book));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetCategoriesAsync();
            ViewData["CategoryId"] = new SelectList(categories.Select(c => _mapper.Map<CategoryViewModel>(c)), "CategoryId", "Name", book.CategoryId);
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<BookViewModel>(book));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            await _bookService.DeleteBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string _cartSessionKey = "CartId";
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public ShoppingCartController(ICartService cartService, IBookService bookService, IMapper mapper)
        {
            _cartService = cartService;
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = GetCartId();
            var cartItems = await _cartService.GetCartItemsAsync(cartId);
            List<CartViewModel> cartViewItems = null;

            if (cartItems != null)
            {
                cartViewItems = new List<CartViewModel>();
                foreach (var item in cartItems)
                {
                    var cartViewItem = _mapper.Map<CartViewModel>(item);
                    cartViewItem.Book = _mapper.Map<BookViewModel>(item.Book);
                    cartViewItems.Add(cartViewItem);
                }
            }
            var viewModel = new ShoppingCartViewModel
            {
                
                CartItems = cartViewItems,
                CartTotal = await _cartService.GetTotalAsync(cartId)
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, [FromQuery] int quantity)
        {
            try
            {
                var book = await _bookService.GetBookAsync(id);

                var cartId = GetCartId();
                await _cartService.AddToCartAsync(book, quantity, cartId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCartQuantity(int id, [FromQuery] int quantity)
        {
            try
            {
                var book = await _bookService.GetBookAsync(id);

                var cartId = GetCartId();
                var updatedQuantity = await _cartService.UpdateCartQuantityAsync(cartId, id, quantity);

                var updateQuantityViewModel = new ShoppingCartUpdateQuantityViewModel
                {
                    Quantity = updatedQuantity,
                    Total = (await _cartService.GetTotalAsync(cartId)).ToString("N2")
                };

                return Ok(updateQuantityViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                var cartId = GetCartId();

                var removedId = await _cartService.RemoveFromCartAsync(cartId, id);
                if (removedId == 0)
                {
                    return NotFound();
                }

                var result = new ShoppingCartRemoveViewModel
                {
                    Message = "Book has been removed from your shopping cart.",
                    CartTotal = (await _cartService.GetTotalAsync(cartId)).ToString("N2"),
                    CartCount = await _cartService.GetCountAsync(cartId),
                    DeleteId = id
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private string GetCartId()
        {
            if (!HttpContext.Session.Keys.Contains(_cartSessionKey))
            {
                if (!string.IsNullOrWhiteSpace(this.HttpContext.User.Identity.Name))
                {
                    HttpContext.Session.SetString(_cartSessionKey,
                        HttpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Session.SetString(_cartSessionKey, tempCartId.ToString());
                }
            }
            return HttpContext.Session.GetString(_cartSessionKey);
        }
    }
}
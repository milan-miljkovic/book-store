using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private const string _cartSessionKey = "CartId";

        public CheckoutController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        public ActionResult AddressAndPayment()
        {
            MigrateShoppingCart();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(OrderViewModel orderViewModel)
        {
            try
            {
                var order = _mapper.Map<Order>(orderViewModel);
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                var orderId = await _cartService.CreateOrderAsync(order, HttpContext.User.Identity.Name);

                return RedirectToAction("Complete",
                    new { id = orderId });
            }
            catch
            {
                return View(orderViewModel);
            }
        }

        public ActionResult Complete(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        private void MigrateShoppingCart()
        {
            var cartId = HttpContext.Session.GetString(_cartSessionKey);
            _cartService.MigrateCartAsync(cartId, HttpContext.User.Identity.Name);
        }
    }
}
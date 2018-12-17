using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess;
using BookStore.Models;

namespace BookStore.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IRepository<Cart> cartRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderDetail> orderDetailRepository,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderDetailRepository = orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AddToCartAsync(Book book, int quantity, string cartId)
        {
            var cartItem = await _cartRepository.FindOneAsync(p => p.BookId == book.BookId && p.CartId == cartId);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    BookId = book.BookId,
                    CartId = cartId,
                    Count = quantity,
                    DateCreated = DateTime.Now
                };
               await _cartRepository.AddAsync(cartItem);
            }
            else
            {
                cartItem.Count += quantity;
                _cartRepository.Update(cartItem);
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> CreateOrderAsync(Order order, string cartId)
        {
            double orderTotal = 0;
            var cartItems = await GetCartItemsAsync(cartId);
            if (cartItems != null)
            {
                await _orderRepository.AddAsync(order);
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Order = order,
                        BookId = item.BookId,
                        OrderId = order.OrderId,
                        UnitPrice = item.Book.Price,
                        Quantity = item.Count
                    };
                    orderTotal += (item.Count * item.Book.Price);

                    await _orderDetailRepository.AddAsync(orderDetail);
                }

                order.Total = orderTotal;

                

                await _unitOfWork.CommitAsync();
                await EmptyCartAsync(cartId);
            }

            return order.OrderId;
        }

        public async Task EmptyCartAsync(string cartId)
        {
            var cartItems = await GetCartItemsAsync(cartId);
            if (cartItems != null)
            {
                foreach (var cart in cartItems)
                {
                    _cartRepository.Delete(cart);
                }
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<Cart>> GetCartItemsAsync(string cartId)
        {
            return await _cartRepository.FindManyAsync(p => p.CartId == cartId, c => c.Book);
        }

        public async Task<int> GetCountAsync(string cartId)
        {
            return (await _cartRepository.FindManyAsync(p => p.CartId == cartId)).Select(c => c.Count).Sum();
        }

        public async Task<double> GetTotalAsync(string cartId)
        {
            return (await _cartRepository.FindManyAsync(p => p.CartId == cartId, c => c.Book)).Select(c => c.Count * c.Book.Price).Sum();
        }

        public async Task MigrateCartAsync(string cartId, string userName)
        {
            var cartItems = await _cartRepository.FindManyAsync(p => p.CartId == cartId);
            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    item.CartId = userName;
                    _cartRepository.Update(item);
                }

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<int> RemoveFromCartAsync(string cartId, int id)
        {
            var cartItem = await _cartRepository.FindOneAsync(p => p.CartId == cartId && p.RecordId == id);
            int recordId = 0;
            if (cartItem != null)
            {
                recordId = cartItem.RecordId;
                _cartRepository.Delete(cartItem);
                await _unitOfWork.CommitAsync();
            }

            return recordId;
        }

        public async Task<int> UpdateCartQuantityAsync(string cartId, int id, int quantity)
        {
            var cartItem = await _cartRepository.FindOneAsync(p => p.CartId == cartId && p.RecordId == id);
            int updatedQuantity = 0;
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    updatedQuantity = quantity;
                    cartItem.Count = quantity;
                    _cartRepository.Update(cartItem);
                    await _unitOfWork.CommitAsync();
                }
            }

            return updatedQuantity;
        }
    }
}

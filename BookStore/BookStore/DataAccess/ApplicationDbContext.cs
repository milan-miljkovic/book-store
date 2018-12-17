using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Test category 1",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Test category 2",
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Test category 3",
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Test category 4",
                }
            );

            builder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Author = "Test author 1",
                    CategoryId = 1,
                    Description = "test description 1",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 10.1,
                    Title = "Test title 1"
                },
                new Book
                {
                    BookId = 2,
                    Author = "Test author 2",
                    CategoryId = 1,
                    Description = "test description 2",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 5.5,
                    Title = "Test title 2"
                },
                new Book
                {
                    BookId = 3,
                    Author = "Test author 3",
                    CategoryId = 1,
                    Description = "test description 3",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 12.0,
                    Title = "Test title 3"
                },
                new Book
                {
                    BookId = 4,
                    Author = "Test author 4",
                    CategoryId = 2,
                    Description = "test description 4",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 10.99,
                    Title = "Test title 4"
                },
                new Book
                {
                    BookId = 5,
                    Author = "Test author 5",
                    CategoryId = 2,
                    Description = "test description 5",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 11.00,
                    Title = "Test title 5"
                },
                new Book
                {
                    BookId = 6,
                    Author = "Test author 6",
                    CategoryId = 3,
                    Description = "test description 6",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 10.00,
                    Title = "Test title 6"
                },
                new Book
                {
                    BookId = 7,
                    Author = "Test author 7",
                    CategoryId = 3,
                    Description = "test description 7",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 7.7,
                    Title = "Test title 7"
                },
                new Book
                {
                    BookId = 8,
                    Author = "Test author 8",
                    CategoryId = 3,
                    Description = "test description 8",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 7.00,
                    Title = "Test title 8"
                },
                new Book
                {
                    BookId = 9,
                    Author = "Test author 9",
                    CategoryId = 3,
                    Description = "test description 9",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 10.00,
                    Title = "Test title 9"
                },
                new Book
                {
                    BookId = 10,
                    Author = "Test author 10",
                    CategoryId = 3,
                    Description = "test description 10",
                    ImageUrl = "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png",
                    Price = 9.8,
                    Title = "Test title 10"
                }
            );


            base.OnModelCreating(builder);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Services;
using BookStore.Models;
using AutoMapper;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ResolveDependencies(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAutoMapper((c) =>
            {
                CreateMaps(c);
            });
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedAdminUser(userManager, roleManager);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=BookStore}/{action=Index}/{id?}");
            });
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
               .AllowAnyHeader()
);
        }

        private void ResolveDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Book>, Repository<Book>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<OrderDetail>, Repository<OrderDetail>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICartService, CartService>();
        }

        private void CreateMaps(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Category, SelectCategoryViewModel>();
            cfg.CreateMap<CategoryViewModel, Category>();
            cfg.CreateMap<Category, CategoryViewModel>();
            cfg.CreateMap<Book, BookViewModel>()
                .ForMember(d => d.CategoryName, src => src.MapFrom(s => s.Category.Name));
            cfg.CreateMap<Cart, CartViewModel>();
            cfg.CreateMap<BookViewModel, Book>();
            cfg.CreateMap<OrderViewModel, Order>();
        }

        /// <summary>
        /// Creates admin user and coresponding role
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        private void SeedAdminUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var adminRole = new IdentityRole
                {
                    Name = "Administrator"
                };
                IdentityResult roleResult = roleManager.CreateAsync(adminRole).Result;
            }
            var user = userManager.FindByNameAsync("admin@example.com").Result;
            if (user == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                };

                IdentityResult result = userManager.CreateAsync(adminUser, "Admin123$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                }
            }
        }
    }
}

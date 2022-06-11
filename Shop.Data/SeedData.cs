using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ShopContext>>()))
            {
                var IsAllTablesHaveData = context.Products.Any() && context.Buyers.Any()
                    && context.Sales.Any();

                if (IsAllTablesHaveData)
                {
                    return;
                }


                var buyers = new List<Buyer>() { new Buyer { Id = 1, Name = "Lesha", Age = 24 }, new Buyer { Id = 2, Name = "Petya", Age = 16 }, 
                        new Buyer { Id = 3, Name = "Alex", Age = 25 } };

                var sales = new List<Sale>() { new Sale { Id = 1, BuyerId = 1, Buyer = buyers[0], TotalAmount = 485, DateTime = DateTime.Now},
                    new Sale { Id = 2, BuyerId = 2, Buyer = buyers[1], TotalAmount = 185, DateTime = DateTime.Now } };

                /*buyers[0].Sales = new HashSet<Sale>() { sales[0] };
                buyers[1].Sales = new HashSet<Sale>() { sales[1] };*/



                var products = new List<Product>() { new Product { Id = 1, Name = "Meat", Price = 300 }, new Product { Id = 2, Name = "Eggs", Price = 100 }, 
                        new Product { Id = 3, Name = "Milk", Price = 50 }, new Product { Id = 4, Name = "Bread", Price = 35 } };

                /*products[0].Sales = new HashSet<Sale>() { sales[0] };
                products[1].Sales = new HashSet<Sale>() { sales[0], sales[1] };
                products[2].Sales = new HashSet<Sale>() { sales[0], sales[1] };
                products[3].Sales = new HashSet<Sale>() { sales[0], sales[1] };*/

                /*sales[0].Products = new HashSet<Product>() { products[0], products[1], products[2], products[3]};
                sales[1].Products = new HashSet<Product>() { products[1], products[2], products[3] };*/

                await context.Buyers.AddRangeAsync(buyers);
                await context.Sales.AddRangeAsync(sales);
                await context.Products.AddRangeAsync(products);

                await context.SaveChangesAsync();
            }
        }
    }
}

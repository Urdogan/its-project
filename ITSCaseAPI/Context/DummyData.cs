using ITSCaseAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Context
{
    public class DummyData
    {
        public static void CreateData(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<RetailCompanyContext>();
            context.Database.Migrate();

            var products = new List<Product>
            {
                new Product{Title="Laptop",Barcode="528576233242",Description="Computer for personal use",Price=1499 },
                new Product{Title="Modem",Barcode="95847306873",Description="Get in the fast line",Price=199 },
                new Product{Title="Book",Barcode="548463867439",Description="The No 1 Sunday Times bestseller",Price=17 },
                new Product{Title="DVD",Barcode="6595473896574",Description="A mythic and emotionally charged hero’s journey",Price=32 },
                new Product{Title="Toy",Barcode="739684730953",Description="Realistically designed wheels really roll for driving play.",Price=78 },
            };

            var customers = new List<Customer>
            {
                new Customer{Name="Nazım", Address="Buca/İzmir"},
                new Customer{Name="Ali", Address="Merkezefendi/Denizli"},
            };

            var orders = new List<CustomerOrder>
            {
                new CustomerOrder{Customer = customers[0], OrderItems =new List<OrderItem> { new OrderItem { Quantity = 1, Product = products[0] } } },
                new CustomerOrder{Customer = customers[1], OrderItems = new List<OrderItem>{ new OrderItem { Quantity = 1, Product = products[1] }, new OrderItem { Quantity = 1, Product = products[3] }, new OrderItem { Quantity = 2, Product = products[2] } }} 
            };

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Product.Count() == 0)
                {
                    context.Product.AddRange(products);
                }

                if (context.Customer.Count() == 0)
                {
                    context.Customer.AddRange(customers);
                }
                if (context.CustomerOrder.Count() == 0)
                {
                    context.CustomerOrder.AddRange(orders);
                }

                context.SaveChanges();
            }
        }


    }
}

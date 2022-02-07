using ITSCaseAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Context
{
    public class RetailCompanyContext :DbContext
    {
        public RetailCompanyContext(DbContextOptions<RetailCompanyContext> options) : base(options)
        {

        }
        public  DbSet<Customer> Customer { get; set; }
        public  DbSet<Product> Product { get; set; }
        public  DbSet<CustomerOrder> CustomerOrder { get; set; }

        public  DbSet<OrderItem> OrderItem { get; set; }

    }
}

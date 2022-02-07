using ITSCaseAPI.Context;
using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly RetailCompanyContext _context;
        public CustomerOrderRepository(RetailCompanyContext context)
        {
            _context = context;
        }
        public void CreateCustomerOrder(CustomerOrder customerOrder)
        {
            throw new NotImplementedException();
        }

        public CustomerOrder GetCustomerOrder(int id)
        {
            return _context.CustomerOrder.Find(id);
        }
    }
}

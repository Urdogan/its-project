using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Repositories
{
    public interface ICustomerOrderRepository
    {
        CustomerOrder GetCustomerOrder(int id);
        void CreateCustomerOrder(CustomerOrder customerOrder);

    }
}

using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts();
        
    }
}

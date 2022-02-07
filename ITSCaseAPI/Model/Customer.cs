using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; init; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<CustomerOrder> Orders { get; set; }
    }
}

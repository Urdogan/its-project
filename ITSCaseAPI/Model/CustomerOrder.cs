using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Model
{
    public class CustomerOrder
    {
        [Key]
        public int CustomerOrderId { get; init; }
        public List<OrderItem> OrderItems { get; set; }
        public Customer Customer { get;set; }
        public string CustomerOrderAddress { get; set; }
    }
}

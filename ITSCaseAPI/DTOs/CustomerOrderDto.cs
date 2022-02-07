using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.DTO
{
    public class CustomerOrderDto
    {
        public int CustomerOrderId { get; init; }
        public List<OrderItemDto> OrderItems { get; set; }
        public CustomerDto Customer { get; set; }
        public string CustomerOrderAddress { get; set; }
    }
}

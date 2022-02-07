using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITSCaseAPI.Model
{
    public class OrderItem
    {

        [Key]
        public int OrderItemId { get; init; }
        public int Quantity { get; set; }

        public DateTimeOffset OrderCreationDate { get; init; }
        public Product Product { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }

}

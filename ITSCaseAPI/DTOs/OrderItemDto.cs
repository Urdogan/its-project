using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.DTO
{
    public class OrderItemDto
    {
        [Range(0,100)]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}

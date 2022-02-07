using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.DTO
{
    public class ProductDto
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}

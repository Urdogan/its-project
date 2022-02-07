using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.Model
{
    public class Product
    {
        [Key]
        public int Id { get; init; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
}

using ITSCaseAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSCaseAPI.DTO
{
    public class CustomerDto
    {

        public int CustomerId { get; init; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}

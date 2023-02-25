using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Models.Product
{
    public class ProductModels
    {

    }

    public class CreateProductRequest
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool Active { get; set; }
        public string? Description { get; set; }
    }
}

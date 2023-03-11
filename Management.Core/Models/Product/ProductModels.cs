using Management.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Models.Product
{
    public class ProductModels: CreateProductRequest 
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

    }

    public class CreateProductRequest : BaseModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool Active { get; set; }
        public string? Description { get; set; }
    }
}

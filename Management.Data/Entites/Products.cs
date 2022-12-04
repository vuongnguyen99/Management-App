using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class Products: BaseModel
    {
        public Guid Id { get; set; }
        public Guid ProductOwnerId { get; set; }
        public string? Name { get; set; }
        public int ViewCount { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ProductInImages>? ProductInImages { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public ProductOwners? ProductOwners { get; set; }
    }
}

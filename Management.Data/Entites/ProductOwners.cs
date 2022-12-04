using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class ProductOwners: BaseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid ProductId { get; set;  }
        public int Follower { get; set; }
        public int TotalProduct { get; set; }
        public virtual ICollection<Products>? Products { get; set; }
    }
}

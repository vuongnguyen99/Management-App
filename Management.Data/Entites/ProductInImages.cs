using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class ProductInImages: BaseModel
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ImageId { get; set; }
        public Products? Products { get; set; }
        public Images? Images { get; set; }
    }
}

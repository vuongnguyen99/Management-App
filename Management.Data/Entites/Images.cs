using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entites
{
    public class Images: BaseModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public long? Size { get; set; }
        public virtual ICollection<ProductInImages>? ProductInImages { get; set; }
    }
}

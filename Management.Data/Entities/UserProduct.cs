using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class UserProduct:BaseModel
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class OrderDetail: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}

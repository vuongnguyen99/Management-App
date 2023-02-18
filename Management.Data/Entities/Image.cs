using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.Entities
{
    public class Image: BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public long FileSize { get; set; }
        public Product? Products { get; set; }
        public User? User { get; set; }
    }
}

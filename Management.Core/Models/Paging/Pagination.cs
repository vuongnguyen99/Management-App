using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.Paging
{
    public class Pagination
    {
        public FilterUser? Filter { get; set; }
        public int StartIndex { get; set; } = 1;
        public int ItemPerPage { get; set; } = 20;
    }

    public class FilterUser
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

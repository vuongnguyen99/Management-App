using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Models.Paging
{
    public class GetUsersByProductIdRequest
    {
        public Sort? Sort { get; set; }
        public FilterUser? Filter { get; set; }
        public int StartIndex { get; set; } = 1;
        public int ItemPerPage { get; set; } = 20;
    }

    public class FilterUser
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProductName { get; set; }
        public string? ManageBy { get; set; }
        public string? RoleName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class Sort
    {
        public string? SortKey { get; set; }
    }
}

using Management.Data.Entities;

namespace Management_Core.Models.User
{
    public class UserModel: BaseModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UsersRole>? Roles { get; set; }
        public virtual ICollection<Products>? Products { get; set; }
        public Guid ManageBy { get; set; }  
    }


    public class Products
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
    }
    public class GetUsersByProductIdResponse
    {
        public GetUsersByProductId Result { get; set; }
        public int StartIndex { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 25;
    }
    public class GetUsersByProductId
    {
        public Guid ProducId { get; set; }
        public string ProductName { get; set; }
        public bool Active { get; set; }
        public ICollection<GetUsers> Users { get; set; }
    }

    public class GetUsers: BaseModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserManage ManageBy { get; set; }
        public bool Active { get; set; }
        public ICollection<UsersRole>? Roles { get; set; }
    }

    public class UsersRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserManage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}

using Management.Common.Common;
using Management.Common.Exception;
using Management.Data;
using Management.Data.Entities;
using Management_Core.Models.Paging;
using Management_Core.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Management.Core.Services
{
    public interface IUserServices
    {
        Task<CreateUserResponse> CreateNewUser(CreateUserRequest request, CancellationToken cancellationToken);
        Task<CreateUserResponse> UpdateUser(Guid Id, UpdateUser request, CancellationToken cancellationToken);
        Task<UserModel> GetUsersByIdAsync(Guid? userId, CancellationToken cancellationToken);
        Task<GetUsersByProductIdResponse> GetUsersByFilterPageAsync(Guid ProductId, GetUsersByProductIdRequest paging, CancellationToken cancellationToken);
        Task<string> DeleteUsersAsync(Guid userId, CancellationToken cancellationToken);
        Task<string> DeleteListUser(List<Guid> usersId, CancellationToken cancellationToken);
    }
    public class UserServices : IUserServices
    {
        private readonly ManagementDbContext _context;

        public UserServices(ManagementDbContext context)
        {
            _context = context;
        }
        public Task<CreateUserResponse> UpdateUser(Guid Id, UpdateUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }   
        public async Task<CreateUserResponse> CreateNewUser(CreateUserRequest request ,CancellationToken cancellationToken)
        {
            var users = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Active = true,
                PasswordHash = PasswordHelper.HashPassword(request.PasswordHash),
                ManagedBy = request.UserManagerId,
            };
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            var roles = await _context.Roles.Where(x => request.RoleId.Contains(x.Id)).ToListAsync(cancellationToken);

            foreach (var role in roles)
            {
                var userRole = new UserRole
                {
                    UserId = users.Id,
                    RoleId = role.Id
                };
                _context.UserRoles.Add(userRole);
            }

            var products = await _context.Products.Where(x => request.ProductId.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach(var newPro in products)
            {
                var userProduct = new UserProduct
                {
                    UserId = users.Id,
                    ProductId = newPro.Id
                };
                _context.UserProducts.Add(userProduct);
            }

            await _context.SaveChangesAsync();

            return new CreateUserResponse {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Username = users.LastName,
                Email = users.Email,
                Active = users.Active,
                CreateBy = users.CreateBy,
                CreateDate = users.CreateDate,
                ModifiedBy = users.ModifiedBy,
                ModifiedDate = users.ModifiedDate,
                PasswordHash = PasswordHelper.HashPassword(users.PasswordHash),
                UserRolesResponse = roles.Select(x => new CreateUserRoleResponse
                {
                    RoleId = x.Id,
                    RoleName = x.Name
                }).ToList(),
                UserProductsResponse = products.Select(y=> new CreateUserProductResponse
                {
                    ProductId = y.Id,
                    ProductName = y.Name
                }).ToList()
            };
        }

        public async Task<string> DeleteListUser(List<Guid> usersId, CancellationToken cancellationToken)
        {
            try
            {
                foreach(var userId in usersId)
                {
                    await DeleteUsersAsync(userId, cancellationToken);
                }
                return "Delete success";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteUsersAsync(Guid userId, CancellationToken cancellationToken)
        {
            var existingUsers = await _context.Users.FirstOrDefaultAsync(x=> x.Id == userId, cancellationToken);
            if(existingUsers== null)
            {
                throw new NotFoundException("Not found users");
            }
            _context.Users.Remove(existingUsers);
            await _context.SaveChangesAsync(cancellationToken);
            return "Delete success";
        }

        public async Task<GetUsersByProductIdResponse> GetUsersByFilterPageAsync(Guid ProductId, GetUsersByProductIdRequest request, CancellationToken cancellationToken)
        {
            var query = await(from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              join up in _context.UserProducts on u.Id equals up.UserId
                              join p in _context.Products on up.ProductId equals p.Id
                              where p.Id == ProductId
                              select new { u, ur, r, up, p }).ToListAsync();
            //filter
            if (request.Filter.FirstName != null)
            {
                query = query.Where(x => x.u.FirstName == request.Filter.FirstName).ToList();
            }
            if (request.Filter.LastName != null)
            {
                query = query.Where(x => x.u.LastName == request.Filter.LastName).ToList();
            }
            if (request.Filter.Email != null)
            {
                query = query.Where(x => x.u.Email == request.Filter.Email).ToList();
            }
            if (request.Filter.ProductName != null)
            {
                query = query.Where(x => x.p.Name == request.Filter.ProductName).ToList();
            }
            //if (request.Filter.ManageBy != null)
            //{
            //    query = query.Where(x => x.u.ManagedBy == request.Filter.ManageBy).ToList();
            //}
            if (request.Filter.RoleName != null)
            {
                query = query.Where(x => x.r.Name == request.Filter.RoleName).ToList();
            }

            var listUser = query.Skip(request.ItemPerPage * (request.StartIndex - 1)).Take(request.ItemPerPage);

            var manageUser = listUser.FirstOrDefault(x => x.u.ManagedBy == x.u.Id);
            var manageUserMapping = $"{manageUser.u.FirstName} {manageUser.u.LastName}";

            var result = new GetUsersByProductId
            {
                ProducId = listUser.FirstOrDefault().p.Id,
                ProductName = listUser.FirstOrDefault().p.Name,
                Active = listUser.FirstOrDefault().p.Active,
                Users = listUser.Select(x => new GetUsers
                {
                    UserId = x.u.Id,
                    FullName = $"{x.u.FirstName} {x.u.LastName}",
                    Email = x.u.Email,
                    Active = x.u.Active,
                    ManageBy = new UserManage
                    {
                        Id = x.u.ManagedBy.Value,
                        Name = manageUserMapping
                    },
                    Roles = new List<UsersRole>
                        {
                            new UsersRole
                            {
                                RoleId = x.r.Id,
                                RoleName = x.r.Name
                            }
                    }
                }).ToList()
            };

            return new GetUsersByProductIdResponse
            {
                Result = result,
                ItemsPerPage = request.ItemPerPage,
                StartIndex = request.StartIndex
            };
        }

        public async Task<UserModel> GetUsersByIdAsync(Guid? userId, CancellationToken cancellationToken)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(x=>x.Id==userId, cancellationToken);
            if(getUser==null)
            {
                throw new NotFoundException($"Cann't find {userId} in DB");
            }
            return new UserModel {
                Id = getUser.Id,
                LastName = getUser.LastName,
                FirstName = getUser.FirstName,
                Email = getUser.Email,
                UserName = getUser.Username,
                Active = getUser.Active,
                CreateBy= getUser.CreateBy,
                CreateDate= getUser.CreateDate,
                ModifiedBy= getUser.ModifiedBy,
                ModifiedDate= getUser.ModifiedDate,
            };
        }

        #region PrivateMethod
        private Dictionary<string, string> ValidateCreateAndUpdateUser(CreateUserRequest request)
        {
            var duplicateError = new Dictionary<string, string>();
            var checkUserEmail = _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (checkUserEmail != null)
            {
                duplicateError.Add("Email", "Email is already exist");
            }

            if (string.IsNullOrEmpty(request.FirstName))
            {
                duplicateError.Add("FirstName", "FirstName is required");
            }

            if (string.IsNullOrEmpty(request.LastName))
            {
                duplicateError.Add("LastName", "LastName is required");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                duplicateError.Add("Email", "Email is required");
            }

            if (string.IsNullOrEmpty(request.Username))
            {
                duplicateError.Add("Username", "Username is required");
            }
            return duplicateError;
        }


        #endregion
    }
}

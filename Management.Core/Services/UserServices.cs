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
        Task<CreateUserResponse> CreateNewUser(CreateUserRequest request,CancellationToken cancellationToken);
        Task<CreateUserResponse> UpdateUser(Guid Id, UpdateUser request, CancellationToken cancellationToken);
        Task<UserModel> GetUsersByIdAsync(Guid? userId, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserModel>> GetUsersByFilterPageAsync(Pagination paging, CancellationToken cancellationToken);
        Task DeleteUsersAsync(Guid userId, CancellationToken cancellationToken);
        Task DeleteListUser(List<GuidObject> GuidObject, CancellationToken cancellationToken);
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
        public async Task<CreateUserResponse> CreateNewUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var users = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Active = true,
                PasswordHash = PasswordHelper.HashPassword(request.PasswordHash),
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
                }).ToList()
            };
        }

        public Task DeleteListUser(List<GuidObject> GuidObject, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUsersAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<UserModel>> GetUsersByFilterPageAsync(Pagination paging, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        private string ValidatePaging(Pagination paging)
        {
            return "Validate Failed";
        }
        #endregion
    }
}

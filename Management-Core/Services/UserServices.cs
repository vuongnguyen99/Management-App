using Management.Data;
using Management.Data.Entities;
using Management_Common.Common;
using Management_Common.Exception;
using Management_Core.Interface;
using Management_Core.Models.Paging;
using Management_Core.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Management_Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly ManagementDbContext _dbContext;
        public UserServices(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateUserResponse> CreateNewUser(CreateUserRequest request, Guid? RolesId, CancellationToken cancellationToken)
        {
            ValidateCreateAndUpdateUser(request);
            var newUser = new User
            {
                FirstName = request.FirstName.ToLower(),
                LastName = request.LastName.ToLower(),
                Active = request.Active || true,
                Email = request.Email.ToLower(),
                PasswordHash = PasswordHelper.HashPassword(request.PasswordHash),
                Username = request.Email.Split('@')[0],
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var UserRoles = new List<UserRole>();

            var roleObj = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == RolesId, cancellationToken);
            if (roleObj != null)
            {
                var assignRole = new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = roleObj.Id
                };
                
                UserRoles.Add(assignRole);

            }
            _dbContext.UserRoles.AddRange(UserRoles);
            await _dbContext.SaveChangesAsync();


            var getNewUserRole = await _dbContext.UserRoles.Include(x => x.Role).Where(x => x.UserId == newUser.Id).ToListAsync();
            var userRoles = new List<CreateUserRoleResponse>();
            foreach (var item in getNewUserRole)
            {
                userRoles.Add(new CreateUserRoleResponse()
                {
                    RoleId = item.Role.Id,
                    RoleName = item.Role.Name,
                });
            }
            return new CreateUserResponse
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Active = newUser.Active,
                PasswordHash = newUser.PasswordHash,
                Username = newUser.Username,
                UserRolesResponse = userRoles,
                CreateBy = newUser.CreateBy,
                CreateDate = newUser.CreateDate,
                ModifiedBy = newUser.ModifiedBy,
                ModifiedDate = newUser.ModifiedDate
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

        public Task<UserModel> GetUsersByIdAsync(Guid? userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CreateUserResponse> UpdateUser(Guid Id, UpdateUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #region PrivateMethod
        private Dictionary<string, string> ValidateCreateAndUpdateUser(CreateUserRequest request)
        {
            var duplicateError = new Dictionary<string, string>();
            var checkUserEmail = _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
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

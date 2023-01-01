using Management.Data;
using Management.Data.Entities;
using Management_Common.Common;
using Management_Common.Exception;
using Management_Core.Interface;
using Management_Core.Models.Paging;
using Management_Core.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Management_Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly ManagementDbContext _dbContext;
        public UserServices(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateUserResponse> CreateNewUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Active = request.Active || true,
                Email = request.Email,
                PasswordHash = PasswordHelper.HashPassword(request.PasswordHash),
                Username = string.IsNullOrEmpty(request.Username) ? $"{request.FirstName}{request.LastName}" : request.Username,
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var newListRole = new List<UserRole>();
            var getRole = await (from role in _dbContext.Roles
                                 select new { role.Name, role.Id }).ToListAsync();
            foreach (var role in getRole)
            {
                newListRole.Add(new UserRole()
                {
                    RoleId = role.Id,
                    UserId = newUser.Id
                });
            }
            await _dbContext.UserRoles.AddRangeAsync(newListRole);
            await _dbContext.SaveChangesAsync();
            //var getNewUserRole = await GetUserRoleByUserId(newUser.Id, cancellationToken);
            var getNewUserRole = await _dbContext.UserRoles.Include(x=>x.Role).Where(x => x.UserId == newUser.Id).ToListAsync();
            var ur = new List<CreateUserRoleResponse>();
            foreach(var item in getNewUserRole)
            {
                ur.Add(new CreateUserRoleResponse()
                {
                    RoleId = item.Role.Id,
                    RoleName = item.Role.Name,
                });
            }
            Console.WriteLine("userRole: ", ur);
            return new CreateUserResponse
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Active = newUser.Active,
                PasswordHash= newUser.PasswordHash,
                Username = newUser.Username,
                UserRolesResponse = ur,
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
        //private Dictionary<string, string> ValidateCreateAndUpdateUser (CreateUser request)
        //{

        //}

        private string ValidatePaging(Pagination paging)
        {
            return "Validate Failed";
        }
        #endregion
    }
}

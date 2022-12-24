using Management.Data;
using Management.Data.Entites;
using Management_Core.Interface;
using Management_Core.Models.Paging;
using Management_Core.Models.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly ProductEntitlementsContext _dbContext;
        public UserServices(ProductEntitlementsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<CreateUserResponse> CreateNewUser(CreateUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        private string ValidatePaging (Pagination paging)
        {
            return "Validate Failed";
        }
        #endregion
    }
}

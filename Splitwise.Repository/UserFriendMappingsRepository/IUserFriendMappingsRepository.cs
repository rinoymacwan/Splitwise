using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.UserFriendMappingsRepository
{
    public interface IUserFriendMappingsRepository
    {
        IEnumerable<UserFriendMappingsAC> GetUserFriendMappings();
        Task<UserFriendMappingsAC> GetUserFriendMapping(int id);
        void CreateUserFriendMapping(UserFriendMappings UserFriendMapping);
        Task<UsersAC> CreateUserFriendMappingByEmail(string id, Users user);
        void UpdateUserFriendMapping(UserFriendMappings UserFriendMapping);
        Task DeleteUserFriendMapping(UserFriendMappingsAC UserFriendMapping);
        Task DeleteUserFriendMappingByIds(string id1, string id2);
        Task SaveAsync();
        bool UserFriendMappingExists(string id);
    }
}

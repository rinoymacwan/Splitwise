using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.UsersRepository
{
    public interface IUsersRepository : IDisposable
    {
        IEnumerable<UsersAC> GetUsers();
        IEnumerable<UsersAC> GetAllFriends(string id);
        Task<UsersAC> GetUser(string id);
        Task<UsersAC> GetUserByEmail(string email);
        Task CreateUser(Users user); 
        void UpdateUser(Users user);
        Task DeleteUser(UsersAC user);
        Task Save();
        bool UserExists(string id);
        Task<UsersAC> Authenticate(Users user);
        Task<bool> Login(Users credentials);
        Task<string> GenerateJWT(Users credentials);
    }
}

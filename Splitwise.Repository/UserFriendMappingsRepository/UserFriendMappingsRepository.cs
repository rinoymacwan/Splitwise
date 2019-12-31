using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;
using Splitwise.Repository.UsersRepository;

namespace Splitwise.Repository.UserFriendMappingsRepository
{
    public class UserFriendMappingsRepository : IUserFriendMappingsRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;

        public UserFriendMappingsRepository(SplitwiseContext context, IUsersRepository usersRepository, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            this._usersRepository = usersRepository;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public void CreateUserFriendMapping(UserFriendMappings userFriendMapping)
        {
            UserFriendMappings otherEntry = new UserFriendMappings() { UserId = userFriendMapping.FriendId, FriendId = userFriendMapping.UserId };
            dataRepository.Add(userFriendMapping);
            dataRepository.Add(otherEntry);
        }
        public async Task<UsersAC> CreateUserFriendMappingByEmail(string id, Users user)
        {
            string email = user.Email;
            var x = await _usersRepository.GetUserByEmail(email);
            if (x != null)
            {
                context.UserFriendMappings.Add(new UserFriendMappings() { UserId = id, FriendId = x.Id });
                context.UserFriendMappings.Add(new UserFriendMappings() { UserId = x.Id, FriendId = id });
                // _userFriendMappingsRepository.CreateUserFriendMapping(new UserFriendMappings() { UserId = id, FriendId = x.Id });
                //  _userFriendMappingsRepository.CreateUserFriendMapping(new UserFriendMappings() { UserId = x.Id, FriendId = id });
                await SaveAsync();
                return x;
            }
            else
            {
                return _mapper.Map<UsersAC>(new Users());
            }
        }
        public async Task DeleteUserFriendMapping(UserFriendMappingsAC UserFriendMapping)
        {
            var x = await dataRepository.GetAll<UserFriendMappings>().Where(k => k.UserId == UserFriendMapping.FriendId && k.FriendId == UserFriendMapping.UserId).FirstOrDefaultAsync();
            var y = await dataRepository.FindAsync<UserFriendMappings>(UserFriendMapping.Id);
            dataRepository.Remove(y);
            dataRepository.Remove(x);
        }

        public async Task DeleteUserFriendMappingByIds(string id1, string id2)
        {
            var x = await dataRepository.GetAll<UserFriendMappings>().Where(k => k.UserId == id1 && k.FriendId == id2).FirstOrDefaultAsync();
            var y = await dataRepository.GetAll<UserFriendMappings>().Where(k => k.UserId == id2 && k.FriendId == id1).FirstOrDefaultAsync();
            // var z = await context.UserFriendMappings.ToListAsync();

            dataRepository.Remove(x);
            dataRepository.Remove(y);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<UserFriendMappingsAC> GetUserFriendMapping(int id)
        {
            return _mapper.Map<UserFriendMappingsAC>(await dataRepository.FindAsync<UserFriendMappings>(id));
        }

        public IEnumerable<UserFriendMappingsAC> GetUserFriendMappings()
        {
            //System.Diagnostics.Debug.WriteLine("ASJKHDSJKHSDK");
            return _mapper.Map<IEnumerable<UserFriendMappingsAC>>(dataRepository.GetAll<UserFriendMappings>());
        }

        public async Task SaveAsync()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateUserFriendMapping(UserFriendMappings UserFriendMapping)
        {
            dataRepository.Entry(UserFriendMapping);
        }

        public bool UserFriendMappingExists(string id)
        {
            return context.Users.Any(e => e.Id == id);
        }
    }
}

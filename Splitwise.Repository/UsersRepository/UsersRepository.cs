﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;
using Splitwise.Utility;
using Splitwise.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Splitwise.Repository.UsersRepository
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly UserManager<Users> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;

        public UsersRepository(SplitwiseContext context, UserManager<Users> _userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            this._userManager = _userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public IEnumerable<UsersAC> GetUsers()
        {
            return _mapper.Map<IEnumerable<UsersAC>>(_userManager.Users);
        }

        public async Task<UsersAC> GetUser(string id)
        { 
            return _mapper.Map<UsersAC>(await _userManager.Users.Where(k => k.Id ==id).FirstAsync());
        }

        public async Task<UsersAC> GetUserByEmail(string email)
        {
            return _mapper.Map<UsersAC>(await _userManager.Users.Where(k => k.Email == email).SingleOrDefaultAsync());
        }

        public IEnumerable<UsersAC> GetAllFriends(string id)
        {
            return _mapper.Map<IEnumerable<UsersAC>>(dataRepository.GetAll<UserFriendMappings>().Where(u => u.UserId == id).Select(x => x.Friend));
        }

        public async Task CreateUser(Users user)
        {
            await _userManager.CreateAsync(user, user.Password);
        }

        public void UpdateUser(Users user)
        {
            context.Entry(user).State = EntityState.Modified;
        }


        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }
        

        async Task IUsersRepository.DeleteUser(UsersAC user)
        {
            var x = await context.Users.FindAsync(user.Id);
            await _userManager.DeleteAsync(x);
        }

        public bool UserExists(string id)
        {
            return context.Users.Any(e => e.Id == id);
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        public async Task<bool> Login(Users credentials)
        {
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            var user = await _userManager.FindByEmailAsync(credentials.UserName);
            if(identity == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<string> GenerateJWT(Users credentials)
        {
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            var user = await _userManager.FindByEmailAsync(credentials.UserName);
            var jwt = await Tokens.GenerateJwt(user, identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return jwt;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);
            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);
            var x = await _userManager.Users.ToListAsync();

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
        public async Task<UsersAC> Authenticate(Users user)
        {
            var x = await context.Users.Where(k => k.Email == user.Email && k.Password == user.Password).SingleOrDefaultAsync();
            if(x != null)
            {
                return _mapper.Map<UsersAC>(x);
            } else
            {
                return _mapper.Map<UsersAC>(new Users());
            }
        }
    }
}

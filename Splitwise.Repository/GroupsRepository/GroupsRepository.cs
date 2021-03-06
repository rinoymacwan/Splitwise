﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;
using Splitwise.Repository.ExpensesRepository;
using Splitwise.Repository.GroupMemberMappingsRepository;
using Splitwise.Repository.UserFriendMappingsRepository;

namespace Splitwise.Repository.GroupsRepository
{
    public class GroupsRepository : IGroupsRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IGroupMemberMappingsRepository _groupMemberMappingsRepository;
        private readonly IUserFriendMappingsRepository _userFriendMappingsRepository;
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;

        public GroupsRepository(SplitwiseContext _context, IGroupMemberMappingsRepository groupMemberMappingsRepository, IUserFriendMappingsRepository userFriendMappingsRepository, IExpensesRepository expensesRepository, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = _context;
            this._groupMemberMappingsRepository = groupMemberMappingsRepository;
            this._userFriendMappingsRepository = userFriendMappingsRepository;
            this._expensesRepository = expensesRepository;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public bool GroupExists(int id)
        {
            return context.Groups.Any(e => e.Id == id);
        }

        public void CreateGroup(Groups Group)
        {
            dataRepository.Add(Group);
        }

        public async Task DeleteGroup(GroupsAC Group)
        {
            await _groupMemberMappingsRepository.DeleteGroupMemberMappingByGroupId(Group.Id);
            await _expensesRepository.DeleteExpensesByGroupId(Group.Id);
            var x = await dataRepository.FindAsync<Groups>(Group.Id);
            dataRepository.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<GroupsAC> GetGroups()
        {
            return _mapper.Map<IEnumerable<GroupsAC>>(dataRepository.GetAll<Groups>());
        }
        public IEnumerable<GroupsAC> GetGroupsByUserId(string id)
        {
            return _mapper.Map<IEnumerable<GroupsAC>>(_groupMemberMappingsRepository.GetGroupMemberMappings().Where(g => g.MemberId == id).Select(k => k.Group).ToList());
        }

        public async Task<GroupsAC> GetGroup(int id)
        {
            return _mapper.Map<GroupsAC>(await dataRepository.GetAll<Groups>().Include(u => u.User).FirstOrDefaultAsync(i => i.Id == id));
        }

        public async Task<GroupAndMembersAC> GetGroupWithDetails(int id)
        {
            var group =  _mapper.Map<GroupsAC>(await dataRepository.GetAll<Groups>().Include(u => u.User).FirstOrDefaultAsync(i => i.Id == id));
            var members = _groupMemberMappingsRepository.GetGroupMemberMappings().Where(g => g.GroupId == id).Select(k => k.User).ToList();

            return new GroupAndMembersAC() { Group = group, Members = members };
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateGroup(Groups Group)
        {
            dataRepository.GetAll<Groups>();
        }
    }
}

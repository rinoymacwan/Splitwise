using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupMemberMappingsRepository
{
    public class GroupMemberMappingsRepository : IGroupMemberMappingsRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;

        public GroupMemberMappingsRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public bool GroupMemberMappingExists(int id)
        {
            return context.GroupMemberMappings.Any(e => e.Id == id);
        }

        public async Task CreateGroupMemberMapping(GroupMemberMappings GroupMemberMapping)
        {
            dataRepository.Add(GroupMemberMapping);
            await Save();
        }

        public async Task DeleteGroupMemberMapping(GroupMemberMappingsAC GroupMemberMapping)
        {
            var x = await dataRepository.FindAsync<GroupMemberMappings>(GroupMemberMapping.Id);
            dataRepository.Remove(x);
        }

        public async Task DeleteGroupMemberMappingByGroupId(int id)
        {
            var x = dataRepository.Where<GroupMemberMappings>(k => k.GroupId == id);
            foreach( var y in x)
            {
                dataRepository.Remove(y);
            }
            
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<GroupMemberMappingsAC> GetGroupMemberMappings()
        {
            return _mapper.Map<IEnumerable<GroupMemberMappingsAC>>(dataRepository.GetAll<GroupMemberMappings>().Include(k => k.User).Include(l => l.Group));
        }

        public async Task<GroupMemberMappingsAC> GetGroupMemberMapping(int id)
        {
            return _mapper.Map<GroupMemberMappingsAC>(await dataRepository.FindAsync<GroupMemberMappings>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateGroupMemberMapping(GroupMemberMappings GroupMemberMapping)
        {
            dataRepository.Entry(GroupMemberMapping);
        }
    }
}

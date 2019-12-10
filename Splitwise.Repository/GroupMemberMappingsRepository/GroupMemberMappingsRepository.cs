using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
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


        public GroupMemberMappingsRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool GroupMemberMappingExists(int id)
        {
            return context.GroupMemberMappings.Any(e => e.Id == id);
        }

        public async Task CreateGroupMemberMapping(GroupMemberMappings GroupMemberMapping)
        {
            context.GroupMemberMappings.Add(GroupMemberMapping);
            await Save();
        }

        public async Task DeleteGroupMemberMapping(GroupMemberMappingsAC GroupMemberMapping)
        {
            var x = await context.GroupMemberMappings.FindAsync(GroupMemberMapping.Id);
            context.GroupMemberMappings.Remove(x);
        }

        public async Task DeleteGroupMemberMappingByGroupId(int id)
        {
            var x = context.GroupMemberMappings.Where(k => k.GroupId == id);
            foreach( var y in x)
            {
                context.GroupMemberMappings.Remove(y);
            }
            
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<GroupMemberMappingsAC> GetGroupMemberMappings()
        {
            return _mapper.Map<IEnumerable<GroupMemberMappingsAC>>(context.GroupMemberMappings.Include(k => k.User).Include(l => l.Group));
        }

        public async Task<GroupMemberMappingsAC> GetGroupMemberMapping(int id)
        {
            return _mapper.Map<GroupMemberMappingsAC>(await context.GroupMemberMappings.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdateGroupMemberMapping(GroupMemberMappings GroupMemberMapping)
        {
            context.Entry(GroupMemberMapping).State = EntityState.Modified;
        }
    }
}

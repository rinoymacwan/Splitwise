using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupMemberMappingsRepository
{
    public interface IGroupMemberMappingsRepository
    {
        IEnumerable<GroupMemberMappingsAC> GetGroupMemberMappings();
        Task<GroupMemberMappingsAC> GetGroupMemberMapping(int id);
        Task CreateGroupMemberMapping(GroupMemberMappings GroupMemberMapping);
        void UpdateGroupMemberMapping(GroupMemberMappings GroupMemberMapping);
        Task DeleteGroupMemberMapping(GroupMemberMappingsAC GroupMemberMapping);
        Task DeleteGroupMemberMappingByGroupId(int id);
        Task Save();
        bool GroupMemberMappingExists(int id);
    }
}

using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.GroupsRepository
{
    public interface IGroupsRepository
    {
        IEnumerable<GroupsAC> GetGroups();
        IEnumerable<GroupsAC> GetGroupsByUserId(string id);
        Task<GroupsAC> GetGroup(int id);
        Task<GroupAndMembersAC> GetGroupWithDetails(int id);
        void CreateGroup(Groups Group);
        void UpdateGroup(Groups Group);
        Task DeleteGroup(GroupsAC Group);
        Task Save();
        bool GroupExists(int id);
    }
}

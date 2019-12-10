using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class GroupAndMembersAC
    {
        public GroupsAC Group { get; set; }
        public List<UsersAC> Members { get; set; }
    }
}

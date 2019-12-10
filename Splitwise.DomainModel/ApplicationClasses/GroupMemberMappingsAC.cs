using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class GroupMemberMappingsAC
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public GroupsAC Group { get; set; }
        public string MemberId { get; set; }
        public UsersAC User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class GroupsAC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MadeById { get; set; }
        public UsersAC User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class ActivitiesAC
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public UsersAC User { get; set; }

        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class UserHubModelsAC
    {
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}

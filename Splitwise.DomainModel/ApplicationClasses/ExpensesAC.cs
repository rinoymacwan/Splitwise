using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class ExpensesAC
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public GroupsAC Group { get; set; }

        public string AddedById { get; set; }
        public UsersAC User { get; set; }

        public DateTime DateTime { get; set; }

        public int? CategoryId { get; set; }
        public CategoriesAC Category { get; set; }
            
        public string Currency{ get; set; }
        public int Total { get; set; }
        public string SplitBy { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class PayeesAC
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public ExpensesAC Expense { get; set; }
        public string PayeeId { get; set; }
        public UsersAC User { get; set; }
        public int PayeeShare { get; set; }
    }
}

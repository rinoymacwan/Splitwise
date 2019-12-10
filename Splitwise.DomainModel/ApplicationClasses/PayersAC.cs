using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.ApplicationClasses
{
    public class PayersAC
    {
        public int Id { get; set; }

        public int ExpenseId { get; set; }
        public ExpensesAC Expense { get; set; }
        public string PayerId { get; set; }
        public UsersAC User { get; set; }
        public int AmountPaid { get; set; }
        public int PayerShare { get; set; }
    }
}

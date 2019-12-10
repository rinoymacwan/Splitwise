using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.ExpensesRepository
{
    public interface IExpensesRepository
    {
        IEnumerable<ExpensesAC> GetExpenses();
        Task<ExpensesAC> GetExpense(int id);
        void CreateExpense(Expenses Expense);
        void UpdateExpense(Expenses Expense);
        Task DeleteExpense(ExpensesAC Expense);
        Task DeleteExpensesByGroupId(int id);
        Task Save();
        bool ExpenseExists(int id);
    }
}

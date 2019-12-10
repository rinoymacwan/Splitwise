using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;

namespace Splitwise.Repository.ExpensesRepository
{
    public class ExpensesRepository : IExpensesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        public ExpensesRepository(SplitwiseContext context, IMapper mapper)
        {
            _mapper = mapper;
            this.context = context;
        }
        public bool ExpenseExists(int id)
        {
            return context.Expenses.Any(e => e.Id == id);
        }

        public void CreateExpense(Expenses Expense)
        {
            context.Expenses.Add(Expense);
        }

        public async Task DeleteExpense(ExpensesAC Expense)
        {
            var x = context.Payers.Where(k => k.ExpenseId == Expense.Id);
            var y = context.Payees.Where(k => k.ExpenseId == Expense.Id);

            foreach(var temp in x)
            {
                context.Payers.Remove(temp);
            }
            foreach (var temp in y)
            {
                context.Payees.Remove(temp);
            }
            var z = await context.Expenses.FindAsync(Expense);
            context.Expenses.Remove(z);
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<ExpensesAC> GetExpenses()
        {
            return _mapper.Map<IEnumerable<ExpensesAC>>(context.Expenses);
        }

        public async Task<ExpensesAC> GetExpense(int id)
        {
            return _mapper.Map<ExpensesAC>(await context.Expenses.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdateExpense(Expenses Expense)
        {
            context.Entry(Expense).State = EntityState.Modified;
        }

        public async Task DeleteExpensesByGroupId(int id)
        {
            var listOfExpenses = context.Expenses.Where(k => k.GroupId == id);
            foreach(var expense in listOfExpenses )
            {
                var x = context.Payers.Where(k => k.ExpenseId == expense.Id);
                var y = context.Payees.Where(k => k.ExpenseId == expense.Id);

                foreach (var temp in x)
                {
                    context.Payers.Remove(temp);
                }
                foreach (var temp in y)
                {
                    context.Payees.Remove(temp);
                }
                context.Expenses.Remove(expense);
            }
        }
    }
}

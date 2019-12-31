using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;

namespace Splitwise.Repository.ExpensesRepository
{
    public class ExpensesRepository : IExpensesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;
        public ExpensesRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            _mapper = mapper;
            this.context = context;
            dataRepository = _dataRepository;

        }
        public bool ExpenseExists(int id)
        {
            return context.Expenses.Any(e => e.Id == id);
        }

        public void CreateExpense(Expenses Expense)
        {
            dataRepository.Add(Expense);
        }

        public async Task DeleteExpense(ExpensesAC Expense)
        {
            var x = dataRepository.Where<Payers>(k => k.ExpenseId == Expense.Id);
            var y = dataRepository.Where<Payees>(k => k.ExpenseId == Expense.Id);

            foreach(var temp in x)
            {
                dataRepository.Remove(temp);
            }
            foreach (var temp in y)
            {
                dataRepository.Remove(temp);
            }
            var z = await dataRepository.FindAsync<Expenses>(Expense.Id);
            context.Expenses.Remove(z);
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<ExpensesAC> GetExpenses()
        {
            return _mapper.Map<IEnumerable<ExpensesAC>>(dataRepository.GetAll<Expenses>());
        }

        public async Task<ExpensesAC> GetExpense(int id)
        {
            return _mapper.Map<ExpensesAC>(await dataRepository.FindAsync<Expenses>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateExpense(Expenses Expense)
        {
            dataRepository.Entry(Expense);
        }

        public async Task DeleteExpensesByGroupId(int id)
        {
            var listOfExpenses = dataRepository.Where<Expenses>(k => k.GroupId == id);
            foreach(var expense in listOfExpenses )
            {
                var x = dataRepository.Where<Payers>(k => k.ExpenseId == expense.Id);
                var y = dataRepository.Where<Payees>(k => k.ExpenseId == expense.Id);

                foreach (var temp in x)
                {
                    dataRepository.Remove(temp);
                }
                foreach (var temp in y)
                {
                    dataRepository.Remove(temp);
                }
                dataRepository.Remove(expense);
            }
        }
    }
}

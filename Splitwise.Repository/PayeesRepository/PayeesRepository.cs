using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.PayeesRepository
{
    public class PayeesRepository : IPayeesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        public PayeesRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool PayeeExists(int id)
        {
            return context.Payees.Any(e => e.Id == id);
        }

        public void CreatePayee(Payees Payee)
        {
            context.Payees.Add(Payee);
        }

        public async Task DeletePayee(PayeesAC Payee)
        {
            var x = await context.Payees.FindAsync(Payee.Id);
            context.Payees.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<PayeesAC> GetPayees()
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(context.Payees.Include(t => t.User));
        }
        public IEnumerable<PayeesAC> GetPayeesByExpenseId(int id)
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(context.Payees.Include(t => t.User).Where(e => e.ExpenseId == id).ToList());
        }

        public IEnumerable<PayeesAC> GetPayeesByPayeeId(string id)
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(this.GetPayees().Where(e => e.PayeeId == id).ToList());
        }

        public async Task<PayeesAC> GetPayee(int id)
        {
            return _mapper.Map<PayeesAC>(await context.Payees.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdatePayee(Payees Payee)
        {
            context.Entry(Payee).State = EntityState.Modified;
        }
    }
}

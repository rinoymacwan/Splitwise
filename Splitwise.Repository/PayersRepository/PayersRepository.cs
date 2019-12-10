using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.PayersRepository
{
    public class PayersRepository : IPayersRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        public PayersRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool PayerExists(int id)
        {
            return context.Payers.Any(e => e.Id == id);
        }

        public void CreatePayer(Payers Payer)
        {
            context.Payers.Add(Payer);
        }

        public async Task DeletePayer(PayersAC Payer)
        {
            var x = await context.Payers.FindAsync(Payer.Id);
            context.Payers.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<PayersAC> GetPayers()
        {
            return _mapper.Map<IEnumerable<PayersAC>>(context.Payers.Include(t => t.User));
        }

        public IEnumerable<PayersAC> GetPayersByExpenseId(int id)
        {
            return _mapper.Map<IEnumerable<PayersAC>>(context.Payers.Include(t => t.User).Where(e => e.ExpenseId == id).ToList());
        }

        public IEnumerable<PayersAC> GetPayersByPayerId(string id)
        {
            return _mapper.Map<IEnumerable<PayersAC>>(context.Payers.Include(t => t.User).Where(e => e.PayerId == id).ToList());
        }

        public async Task<PayersAC> GetPayer(int id)
        {
            return _mapper.Map<PayersAC>(await context.Payers.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdatePayer(Payers Payer)
        {
            context.Entry(Payer).State = EntityState.Modified;
        }
    }
}

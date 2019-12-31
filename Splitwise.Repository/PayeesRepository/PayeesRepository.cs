using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;
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
        private readonly IDataRepository dataRepository;
        public PayeesRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public bool PayeeExists(int id)
        {
            return context.Payees.Any(e => e.Id == id);
        }

        public void CreatePayee(Payees Payee)
        {
            dataRepository.Add(Payee);
        }

        public async Task DeletePayee(PayeesAC Payee)
        {
            var x = await dataRepository.FindAsync<Payees>(Payee.Id);
            dataRepository.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<PayeesAC> GetPayees()
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(dataRepository.GetAll<Payees>().Include(t => t.User));
        }
        public IEnumerable<PayeesAC> GetPayeesByExpenseId(int id)
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(dataRepository.GetAll<Payees>().Include(t => t.User).Where(e => e.ExpenseId == id).ToList());
        }

        public IEnumerable<PayeesAC> GetPayeesByPayeeId(string id)
        {
            return _mapper.Map<IEnumerable<PayeesAC>>(this.GetPayees().Where(e => e.PayeeId == id).ToList());
        }

        public async Task<PayeesAC> GetPayee(int id)
        {
            return _mapper.Map<PayeesAC>(await dataRepository.FindAsync<Payees>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdatePayee(Payees Payee)
        {
            dataRepository.Entry(Payee);
        }
    }
}

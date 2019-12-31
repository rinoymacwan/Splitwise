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

namespace Splitwise.Repository.PayersRepository
{
    public class PayersRepository : IPayersRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;
        public PayersRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public bool PayerExists(int id)
        {
            return context.Payers.Any(e => e.Id == id);
        }

        public void CreatePayer(Payers Payer)
        {
            dataRepository.Add(Payer);
        }

        public async Task DeletePayer(PayersAC Payer)
        {
            var x = await dataRepository.FindAsync<Payers>(Payer.Id);
            dataRepository.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<PayersAC> GetPayers()
        {
            return _mapper.Map<IEnumerable<PayersAC>>(dataRepository.GetAll<Payers>().Include(t => t.User));
        }

        public IEnumerable<PayersAC> GetPayersByExpenseId(int id)
        {
            return _mapper.Map<IEnumerable<PayersAC>>(dataRepository.GetAll<Payers>().Include(t => t.User).Where(e => e.ExpenseId == id).ToList());
        }

        public IEnumerable<PayersAC> GetPayersByPayerId(string id)
        {
            return _mapper.Map<IEnumerable<PayersAC>>(dataRepository.GetAll<Payers>().Include(t => t.User).Where(e => e.PayerId == id).ToList());
        }

        public async Task<PayersAC> GetPayer(int id)
        {
            return _mapper.Map<PayersAC>(await dataRepository.FindAsync<Payers>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdatePayer(Payers Payer)
        {
            dataRepository.Entry(Payer);
        }
    }
}

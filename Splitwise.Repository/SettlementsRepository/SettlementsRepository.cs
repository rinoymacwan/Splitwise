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

namespace Splitwise.Repository.SettlementsRepository
{
    public class SettlementsRepository : ISettlementsRepository, IDisposable
    {
        private SplitwiseContext context;
        IMapper _mapper;
        private readonly IDataRepository dataRepository;
        public SettlementsRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository;
        }
        public bool SettlementExists(int id)
        {
            return context.Settlements.Any(e => e.Id == id);
        }

        public void CreateSettlement(Settlements Settlement)
        {
            dataRepository.Add(Settlement);
        }

        public async Task DeleteSettlement(SettlementsAC Settlement)
        {
            var x = await dataRepository.FindAsync<Settlements>(Settlement.Id);
            context.Settlements.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<SettlementsAC> GetSettlements()
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(dataRepository.GetAll<Settlements>().Include(p => p.Payee).Include(l => l.Payer));
        }

        public IEnumerable<SettlementsAC> GetSettlementsByUserId(string id)
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(dataRepository.GetAll<Settlements>().Include(p => p.Payee).Include(l => l.Payer).Where(s => s.PayeeId == id || s.PayerId == id).ToList());
        }

        public IEnumerable<SettlementsAC> GetSettlementsByGroupId(int id)
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(dataRepository.GetAll<Settlements>().Include(p => p.Payee).Include(l => l.Payer).Where(s => s.GroupId == id).ToList());
        }

        public async Task<SettlementsAC> GetSettlement(int id)
        {
            return _mapper.Map<SettlementsAC>(await dataRepository.FindAsync<Settlements>(id));
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateSettlement(Settlements Settlement)
        {
            context.Entry(Settlement);
        }
    }
}

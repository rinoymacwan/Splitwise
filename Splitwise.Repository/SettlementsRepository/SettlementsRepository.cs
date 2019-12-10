using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
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
        public SettlementsRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool SettlementExists(int id)
        {
            return context.Settlements.Any(e => e.Id == id);
        }

        public void CreateSettlement(Settlements Settlement)
        {
            context.Settlements.Add(Settlement);
        }

        public async Task DeleteSettlement(SettlementsAC Settlement)
        {
            var x = await context.Settlements.FindAsync(Settlement.Id);
            context.Settlements.Remove(x);
        }

        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<SettlementsAC> GetSettlements()
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(context.Settlements.Include(p => p.Payee).Include(l => l.Payer));
        }

        public IEnumerable<SettlementsAC> GetSettlementsByUserId(string id)
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(context.Settlements.Include(p => p.Payee).Include(l => l.Payer).Where(s => s.PayeeId == id || s.PayerId == id).ToList());
        }

        public IEnumerable<SettlementsAC> GetSettlementsByGroupId(int id)
        {
            return _mapper.Map<IEnumerable<SettlementsAC>>(context.Settlements.Include(p => p.Payee).Include(l => l.Payer).Where(s => s.GroupId == id).ToList());
        }

        public async Task<SettlementsAC> GetSettlement(int id)
        {
            return _mapper.Map<SettlementsAC>(await context.Settlements.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdateSettlement(Settlements Settlement)
        {
            context.Entry(Settlement).State = EntityState.Modified;
        }
    }
}

using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.SettlementsRepository
{
    public interface ISettlementsRepository
    {
        IEnumerable<SettlementsAC> GetSettlements();
        IEnumerable<SettlementsAC> GetSettlementsByUserId(string id);
        IEnumerable<SettlementsAC> GetSettlementsByGroupId(int id);
        Task<SettlementsAC> GetSettlement(int id);
        void CreateSettlement(Settlements Settlement);
        void UpdateSettlement(Settlements Settlement);
        Task DeleteSettlement(SettlementsAC Settlement);
        Task Save();
        bool SettlementExists(int id);
    }
}

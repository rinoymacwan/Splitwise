using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.PayersRepository
{
    public interface IPayersRepository
    {
        IEnumerable<PayersAC> GetPayers();
        IEnumerable<PayersAC> GetPayersByExpenseId(int id);
        IEnumerable<PayersAC> GetPayersByPayerId(string id);
        Task<PayersAC> GetPayer(int id);
        void CreatePayer(Payers Payer);
        void UpdatePayer(Payers Payer);
        Task DeletePayer(PayersAC Payer);
        Task Save();
        bool PayerExists(int id);
    }
}

using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.PayeesRepository
{
    public interface IPayeesRepository
    {
        IEnumerable<PayeesAC> GetPayees();
        IEnumerable<PayeesAC> GetPayeesByExpenseId(int id);
        IEnumerable<PayeesAC> GetPayeesByPayeeId(string id);
        Task<PayeesAC> GetPayee(int id);
        void CreatePayee(Payees Payee);
        void UpdatePayee(Payees Payee);
        Task DeletePayee(PayeesAC Payee);
        Task Save();
        bool PayeeExists(int id);
    }
}

using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository.ActivitiesRepository
{
    public interface IActivitiesRepository
    {
        IEnumerable<ActivitiesAC> GetActivities();
        IEnumerable<ActivitiesAC> GetActivitiesByUserId(string id);
        Task<ActivitiesAC> GetActivity(int id);
        Activities CreateActivity(Activities Activity);
        void UpdateActivity(Activities Activity);
        Task DeleteActivity(ActivitiesAC Activity);
        Task DeleteAllActivities(string id);
        Task Save();
        bool ActivityExists(int id);
    }
}

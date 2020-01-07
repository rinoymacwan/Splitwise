using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.DataRepository;

namespace Splitwise.Repository.ActivitiesRepository
{
    public class ActivitiesRepository : IActivitiesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        private readonly IDataRepository dataRepository;
        public ActivitiesRepository(SplitwiseContext context, IMapper mapper, IDataRepository _dataRepository)
        {
            this.context = context;
            _mapper = mapper;
            dataRepository = _dataRepository; 
        }
        /*

         * */
        public bool ActivityExists(int id)
        {
            return context.Activities.Any(e => e.Id == id);
        }

        public Activities CreateActivity(Activities Activity)
        {
            var x = dataRepository.Add(Activity);
            return x;

        }

        public async Task DeleteActivity(ActivitiesAC Activity)
        {
            var x = await dataRepository.FindAsync<Activities>(Activity.Id);
            context.Activities.Remove(x);
        }
        public async Task DeleteAllActivities(string id)
        {
            dataRepository.RemoveRange(dataRepository.Where<Activities>(k => k.UserId == id));
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<ActivitiesAC> GetActivities()
        {
            return _mapper.Map<IEnumerable<ActivitiesAC>>(dataRepository.GetAll<Activities>());
        }

        public IEnumerable<ActivitiesAC> GetActivitiesByUserId(string id)
        {
            return _mapper.Map<IEnumerable<ActivitiesAC>>(dataRepository.Where<Activities>(k => k.UserId == id));
        }

        public async Task<ActivitiesAC> GetActivity(int id)
        {
            var data =  await dataRepository.FirstAsync<Activities>(k => k.Id == id);
            var data2 = _mapper.Map<ActivitiesAC>(data);
            return data2;
        }

        public async Task Save()
        {
            await dataRepository.SaveChangesAsync();
        }

        public void UpdateActivity(Activities Activity)
        {
            dataRepository.Entry(Activity);
        }
    }
}

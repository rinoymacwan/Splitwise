using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;

namespace Splitwise.Repository.ActivitiesRepository
{
    public class ActivitiesRepository : IActivitiesRepository, IDisposable
    {
        private SplitwiseContext context;
        private readonly IMapper _mapper;
        public ActivitiesRepository(SplitwiseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }
        public bool ActivityExists(int id)
        {
            return context.Activities.Any(e => e.Id == id);
        }

        public void CreateActivity(Activities Activity)
        {
            context.Activities.Add(Activity);
        }

        public async Task DeleteActivity(ActivitiesAC Activity)
        {
            var x = await context.Activities.FindAsync(Activity.Id);
            context.Activities.Remove(x);
        }
        public async Task DeleteAllActivities(string id)
        {
            context.Activities.RemoveRange(context.Activities.Where(k => k.UserId == id));
        }

        public void Dispose()
        {
            
        }

        public IEnumerable<ActivitiesAC> GetActivities()
        {
            return _mapper.Map<IEnumerable<ActivitiesAC>>(context.Activities);
        }

        public IEnumerable<ActivitiesAC> GetActivitiesByUserId(string id)
        {
            return _mapper.Map<IEnumerable<ActivitiesAC>>(context.Activities.Where(k => k.UserId == id));
        }

        public async Task<ActivitiesAC> GetActivity(int id)
        {
            return _mapper.Map<ActivitiesAC>(await context.Activities.FindAsync(id));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void UpdateActivity(Activities Activity)
        {
            context.Entry(Activity).State = EntityState.Modified;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Moq;
using Splitwise.Repository.ActivitiesRepository;
using Splitwise.Repository.DataRepository;
using Splitwise.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.Test
{
    public class Bootstrap
    {
        public Mock<IDataRepository> dataRepository;
        public ServiceProvider serviceProvider;

        public Bootstrap()
        {
            var services = new ServiceCollection();
            dataRepository = new Mock<IDataRepository>();

            services.AddScoped<IActivitiesRepository, ActivitiesRepository.ActivitiesRepository> ();
            services.AddScoped<IDataRepository>(obj => dataRepository.Object);
            services.AddScoped<IJwtFactory, JwtFactory>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}

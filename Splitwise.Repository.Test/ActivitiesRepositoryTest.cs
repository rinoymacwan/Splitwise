using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.DataRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Splitwise.Repository.Test
{
    public class ActivitiesRepositoryTest
    {
        private Mock<IDataRepository> dataRepository;
        // dependency injection for bootstrap?
        public ActivitiesRepositoryTest(Bootstrap bootstrap)
        {
            dataRepository = bootstrap.dataRepository;
        }

        [Fact]
        public void CreateActivity()
        {
            var activity = new Activities()
            {
                Id = 1,
                UserId = "aa",
                Description = "Hello",
                DateTime = new DateTime()
            };
            dataRepository.Setup(k => k.GetAll<Activities>());
            var x = dataRepository.Object.GetAll<Activities>();
            dataRepository.Verify(k => k.GetAll<Activities>(), Times.Once());
            
        }
    }
}

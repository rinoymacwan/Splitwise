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
using Microsoft.Extensions.DependencyInjection;
using Splitwise.Repository.ActivitiesRepository;
using System.Linq.Expressions;

namespace Splitwise.Repository.Test
{
    [Collection("Register Dependency")]
    public class ActivitiesRepositoryTest: IClassFixture<Bootstrap>
    {
        private Mock<IDataRepository> dataRepository;
        private IActivitiesRepository activitiesRepository;
      

        // dependency injection for bootstrap?
        public ActivitiesRepositoryTest(Bootstrap bootstrap)
        {
            dataRepository = bootstrap.dataRepository;
            
            activitiesRepository = bootstrap.serviceProvider.GetService<IActivitiesRepository>();
            // activitiesRepository = bootstrap.serviceProvider.GetService<IActivitiesRepository>();
        }

        [Fact]
        public void CreateActivity_VerifyIfItReturnsNewActivity()
        {
            var activity = new Activities()
            {
                Id = 1,
                UserId = "aa",
                Description = "Hello",
                DateTime = new DateTime()
            };

            dataRepository.Setup(k => k.Add(It.IsAny<Activities>())).Returns(activity);

            var x = activitiesRepository.CreateActivity(activity);

            dataRepository.Verify(k => k.Add(It.IsAny<Activities>()), Times.Once);
            Assert.Equal(activity.Id, x.Id);
            Assert.Equal(activity.Description, x.Description);
            Assert.Equal(activity.UserId, x.UserId);
            Assert.Equal(activity.DateTime, x.DateTime);

        }
        [Fact]
        public async Task GetActivityById_VerifyIfIdIsCorrect()
        {
            var activityId = 1;

            var activity1 = new Activities()
            {
                Id = 1,
                UserId = "aa",
                Description = "Hello",
                DateTime = new DateTime()
            };

            dataRepository.Setup(x => x.FirstAsync<Activities>(k => k.Id == activityId)).ReturnsAsync(activity1);

            

            var result = await dataRepository.Object.FirstAsync<Activities>(k => k.Id == activityId);

            // How to write lambda expression in It.IsAny?

            dataRepository.Verify(x => x.FirstAsync<Activities>(k => k.Id == activityId), Times.Once);

            Assert.Equal(result.Id, activity1.Id);

        }

        [Fact]
        public async Task GetActivityByIdRepoTest_VerifyIfIdIsCorrect()
        {
            var activityId = 4002;
            var activity1 = new Activities()
            {
                Id = 1,
                UserId = "aa",
                Description = "Hello",
                DateTime = new DateTime()
            };

            dataRepository.Setup(x => x.FirstAsync(It.IsAny<Expression<Func<Activities, bool>>>()))
                .Returns(Task.FromResult(activity1));

            var result = await activitiesRepository.GetActivity(activityId);

            //dataRepository.Verify(x => x.FindAsync<Activities>(It.IsAny<int>()), Times.Once);

            Assert.Equal(result.Id, activity1.Id);

        }
    }
}

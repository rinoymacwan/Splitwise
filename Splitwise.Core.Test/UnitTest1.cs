using Microsoft.AspNetCore.Mvc;
using Moq;
using Splitwise.Controllers.ApiControllers;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.ActivitiesRepository;
using Splitwise.Repository.GroupsRepository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Splitwise.Core.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            

            //Act
            
            //Assert
        }

        [Fact]
        public void GetActivities()
        {
            var ActivitiesRepo = new Mock<IActivitiesRepository>();
            var ActivitiesControl = new ActivitiesController(ActivitiesRepo.Object);

            Assert.IsAssignableFrom<IEnumerable<ActivitiesAC>>(ActivitiesControl.GetActivities());
            
        }

        [Fact]
        public void GetActivity_NotFound()
        {
            var ActivitiesRepo = new Mock<IActivitiesRepository>();
            var ActivitiesControl = new ActivitiesController(ActivitiesRepo.Object);
            var result = ActivitiesControl.GetActivities(0);

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void PutActivities()
        {
            var ActivitiesRepo = new Mock<IActivitiesRepository>();
            var ActivitiesControl = new ActivitiesController(ActivitiesRepo.Object);
            var x = ActivitiesControl.GetActivities();
            Assert.IsAssignableFrom<IEnumerable<ActivitiesAC>>(x);

        }


    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.ActivitiesRepository;
using System;
using System.Collections.Generic;
using Xunit;

namespace Splitwise.ActivitiesRepository.Test
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

        //[Fact]
        //public async void GetActivities()
        //{
        //    //Arrange
        //    var testObject = new Activities();
        //    var context = new Mock<SplitwiseContext>();
        //    var dbSetMock = new Mock<DbSet<Activities>>();
        //    var mapperMock = new Mock<IMapper>();

        //    context.Setup(x => x.Set<Activities>()).Returns(dbSetMock.Object);
        //    // .Setup(x => x.Add(It.IsAny<Activities>())).Returns(testObject);

        //    //Act
        //    var repository = new Splitwise.Repository.ActivitiesRepository.ActivitiesRepository(context.Object, mapperMock.Object);
        //    var y = await repository.GetActivity(3002);

        //    //Assert
        //    Assert.Equal("beacd062-6060-4108-b9f7-0c7a13823a81", y.UserId);
        //}

        //[Fact]
        //public void GetActivities()
        //{
        //    var ActivitiesRepo = new Mock<IActivitiesRepository>();
        //    var ActivitiesControl = new ActivitiesController(ActivitiesRepo.Object);

        //    Assert.IsAssignableFrom<IEnumerable<ActivitiesAC>>(ActivitiesControl.GetActivities());
            
        //}

        //[Fact]
        //public void GetActivity_NotFound()
        //{
        //    var ActivitiesRepo = new Mock<IActivitiesRepository>();
        //    var ActivitiesControl = new ActivitiesController(ActivitiesRepo.Object);
        //    var result = ActivitiesControl.GetActivities(0);

        //    Assert.IsType<NotFoundResult>(result.Result);

        //}
    }
}

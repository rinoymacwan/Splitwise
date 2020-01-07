using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.ActivitiesRepository;
using Splitwise.Repository.CategoriesRepository;
using Splitwise.Repository.DataRepository;
using Splitwise.Repository.ExpensesRepository;
using Splitwise.Repository.GroupMemberMappingsRepository;
using Splitwise.Repository.GroupsRepository;
using Splitwise.Repository.PayeesRepository;
using Splitwise.Repository.PayersRepository;
using Splitwise.Repository.SettlementsRepository;
using Splitwise.Repository.UserFriendMappingsRepository;
using Splitwise.Repository.UsersRepository;
using Splitwise.Utility;
using System;
using System.Collections.Generic;
using System.Text;
namespace Splitwise.Repository.Test
{
    public class Bootstrap
    {
        public Mock<IDataRepository> dataRepository;
        public Mock<SplitwiseContext> contextMock;
        public ServiceProvider serviceProvider;

        public Bootstrap()
        {
            var services = new ServiceCollection();
            dataRepository = new Mock<IDataRepository>();
            contextMock = new Mock<SplitwiseContext>();

            // Services

            services.AddScoped<IUsersRepository, UsersRepository.UsersRepository>();
            services.AddScoped<IUserFriendMappingsRepository, UserFriendMappingsRepository.UserFriendMappingsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository.CategoriesRepository>();
            services.AddScoped<IActivitiesRepository, ActivitiesRepository.ActivitiesRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository.GroupsRepository>();
            services.AddScoped<IGroupMemberMappingsRepository, GroupMemberMappingsRepository.GroupMemberMappingsRepository>();
            services.AddScoped<IExpensesRepository, ExpensesRepository.ExpensesRepository>();
            services.AddScoped<ISettlementsRepository, SettlementsRepository.SettlementsRepository>();
            services.AddScoped<IPayersRepository, PayersRepository.PayersRepository>();
            services.AddScoped<IPayeesRepository, PayeesRepository.PayeesRepository>();

            services.AddScoped<IJwtFactory, JwtFactory>();

            services.AddScoped<IDataRepository, DataRepository.DataRepository>();

            // AutoMapper

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Activities, ActivitiesAC>().ReverseMap();
                cfg.CreateMap<Categories, CategoriesAC>();
                cfg.CreateMap<Expenses, ExpensesAC>();
                cfg.CreateMap<GroupAndMembers, GroupAndMembersAC>();
                cfg.CreateMap<GroupMemberMappings, GroupMemberMappingsAC>();
                cfg.CreateMap<Groups, GroupsAC>();
                cfg.CreateMap<Payees, PayeesAC>();
                cfg.CreateMap<Payers, PayersAC>();
                cfg.CreateMap<Settlements, SettlementsAC>();
                cfg.CreateMap<UserFriendMappings, UserFriendMappingsAC>();
                cfg.CreateMap<Users, UsersAC>();

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<SplitwiseContext>(options => options.UseInMemoryDatabase());
            serviceProvider = services.BuildServiceProvider();
        }
    }
}

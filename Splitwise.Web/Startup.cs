using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Hubs;
using Splitwise.DomainModel.Models;
using Splitwise.Models;
using Splitwise.Repository.ActivitiesRepository;
using Splitwise.Repository.CategoriesRepository;
using Splitwise.Repository.ExpensesRepository;
using Splitwise.Repository.GroupMemberMappingsRepository;
using Splitwise.Repository.GroupsRepository;
using Splitwise.Repository.PayeesRepository;
using Splitwise.Repository.PayersRepository;
using Splitwise.Repository.SettlementsRepository;
using Splitwise.Repository.UserFriendMappingsRepository;
using Splitwise.Repository.UsersRepository;
using Splitwise.Utility;
using Splitwise.Utility.Helpers;
using System;
using System.Text;
using AutoMapper;
using Splitwise.DomainModel.ApplicationClasses;
using Splitwise.Repository.DataRepository;

namespace Splitwise.Web
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Activities, ActivitiesAC>();
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
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "App/dist";
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserFriendMappingsRepository, UserFriendMappingsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository>();
            services.AddScoped<IGroupMemberMappingsRepository, GroupMemberMappingsRepository>();
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
            services.AddScoped<ISettlementsRepository, SettlementsRepository>();
            services.AddScoped<IPayersRepository, PayersRepository>();
            services.AddScoped<IPayeesRepository, PayeesRepository>();
            services.AddDbContext<SplitwiseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SplitwiseContext")));
            services.AddCors();

            services.AddScoped<IJwtFactory, JwtFactory>();

            services.AddScoped<IDataRepository, DataRepository>();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            var builder = services.AddIdentityCore<Users>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<SplitwiseContext>().AddDefaultTokenProviders();

            services.AddIdentity<Users, IdentityRole>()
            .AddEntityFrameworkStores<SplitwiseContext>()
            .AddDefaultTokenProviders();

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            services.AddCors(o => o.AddPolicy("CorsPolicy", b =>
            {
                b
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSignalR(options =>
            {
                options.MapHub<ChatHub>("/notify");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "App/dist";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

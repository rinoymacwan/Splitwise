using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Models;

namespace Splitwise.Models
{
    public class SplitwiseContext : IdentityDbContext<Users>
    {
        public SplitwiseContext (DbContextOptions<SplitwiseContext> options)
            : base(options)
        {
        }

        public SplitwiseContext()
        {
        }

        // public DbSet<Splitwise.DomainModel.Models.Users> Users { get; set; }

        public DbSet<Splitwise.DomainModel.Models.UserFriendMappings> UserFriendMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }

        public DbSet<Splitwise.DomainModel.Models.Groups> Groups { get; set; }

        public DbSet<Splitwise.DomainModel.Models.GroupMemberMappings> GroupMemberMappings { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Settlements> Settlements { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Categories> Categories { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Activities> Activities { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Expenses> Expenses { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Payers> Payers { get; set; }

        public DbSet<Splitwise.DomainModel.Models.Payees> Payees { get; set; }
    }
}

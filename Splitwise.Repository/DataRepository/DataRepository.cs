using Microsoft.EntityFrameworkCore;
using Splitwise.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Repository.DataRepository
{
    class DataRepository: IDataRepository
    {
        SplitwiseContext context;
        //DbSet<TEntity> dbSet; 
        public DataRepository(SplitwiseContext _context)
        {
            context = _context;
        }
    }
}

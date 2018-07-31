using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using udemyCoreWithAngular.Models;

namespace udemyCoreWithAngular.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ): base(options)
        {
          
         }
         
        public DbSet<Value> Values { get; set; }
    }
}

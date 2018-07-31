
using DatingApp.Data.Helpers;
using DatingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;



namespace DatingApp.Data
{


    public class DatingContext : DbContext
    {
        private readonly IConfiguration configuration;

        //public DatingContext(IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //}

        //public DatingContext() 
        //  : base(GetOptions("Data Source=EYAL-PC;Initial Catalog=AspCoreDb;Integrated Security=True"))//: base(ConnectionManager.GetOptions("DefaultConnection"))
        //{

        //}

        //private static DbContextOptions GetOptions(string connectionString)
        //     {
        //         return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        //     }
        public DatingContext(DbContextOptions<DatingContext> option) : base(option)
        {

        }
         
        public DbSet<Value> Values { get; set; }
        public DbSet<User> User { get; set; }
    }


}

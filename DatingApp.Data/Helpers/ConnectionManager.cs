using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Data.Helpers
{
    public static class ConnectionManager
    {
        public static IConfiguration configuration { get; set; }


        public static DbContextOptions GetOptions(string connectionString)
        {             
            return SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), configuration.GetConnectionString(connectionString))
                .Options;
        }

    }
}

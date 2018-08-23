using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Data;
using DatingApp.Models;
 
using System;
using System.Collections.Generic;
using System.Linq;
 

namespace DatingApp.Common.Repositories
{
    public class AuthRepository : GenericRepo<User> , IAuthRepository
    {
        public AuthRepository(DatingContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUser(Func<User, bool> func)
        {
            return Get(func);
        }

        public int SaveUser(User user)
        {
            return Add(user);
        }
    }
}

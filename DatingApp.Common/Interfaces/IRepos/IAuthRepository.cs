using DatingApp.Models;
using System;
using System.Collections.Generic;

namespace DatingApp.Common.Interfaces.IRepos
{
    public interface IAuthRepository
    {
        IEnumerable<User> GetUser(Func<User, bool> func);
        int SaveUser(User user);
    }
}
using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Common.Interfaces.IServices
{
    public interface IAuthService
    {
        bool RegisterUser(User user , string password);
        User Login(string userName, string password);
        bool UserExist(string username);
    }
}

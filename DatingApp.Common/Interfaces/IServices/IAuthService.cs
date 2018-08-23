using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Common.Interfaces.IServices
{
    public interface IAuthService
    {
        bool RegisterUser(string userName, string password);
        bool Login(string userName, string password);
        User GetUser(string username);
        User CheckIfUserValid(string userName, string password);


    }
}

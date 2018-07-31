using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Models;
using System.Collections.Generic;

namespace DatingApp.Common.Services
{
    public class ValueServices : IService
    {

        private readonly IValuesRepos _repos;

        public ValueServices(IValuesRepos repos)
        {
            _repos = repos;
           

        }
        
        IEnumerable<Value> IService.GetAllValues()
        {
            return _repos.GetAllValues();
        }
    }
}

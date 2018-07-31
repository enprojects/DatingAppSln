using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Data;
using DatingApp.Models;
using System.Collections.Generic;

namespace DatingApp.Common.Repositories
{


    public class ValuesRepository : GenericRepo<Value> , IValuesRepos
    {
         
        public ValuesRepository(DatingContext context ) : base(context)
        { 

        }

        public IEnumerable<Value> GetAllValues()
        {
            return this.Get();
        }
    }
  
}

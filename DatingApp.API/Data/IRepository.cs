using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using udemyCoreWithAngular.Models;

namespace udemyCoreWithAngular.Data
{
    public interface IRepository
    {
        Task<List<Value>>GetValues();
    }
}

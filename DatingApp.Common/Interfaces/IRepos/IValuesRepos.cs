using DatingApp.Data;
using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Common.Interfaces.IRepos
{
    public interface  IValuesRepos
    {
        IEnumerable<Value> GetAllValues();

    }
}

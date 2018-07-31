using System;
using System.Collections.Generic;

namespace DatingApp.Data

{ 
    public interface IGenericRepo<T> where T : class  

    {
        IEnumerable<T> Get(Func<T, bool> func = null);
        int Add(T obj);
        int Update();
    }
}
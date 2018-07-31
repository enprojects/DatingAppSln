using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Data
{
    public interface IRepository <T> where T: class
    {
        IGenericRepo<T> Repository { get; }
    }
}

using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Common.Interfaces.IServices
{
    public interface IService
    {
       IEnumerable<Value> GetAllValues();
    }
}

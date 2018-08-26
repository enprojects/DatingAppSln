using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Models;
using System;
using System.Collections.Generic;

namespace DatingApp.Common.Services
{


    public class ValueServices : IService
    {

        private readonly IValuesRepos _repos;
      
        //Ex1 get specific implemintation for IA --
        private readonly Func<string, IServMultipaleConcreate> _serviceAccessorTest;
        private readonly IServMultipaleConcreateGeneric<ConcreateC> _serviceAccessorTest2;


        public ValueServices(IValuesRepos repos, Func<string, IServMultipaleConcreate> serviceAccessorTest, IServMultipaleConcreateGeneric<ConcreateC> serviceAccessorTest2)
        {
            _repos = repos;
            //_serviceAccessorTest = serviceAccessorTest;
            //_serviceAccessorTest2 = serviceAccessorTest2;

            //Ex1 get specific implemintation for IServMultipaleConcreate
            //_serviceAccessorTest("A").DoSomthing();
            //_serviceAccessorTest2.DoSomthing();

            }
        
        IEnumerable<Value> IService.GetAllValues()
        {
            return _repos.GetAllValues();
        }
    }
}

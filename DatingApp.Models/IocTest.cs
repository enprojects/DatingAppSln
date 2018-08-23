using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Models
{
    public interface IServMultipaleConcreate
    {
        void DoSomthing();
    }

    public interface IServMultipaleConcreateGeneric<T>
    {
        void DoSomthing();
    }

    public class ConcreateA : IServMultipaleConcreate
    {
        public void DoSomthing()
        {

        }
    }


    public class ConcreateB : IServMultipaleConcreate
    {
        public void DoSomthing()
        {

        }
    }

    public class ConcreateC : IServMultipaleConcreateGeneric<ConcreateC>
    {
        public void DoSomthing()
        {

        }
    }


    public class ConcreateD : IServMultipaleConcreateGeneric<ConcreateD>
    {
        public void DoSomthing()
        {

        }
    }

}

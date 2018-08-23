using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Common.Interfaces;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Common.Repositories;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 

namespace udemyCoreWithAngular.Controllers
{
    



    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IService _serv;
        public ValuesController(IService serv)
        {
            _serv = serv;
        }

 
        [AllowAnonymous]
        public IEnumerable<Value> FetchValues()
        { 
            return _serv.GetAllValues();
        }


      //  GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return "valuedddd";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

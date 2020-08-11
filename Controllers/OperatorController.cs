using DemoAPI.DAO;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{

    public class OperatorController : ApiController
    {
        // GET: api/Operator
        List<Operator> ListOperator = new OperatorDAO().OperatorList();

        public IEnumerable<Operator> Get()
        {
            return ListOperator;
        }

        // GET: api/Operator/5
        public Operator Get(int id)
        {
            return ListOperator[id];
        }
      

        public Operator GetUsingName(string name)
        {
            return ListOperator.FirstOrDefault(x => x.Name == name);
        }
        // POST: api/Operator
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Operator/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Operator/5
        public void Delete(int id)
        {
        }
    }
}

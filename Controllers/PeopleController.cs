
using DataModel.DAO;
using DataModel.EF;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{
     /// <summary>
     /// this where you get person information
     /// </summary>
    public class PeopleController : ApiController
    {

        // GET: api/People
        PersonDao dao = new PersonDao();
        
        public Person Get()
        {

            return dao.getInfo();
        }

        // GET: api/People/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/People
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/People/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/People/5
        public void Delete(int id)
        {

        }
        /// <summary>
        /// check for login for this person 
        /// </summary>
        /// <param name="name">this is login id</param>
        /// <param name="password">this is password</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("api/People/Login")]
        
        public Person Login(LoginModel model)
        {
            return new PersonDao().login(model.UserName, model.Password);
        }
    }
}

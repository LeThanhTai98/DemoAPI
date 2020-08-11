using DataModel.DAO;
using DataModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{
    public class educationController : ApiController
    {
        // GET: api/education
        public IHttpActionResult Get()
        {
            return Ok("hello");
        }

        // GET: api/education/5
        public IHttpActionResult Get(int id)
        {
            if (id > 0)
                return Ok(new educationDao().getDetail(id));
            else return BadRequest("id khong hợp lệ");
        }

        // POST: api/education
        public IHttpActionResult Post(education model)  
        {
            bool result;
            using (var dao = new educationDao())
            {
                result = dao.CreateDetail(model);
            }
            if (result) return Ok(model);
            else return BadRequest("model không phù hợp");
        }

        // PUT: api/education/5
        public IHttpActionResult Put(education model)
        {
            bool result;
            using (var dao = new educationDao())
            {
                result = dao.Update(model);
            }
            if (result) return Ok(model);
            else return BadRequest("model không phù hợp");
        }

        // DELETE: api/education/5
        public IHttpActionResult Delete(int id)
        {
            if (id < 0) return BadRequest(" id không hợp lệ");

            bool result;
            using (var dao = new educationDao())
            {

            }
            return Ok();
        }
    }
}

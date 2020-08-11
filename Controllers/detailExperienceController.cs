using DataModel.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAPI.Controllers
{   /// <summary>
    /// get detail experience of person 
    /// </summary>
    /// 
   
    public class detailExperienceController : ApiController
    {

        DataModel.DAO.detailExperienceDao dao = new DataModel.DAO.detailExperienceDao();

        // GET: api/detailExperience
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/detailExperience/FromPersonID/5
        /// <summary>
        /// using id of person all of its detail experience
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Route("api/detailExperience/FromPersonID/{id}")]
        public IEnumerable<detailExperience> GetFromPersonID(int id)
        {
            return dao.getAllDetail(id);
        }

        // GET: api/detailExperience/5
        /// <summary>
        /// get 1 detail experience using its id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
       
        [HttpGet]
        [Route("api/detailExperience/{id}")]
        public detailExperience Get(int id)
        {
            return dao.getDetail(id);
        }

        // POST: api/detailExperience
        [HttpPost]
        [Route("api/detailExperience/{model}")]
        public detailExperience Post(detailExperience model)
        {
            if (dao.CreateDetail(model)) return model;
            return null;
        }

        
        // PUT: api/detailExperience/{model}
        [HttpPut]
        [Route("api/detailExperience/{model}")]
        public detailExperience Put(detailExperience model)
        {
            if (dao.update(model)) return model;
            else return null;
        }

        // DELETE: api/detailExperience/5
        [HttpDelete]
        [Route("api/detailExperience/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (id < 0) return BadRequest("id không hợp lệ");
            if (!dao.Delete(id)) return BadRequest("không xóa dc ");
            return Ok(true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using DataModel.EF;

namespace DemoAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DataModel.EF;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<experience>("experiences");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */


    public class experiencesController : ODataController
    {
        private Blog db = new Blog();

        // GET: odata/experiences
        [EnableQuery]
        public IQueryable<experience> Getexperiences()
        {
            return db.experiences;
        }

        // GET: odata/experiences(5)
        [EnableQuery]
        public SingleResult<experience> Getexperience([FromODataUri] int key)
        {
            return SingleResult.Create(db.experiences.Where(experience => experience.id == key));
        }

        // PUT: odata/experiences(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<experience> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            experience experience = db.experiences.Find(key);
            if (experience == null)
            {
                return NotFound();
            }

            patch.Put(experience);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!experienceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(experience);
        }

        // POST: odata/experiences
        public IHttpActionResult Post(experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.experiences.Add(experience);
            db.SaveChanges();

            return Created(experience);
        }

        // PATCH: odata/experiences(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<experience> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            experience experience = db.experiences.Find(key);
            if (experience == null)
            {
                return NotFound();
            }

            patch.Patch(experience);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!experienceExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(experience);
        }

        // DELETE: odata/experiences(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            experience experience = db.experiences.Find(key);
            if (experience == null)
            {
                return NotFound();
            }

            db.experiences.Remove(experience);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool experienceExists(int key)
        {
            return db.experiences.Count(e => e.id == key) > 0;
        }
    }
}

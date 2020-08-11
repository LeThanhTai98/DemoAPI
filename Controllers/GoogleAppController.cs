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
    public class GoogleAppController : ApiController
    {
        // GET: api/GoogleApp
        public IEnumerable<GoogleApp> Get()
        {
            var ListApp = new GoogleAppStore().getGoogleApp();
            return ListApp;
        }

        // GET: api/GoogleApp?first=a&last=b
        public IEnumerable<GoogleApp> Get(int first, int last)
        {
            var ListApp = new GoogleAppStore().getRangeGoogleApp(first, last);
            return ListApp;
        }



        // POST: api/GoogleApp
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/GoogleApp/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/GoogleApp/5
        //public void Delete(int id)
        //{
        //}

        [HttpGet]
        [Route("api/GoogleApp/GetPaging")]
        public PagingResult GetPaging(string search = "ca ro" , string cursor = null)
        {
            int first = 5;
            var ListApp = new GoogleAppStore().getGoogleApp(search);
            PagingResult result = new PagingResult();
            result.limit = 5;
            result.total = ListApp.Count;
            if (cursor == null)
            {
                
               
               
                var temp = (from x in ListApp
                            where first <= x.id
                            orderby x.id
                            select x).Take(5).ToList();
                result.ListApp = temp;

                result.previous_page = "previous_" + result.ListApp[0].id.ToString();
                result.next_page = "next_" + result.ListApp[result.ListApp.Count - 1].id.ToString();

                return result;
            }
            else
            {
                try
                {
                    string[] request = cursor.Split('_');
                    int page = int.Parse(request[1]);

                    if (request[0] == "next")
                    {
                        var temp = (from x in ListApp
                                    where page <= x.id
                                    orderby x.id
                                    select x).Take(5).ToList();
                        result.ListApp = temp;

                        result.previous_page = "previous_" + result.ListApp[0].id.ToString();
                        result.next_page = "next_" + result.ListApp[result.ListApp.Count - 1].id.ToString();

                        return result;
                    }
                    else
                    {
                        var temp = (from x in ListApp
                                    where page > x.id
                                    orderby x.id
                                    select x).Take(5).ToList();
                        result.ListApp = temp;

                        result.previous_page = "previous_" + result.ListApp[0].id.ToString();
                        result.next_page = "next_" + result.ListApp[result.ListApp.Count - 1].id.ToString();
                        result.SearchString = search;

                        return result;
                    }
                }
                catch
                {
                    var temp = (from x in ListApp
                                where first <= x.id
                                orderby x.id
                                select x).Take(5).ToList();
                    result.ListApp = temp;

                    result.previous_page = "previous_" + result.ListApp[0].id.ToString();
                    result.next_page = "next_" + result.ListApp[result.ListApp.Count - 1].id.ToString();

                    return result;
                }
            }
        }

    }
}

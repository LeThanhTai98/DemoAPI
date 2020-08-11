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
    public class DetailOperatorController : ApiController
    {
        public DetailOperator Get(string name)
        {
            return new DetailOperatopDAO().GetDetail(name);
        }

    }
}

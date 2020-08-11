using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    /// <summary>
    /// represent for 1 login action
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// this is login od
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// this login password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// this just for fun 
        /// </summary>
        public bool remember { get; set; }
    }
}
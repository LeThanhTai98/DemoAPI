using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    public class Operator
    {
        // name of operator
        public string Name { get; set; }
        // link face icon of operator 
        public string Icon { get; set; }
        // rarity of operator 
        public int stars { get; set; }
    }
}
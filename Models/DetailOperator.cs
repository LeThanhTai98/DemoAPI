using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    public class DetailOperator
    {
        public string BigPicture { get; set; }
        public string Class { get; set; }

        public string Faction { get; set; }

        public string Position { get; set; }

        public string AttackType { get; set; }

        public List<List<int>> Range { get; set; }
    }
}
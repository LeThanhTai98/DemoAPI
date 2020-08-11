using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.Models
{
    public class PagingResult
    {
        public List<GoogleApp> ListApp { get; set; }
        public string previous_page { get; set; }
        public string next_page { get; set; }
        public string SearchString { get; set; }
        public int total { get; set; }
        public int limit { get; set; }
    }
}
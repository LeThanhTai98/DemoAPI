using DemoAPI.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DemoAPI.DAO
{
    public class OperatorDAO
    {
        public string url { get; set; } = "https://gamepress.gg/arknights/database/interactive-operator-list#tags=null##stats";
        public string storeAddress { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OP_JSON_DATA.txt");
        public List<Operator> OperatorList()
        {
            var operatorList = new List<Operator>();
            if (!File.Exists(storeAddress ))
            {

                HtmlWeb htmlweb = new HtmlWeb()
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.UTF8
                };

                HtmlDocument document = htmlweb.Load(url);
                var listItem = document.DocumentNode.SelectNodes("//tr[@class = 'operators-row']").ToList();


                foreach (HtmlNode item in listItem)
                {
                    var Op = new Operator();
                    Op.Name = item.Attributes["data-name"].Value;

                    Op.Icon = "https://gamepress.gg/" + item.QuerySelector("div.operator-icon > a > img").Attributes["src"].Value;

                    Op.stars = item.QuerySelector("div.rarity-div").Descendants().Count();

                    operatorList.Add(Op);
                }

                using (StreamWriter file = File.CreateText(storeAddress))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, operatorList);
                }

            }
            else
            {
                var jsontext = File.ReadAllText(storeAddress);
                operatorList = JsonConvert.DeserializeObject<List<Operator>>(jsontext);
            }
            return operatorList;
        }
    }
}
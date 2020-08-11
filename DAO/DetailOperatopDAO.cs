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
    public class DetailOperatopDAO
    {

        internal string url = @"https://gamepress.gg/arknights/operator/";
        public string storeAddress { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public DetailOperator GetDetail(string Name)
        {
            HtmlWeb htmlweb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            if (!File.Exists(storeAddress + "DETAIL_OP_JSON_DATA_" + Name.ToLower() + ".txt"))
            {
                try
                {
                    HtmlDocument document = htmlweb.Load(url + Name);
                    var listItem = document.DocumentNode.SelectNodes("//div[@class = 'operator-top-cell']");

                    var operatorData = new DetailOperator();
                    foreach (HtmlNode item in listItem)
                    {
                        var operatorClass = item.QuerySelector("div.profession-cell > img").Attributes["src"].Value;

                        var temp = document.DocumentNode.SelectSingleNode("//div[@id = 'image-tab-3']");

                        string img = temp.QuerySelector("a > img").Attributes["src"].Value;

                        var faction = document.DocumentNode.SelectSingleNode("//div[@class = 'faction-cell']").QuerySelector("img").Attributes["src"].Value;

                        var postionAndAttactType = document.DocumentNode.SelectNodes("//div[@class = 'text-content-cell']");

                        for (int i = 0; i < 2; i++)
                        {
                            var tempPostionAndAttackTypeInformation = postionAndAttactType[i].QuerySelector("a").InnerHtml;
                            if (i == 0) operatorData.Position = tempPostionAndAttackTypeInformation;
                            else operatorData.AttackType = tempPostionAndAttackTypeInformation;
                        }
                        var range = temp.QuerySelectorAll("div.range-outer > div.operator-range > div.range-box > div.range-cell");
                        List<List<int>> listRange = new List<List<int>>();

                        foreach (HtmlNode rangeItem in range)
                        {
                            var tempNode = rangeItem.Descendants();
                            var tempList = new List<int>();
                            foreach (var nodeItem in tempNode)
                            {
                                if (nodeItem.HasClass("empty-box")) { tempList.Add(0); }
                                else if (nodeItem.HasClass("fill-box")) { tempList.Add(1); }
                                else if (nodeItem.HasClass("null-box")) { tempList.Add(2); }

                            }
                            listRange.Add(tempList);
                        }


                        operatorData.Class = "https://gamepress.gg/" + operatorClass;
                        operatorData.BigPicture = "https://gamepress.gg/" + img;
                        operatorData.Faction = "https://gamepress.gg/" + faction;
                        operatorData.Range = listRange;
                    }
                    using (StreamWriter file = File.CreateText(storeAddress + "DETAIL_OP_JSON_DATA_" + Name.ToLower() + ".txt"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, operatorData);
                    }
                    return operatorData;
                }
                catch (Exception ex)
                {
                    return null;
                }
            
            }
            else
            {
                var jsontext = File.ReadAllText(storeAddress + "DETAIL_OP_JSON_DATA_" + Name.ToLower() + ".txt");
                var operatorData = JsonConvert.DeserializeObject<DetailOperator>(jsontext);
                return operatorData;
            }
        }
    }
}
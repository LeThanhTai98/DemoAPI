using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using DemoAPI.Models;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DemoAPI.DAO
{
    public class GoogleAppStore
    {
        string folderAddress = @"C:\project\DemoAPI\";
        public List<GoogleApp> getGoogleApp(string search = "ca ro")
        {
       
            search = Regex.Replace(search, "[ ]", "%20");
            search = search + "&c=apps&hl=en";
            HtmlDocument doc = new HtmlDocument();
        begin:
            if (!File.Exists(folderAddress + search + ".txt"))
            {



                HtmlWeb htmlweb = new HtmlWeb()
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.UTF8
                };
                //Do not start the chrome window
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");

                //Close the Chrome Driver console
                ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;

                ChromeDriver driver = new ChromeDriver(driverService, options);
                //ChromeDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://play.google.com/store/search?q=" + search);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                int height = 1;
                int lastHeight = 0;
                while (lastHeight != height)
                {
                    lastHeight = height;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    Thread.Sleep(3000);
                    height = (int)(long)js.ExecuteScript("return document.body.scrollHeight;");
                }

                string title = driver.Title;//Page title
                string html = driver.PageSource;//Page Html

                using (StreamWriter file = File.CreateText(folderAddress + search + ".txt"))
                {
                    file.Write(html);
                }
                doc.LoadHtml(html);

                Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                foreach (var chromeDriverProcess in chromeDriverProcesses)
                {
                    chromeDriverProcess.Kill();
                }
            }
            else
            {
                DateTime createTime = File.GetCreationTime(folderAddress + search + ".txt");
                DateTime currentTime = DateTime.Now;
                var result = currentTime.Subtract(createTime).TotalMinutes;
                if (result > 30) { File.Delete(folderAddress + search + ".txt"); goto begin; }

                var html = File.ReadAllText(folderAddress + search + ".txt");

                doc.LoadHtml(html);
            }
            var listItem = doc.DocumentNode.SelectNodes("//div[@class = 'ImZGtf mpg5gc']").ToList();
            var listItem2 = doc.DocumentNode.SelectNodes("//div[@class = 'uMConb  V2Vq5e POHYmb-eyJpod YEDFMc-eyJpod y1APZe-eyJpod drrice']").ToList();
            Console.WriteLine(listItem.Count + listItem2.Count);
            listItem.AddRange(listItem2);
            var AppList = new List<GoogleApp>();
            int id = 0;
            foreach (var item in listItem)
            {
                var tempApp = new GoogleApp();
                tempApp.id = id;
                tempApp.AppName = item.QuerySelector("div.WsMG1c.nnK0zc").Attributes["title"].Value;
                tempApp.Link = item.QuerySelector("div.wXUyZd > a").Attributes["href"].Value;
                try
                {
                    tempApp.Img = item.QuerySelector("div.N9c7d.eJxoSc > span > img").Attributes["src"].Value;
                    tempApp.Rating = item.QuerySelector("div.pf5lIe > div").Attributes["aria-label"].Value;
                    var temp = item.QuerySelector("div.Z2nl8b ").Descendants().ToList().Count;
                }
                catch
                {

                }
                id++;
                AppList.Add(tempApp);
            }
            return AppList;
        }

        internal List<GoogleApp> getRangeGoogleApp(int first, int last)
        {
            var listApp = getGoogleApp();
            return listApp.GetRange(first, last - first < listApp.Count - first ? last - first : listApp.Count - first);
        }
    }
}
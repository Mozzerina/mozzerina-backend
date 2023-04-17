using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Models;
using System.Collections.Generic;

namespace Mozzerina.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MozzerinaContext _dataContext;
        public DbInitializer(MozzerinaContext dataContext, IHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
        }
        public static Dictionary<string, string> GetHrefSpanDictionary()
        {
            string path = Path.Combine(@"C:\Users\ddazk\source\repos\Mozzerina\Mozzerina\ParseInfo", "menu.txt");

            string html = File.ReadAllText(path);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var links = doc.DocumentNode.SelectNodes("//a[@class='block linkOverlay__primary tile___1wb3i']");

            var result = new Dictionary<string, string>();

            foreach (var link in links)
            {
                string hrefValue = link.Attributes["href"].Value;
                string spanText = link.SelectSingleNode(".//span").InnerText.Trim();

                result[hrefValue] = spanText;
            }

            return result;
        }
        public static List<string> GetImagesDictionary()
        {
            string path = Path.Combine(@"C:\Users\ddazk\source\repos\Mozzerina\Mozzerina\ParseInfo", "imgMenu.txt");

            string html = File.ReadAllText(path);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var images = doc.DocumentNode.SelectNodes(".//img[@class='imageBlock___30QOb sb-imageFade__imagePositioning sb-imageFade__show']");

            var result = new List<string>();

            foreach (var image in images)
            {
                string srcValue = image.Attributes["src"].Value;
                result.Add(srcValue);
            }

            return result;
        }
        public void Initialize()
        {
            if (_dataContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dataContext.Database.Migrate();
            }
            if (!_dataContext.MenuTypes.Any())
            {
                Dictionary<string, string> info = GetHrefSpanDictionary();
                List<string> img = GetImagesDictionary();

                foreach (var pair in info.Zip(img, (d, l) => (Key: d.Key, Value: d.Value, Item: l)))
                {
                    _dataContext.MenuTypes.Add(
                        new MenuType
                        {
                            Name = pair.Value,
                            Href = pair.Key,
                            ImagePreview = pair.Item
                        });
                }
            }
            _dataContext.SaveChanges();
        }
    }
}

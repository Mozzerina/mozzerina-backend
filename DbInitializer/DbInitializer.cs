using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;
using Mozzerina.Models;

namespace Mozzerina.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MozzerinaContext _dataContext;
        private readonly IHostEnvironment _hostEnvironment;
        public DbInitializer(MozzerinaContext dataContext, IHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }
        public Dictionary<string, string> GetHrefSpanDictionary()
        {
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "ParseInfo/menu.txt");

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
        public List<string> GetImagesDictionary()
        {
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "ParseInfo/imgMenu.txt");

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
        public Dictionary<string, List<Info>> ProductTypesDict()
        {
            string path = Path.Combine(_hostEnvironment.ContentRootPath, "ParseInfo/Products.txt");
            var html = File.ReadAllText(path);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var dictionary = new Dictionary<string, List<Info>>();

            var sections = doc.DocumentNode.SelectNodes("//section[@class='pb4 lg-pb6 ']");
            foreach (var section in sections)
            {
                var heading = section.SelectSingleNode(".//h2[@class='sb-heading text-md pb3 text-bold']");
                if (heading == null)
                {
                    continue;
                }

                string currentHeading = heading.InnerText;
                dictionary[currentHeading] = new List<Info>();

                var spans = section.SelectNodes(".//span[@class='hiddenVisually']");
                var images = section.SelectNodes(".//img[@class='sb-imageFade__imagePositioning sb-imageFade__show']");

                if (spans == null && images == null)
                {
                    continue;
                }

                if (spans != null && images != null && spans.Count != images.Count)
                {
                    continue;
                }

                for (int i = 0; i < spans.Count; i++)
                {
                    var hrefSpan = new Info();

                    if (spans != null)
                    {
                        hrefSpan.SpanText = spans[i].InnerText.Trim();
                    }
                    if (images != null)
                    {
                        hrefSpan.Href = images[i].Attributes["src"].Value;
                    }

                    dictionary[currentHeading].Add(hrefSpan);
                }
            }

            return dictionary;
        }

        public void Initialize()
        {
            
            Dictionary<string, string> info = GetHrefSpanDictionary();
            List<string> img = GetImagesDictionary();
            Dictionary<string, List<Info>> types = ProductTypesDict();

            if (_dataContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dataContext.Database.Migrate();
            }
            if (!_dataContext.MenuTypes.Any())
            {

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
            if (!_dataContext.ProductTypes.Any())
            {
                int id = 0;
                int typeId = 1;
                foreach (KeyValuePair<string, List<Info>> kvp in types)
                {
                    string key = kvp.Key;

                    foreach (Info i in kvp.Value)
                    {
                        _dataContext.SubProducts.Add(
                            new SubProduct
                            {
                                Name = i.SpanText,
                                Image = i.Href,
                                ProductTypeId = typeId
                            });
                        if (img.Contains(i.Href))
                        {
                            id++;
                        }
                    }
                    _dataContext.ProductTypes.Add(
                        new ProductType
                        {
                            Name = key,
                            IdMenuType = id
                        });
                    typeId++;
                }
            }
            _dataContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace CLMonitor
{
    class Program
    {
        public static Dictionary<string, string> urls;
        public static string urlFile;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Count() > 0)
            {
                urlFile = args[0];
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Select URL list file...";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    urlFile = dlg.FileName;
                }
                else { return; }
            }

            ReadURLs(urlFile);
            if (args.Count() != 1)
            {
                if ((args.Count() == 0 || args[1].ToUpper() == "UI"))
                {
                    //Called for the UI
                    var ui = new ui();
                    ui.Text = "CLMonitor - " + urlFile;
                    ui.ReadFromFile(urlFile + "_Data.CSV");
                    //ui.Listings = System.IO.File.ReadAllLines(
                    //    args[0] + "_Data.CSV")
                    //    .Select(r => r.Split('|'))
                    //    .Select(c => new ui.Listing()
                    //        {
                    //           QueryDate = DateTime.Parse(c[0]),
                    //           Uri = new Uri(c[1]),
                    //           Title = c[2],
                    //           PostDate = DateTime.Parse(c[3]),
                    //           UpdateDate = DateTime.Parse(c[4]),
                    //           Category = c[5],
                    //           Price = decimal.Parse(c[6], System.Globalization.NumberStyles.Any),
                    //           Location = c[7],
                    //           Description = c[8],
                    //           Images = c[9].Split(',').Select(i => new Uri(i))
                    //        }
                    //    ).ToList();
                    ui.LoadGroups();
                    ui.ShowDialog();
                }
                if ((args.Count() > 0 && Uri.IsWellFormedUriString(args[1], UriKind.Absolute)))
                {
                    //URL specified, add it to the list and query it...
                    System.IO.File.AppendAllLines(Program.urlFile, new string[] { args[1] });
                    Program.ReadURLs(Program.urlFile);
                    Program.QuerySingle(new Uri(args[1]));
                }
            }
            else
            {
                QueryAll();
            }

        }

        public static void ReadURLs(string filename)
        {
            urls = System.IO.File.ReadAllLines(filename).ToDictionary(r => r.Split(',')[0], r => r.Split(',')[1]);
        }

        public static void QuerySingle(Uri url)
        {
            List<Listing> Listings = new List<Listing>();
            Listings.Add(GetListing(url));
            Parallel.ForEach(Listings, listing =>
            {
                listing.SaveImages(urlFile + "_Images\\" + listing.Uri.Segments.Last() + "\\");
            });
            System.IO.File.AppendAllLines(urlFile + "_Data.CSV", Listings.Select(l => l.ToCSVLine()));

        }

        public static void QueryAll()
        {
            try
            {
                List<Listing> Listings = new List<Listing>();
                Parallel.ForEach(urls, url =>
                    {
                        Listings.Add(GetListing(new Uri(url.Key)));
                    });
                Parallel.ForEach(Listings.Where(l => l.Title != "[NO TITLE!]"), listing =>
                {
                    listing.SaveImages(urlFile + "_Images\\" + listing.Uri.Segments.Last() + "\\");
                });
                System.IO.File.AppendAllLines(urlFile + "_Data.CSV", Listings.Select(l => l.ToCSVLine()));
            }
            catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
        }

        public static HtmlAgilityPack.HtmlDocument GetPage(Uri uri)
        {
            HtmlAgilityPack.HtmlDocument rDoc = new HtmlAgilityPack.HtmlDocument();
            try
            {
                using (WebClient client = new WebClient())
                {
                    rDoc.LoadHtml(client.DownloadString(uri));
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return rDoc;
        }

        public static Listing GetListing(Uri uri)
        {
            try
            {
                return new Listing(uri, GetPage(uri));
            }
            catch (Exception)
            { }
            return null;
        }

        public class Listing
        {
            public Listing(Uri uri, HtmlAgilityPack.HtmlDocument doc)
            {
                _doc = doc;
                Uri = uri;
            }

            HtmlAgilityPack.HtmlDocument _doc;
            public readonly Uri Uri;

            public bool SaveImages(string path)
            {
                if (Images != null)
                    Parallel.ForEach(Images, image =>
                    {
                        try
                        {
                            using (WebClient client = new WebClient())
                            {
                                System.IO.Directory.CreateDirectory(path);
                                client.DownloadFile(image, path + image.Segments.Last());
                            }
                        }
                        catch (Exception e)
                        { Console.WriteLine(e.Message + e.StackTrace); }
                    });
                return true;
            }

            public string ToCSVLine(string delimiter = "|")
            {
                return string.Join("|", new[]
                    {
                        DateTime.Now.ToString("o"),
                        Uri.ToString(),
                        Title,
                        PostDate.ToString("o"),
                        UpdateDate.ToString("o"),
                        Category,
                        Price.ToString("c"),
                        Location,
                        Description.Replace("|", "/").Replace("\n", "\\n"),
                        string.Join(",", Images.Select(i => i.ToString()))
                    }
                );
            }

            public string Contact
            {
                get
                {
                    return "";
                }
            }

            public IEnumerable<Uri> Images
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectNodes("//div[@id='thumbs']/a") != null)
                            return _doc.DocumentNode.SelectNodes("//div[@id='thumbs']/a").Select(l => new Uri(l.GetAttributeValue("href", "")));

                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
                    return new List<Uri>();
                }
            }

            public DateTime PostDate
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("//*[@id='display-date']/time") != null)
                            return DateTime.Parse(_doc.DocumentNode.SelectSingleNode("//*[@id='display-date']/time").GetAttributeValue("datetime", "2000-01-01T00:00:00-0000").Trim());
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }

                    return new DateTime();
                }
            }

            public DateTime UpdateDate
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("html/body/section/section/section/div[2]/p[2]/time") != null)
                            return DateTime.Parse(_doc.DocumentNode.SelectSingleNode("html/body/section/section/section/div[2]/p[2]/time").GetAttributeValue("datetime", "2000-01-01T00:00:00-0000").Trim());
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }

                    return DateTime.Now;
                }
            }

            public string Title
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("//*[@id='titletextonly']") != null)
                            return _doc.DocumentNode.SelectSingleNode("//*[@id='titletextonly']").InnerText.Trim();
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
                    return "[NO TITLE!]";
                }
            }

            public string Category
            {
                get
                {
                    try
                    {
                        return _doc.DocumentNode.SelectSingleNode("//li[@class='crumb category']").InnerText.Trim();
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
                    return "";
                }
            }

            public decimal Price
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("//span[@class='postingtitletext']/span[@class='price']") != null)
                            return decimal.Parse(_doc.DocumentNode.SelectSingleNode("//span[@class='postingtitletext']/span[@class='price']").InnerText.Trim(), System.Globalization.NumberStyles.Currency);
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }

                    return -1;
                }
            }

            public string Location
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("//span[@class='postingtitletext']/small") != null)
                            return _doc.DocumentNode.SelectSingleNode("//span[@class='postingtitletext']/small").InnerText.Trim();
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
                    return "";
                }
            }

            public string Description
            {
                get
                {
                    try
                    {
                        if (_doc.DocumentNode.SelectSingleNode("//div[@class='removed']") != null)
                            return _doc.DocumentNode.SelectSingleNode("//div[@class='removed']").InnerText.Trim();
                        if (_doc.DocumentNode.SelectSingleNode(".//*[@id='postingbody']") != null)
                            return _doc.DocumentNode.SelectSingleNode(".//*[@id='postingbody']").InnerText.Trim();
                    }
                    catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
                    return "";
                }
            }
        }
    }
}

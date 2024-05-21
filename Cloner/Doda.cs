using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
namespace WebDoda
{
    class Helpers
    {
        public static Regex IMG_RGX = new Regex("<img.*?src=[\"|'](.*?)[\"|']");
        public static Regex RES_RGX = new Regex("<link.*?href=[\"|'](.*?)[\"|']");
        public static Regex JS_RGX = new Regex("<script.*?src=[\"|'](.*?)[\"|']");
        public static Regex IMT_RGX = new Regex("@import.*?[\"|\'](.*?)[\"|\']");
    }
    public enum ResourceType
    {
        LocalResource = 0,
        OnlineResource = 1
    }
    class Doda
    {
        private Uri target { get; set; }
        private string Scheme { get; set; }
        private string host { get; set; }
        private string path { get; set; }
        public string Source { get; set; }
        public ResourceType rType { get; set; }
        public Doda(Uri Target)
        {
            this.target = Target;
            this.Scheme = this.target.Scheme;
            this.host = this.target.Host;
            this.path = Path.GetDirectoryName(this.target.LocalPath);
        }
        public bool DetectLocalResource(ref string link)
        {
            bool flag = true;
            if (link.StartsWith($"{this.Scheme}://{this.host}"))
            {
                rType = ResourceType.LocalResource;
            }
            else if (link.StartsWith("../"))
            {
                var maindirectory = Path.GetDirectoryName(this.target.LocalPath);
                for (int i = 0; i < Regex.Matches(link, @"\.\.\/").Count; i++)
                {
                    maindirectory = Path.GetDirectoryName(maindirectory);
                }
                rType = ResourceType.LocalResource;
                link = $"{this.Scheme}://{this.host}{maindirectory.Replace("\\", "/")}/{link.Replace("../", "")}";
            }
            else if (link.StartsWith("/"))
            {
                rType = ResourceType.LocalResource;
                link = $"{this.Scheme}://{this.host}{link}";
            }
            else if (!link.StartsWith("http"))
            {
                rType = ResourceType.LocalResource;
                var maindirectory = Path.GetDirectoryName(this.target.LocalPath);
                link = $"{this.Scheme}://{this.host}{maindirectory.Replace("\\", "/")}/{link}";
            }
            else
            {
                rType = ResourceType.OnlineResource;
                flag = false;
            }


            if (rType == ResourceType.LocalResource)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[{rType.ToString()}] {link}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{rType.ToString()}] {link}");
                Console.ResetColor();
            }
            return flag;
        }
        public List<Uri> GetAllResources()
        {
            List<Uri> gg = new List<Uri>();
            foreach (Match m in Helpers.JS_RGX.Matches(this.Source))
            {
                var src = m.Groups[1].Value;
                if (DetectLocalResource(ref src))
                {
                    gg.Add(new Uri(src));
                }

            }
            foreach (Match m in Helpers.RES_RGX.Matches(this.Source))
            {
                var src = m.Groups[1].Value;
                if (DetectLocalResource(ref src))
                {
                    gg.Add(new Uri(src));
                }

            }
            foreach (Match m in Helpers.IMG_RGX.Matches(this.Source))
            {
                var src = m.Groups[1].Value;
                if (DetectLocalResource(ref src))
                {
                    gg.Add(new Uri(src));
                }

            }
            return gg;
            //throw new NotImplementedException();
        }
        public void StoreCurrent(){
            Directory.CreateDirectory($"{Environment.CurrentDirectory}{Path.GetDirectoryName(this.target.LocalPath)}");
            var fn = Path.GetFileNameWithoutExtension(this.target.LocalPath);
            File.WriteAllText($"{Environment.CurrentDirectory}{Path.GetDirectoryName(this.target.LocalPath)}/{(String.IsNullOrWhiteSpace(fn) ? "index" : fn)}.html", this.Source);
        }
        public void Dump() => this.Source = HttpUtility.HtmlDecode(new WebClient().DownloadString(this.target));
    }
}
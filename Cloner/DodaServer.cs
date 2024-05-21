using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebDoda
{
    class DodaServer
    {
        private List<Uri> dodalist { get; set; } = new List<Uri>();
        private int[] Port { get; set; }
        private HttpListener listener { get; set; }
        private bool flag { get; set; }
        public DodaServer(int[] port)
        {
            if (!HttpListener.IsSupported)
            {
                throw new Exception("Your System Dosen't Match Requirement");
            }
            this.Port = port;
            this.listener = new HttpListener();
            for (int i = 0; i < this.Port.Length; i++)
            {
                this.listener.Prefixes.Add($"http://localhost:{this.Port[i]}/");
            }
        }
        public void start()
        {
            new Task(() =>
            {
                listener.Start();
                flag = true;
                Console.WriteLine("Listening For Assets...\nPress Enter To Stop Listeining");
                while (flag)
                {
                    try
                    {


                        //Get Request body
                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        string documentContents;
                        using (Stream receiveStream = request.InputStream)
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                documentContents = readStream.ReadToEnd();
                            }
                        }

                        /// Request Body
                        var data = JObject.Parse(documentContents);

                        dodalist.Add(new Uri(data["url"].ToString()));
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("[Plugin]");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($" {data["method"]} -> {data["url"].ToString()}");

                        ///return 0x00 to close connection
                        HttpListenerResponse response = context.Response;
                        response.ContentLength64 = 1;
                        System.IO.Stream output = response.OutputStream;
                        output.Write(new byte[1], 0, 1);
                        output.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("That Maybe Cuz Unexpected Close Listener Object");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                    }
                }
            }).Start();
        }

        string GetExtension(string contentType)
        {
            if (String.IsNullOrWhiteSpace(contentType))
            {
                return null;
            }
            if (contentType.Contains(';'))
            {
                contentType = contentType.Split(';')[0];
            }
            return MimeTypes.MimeTypeMap.GetExtension(contentType);
        }

        public void DodaTime()
        {
            for (int i = 0; i < this.dodalist.Count; i++)
            {
                try
                {
                    var web = new WebClient();
                    var bytes = web.DownloadData(this.dodalist[i]);
                    var ext = Path.GetExtension(this.dodalist[i].LocalPath);
                    if (String.IsNullOrWhiteSpace(ext))
                        ext = GetExtension(web.ResponseHeaders["Content-Type"]);
                    if (ext == null) continue;
                    try
                    {
                        Directory.CreateDirectory($"{Environment.CurrentDirectory}/DodaDump{Path.GetDirectoryName(this.dodalist[i].LocalPath).Replace("\\", "/")}");
                    }
                    catch
                    {

                    }
                    File.WriteAllBytes($"{Environment.CurrentDirectory}/DodaDump{Path.GetDirectoryName(this.dodalist[i].LocalPath).Replace("\\", "/")}/{Path.GetFileNameWithoutExtension(this.dodalist[i].LocalPath)}{ext}", bytes);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[DodaTime]");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" GET -> {this.dodalist[i].ToString()}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[Server Error]");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void Stop()
        {
            this.flag = false;
            this.listener.Stop();
        }
    }
}
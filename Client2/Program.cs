using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Text;
using System.Web;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();

            string content = HttpPost("http://localhost.fiddler:9070/uploadfile", @"C:\TEMP.TIF");
            
            Console.Write(content);
            Console.ReadLine();
            
            HttpGet("http://localhost.fiddler:9070/uploadfile?DocumentId="+content);   
        }
        
        /// <summary>
        /// Performs a basic HTTP POST request
        /// </summary>
        /// <param name="url">The URL of the asp page where document is processed(saves doc, returns doc Id).</param>
        /// <param name="path">path of the file to be processed.</param>
        /// <returns>HTML Content of the response(Specifically, the stored document Id).</returns>
        public static string HttpPost(string url, string path)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            using (Stream requestStream = request.GetRequestStream())
            {
                fileStream.CopyTo(requestStream);
            }
            
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();           
        }
        


        public static void HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream sr = response.GetResponseStream())
            {

                using (var fileStream = File.Create(@"C:\TEMP2.TIF"))
                {
                    sr.CopyTo(fileStream);
                }

            }
        }
    }
}

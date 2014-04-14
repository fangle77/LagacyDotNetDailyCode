using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;


namespace SearchResponseSpider
{
    public class NetRequest
    {
        private static Logger logger = new Logger("NetRequest");

        private static int timeOutMs = 300 * 1000;
        public static int TimeOutSecond
        {
            get { return timeOutMs / 1000; }
            set { timeOutMs = value * 1000; }
        }

        public static string GetRequest(string url)
        {
            return GetRequest(url, null);
        }

        public static string GetRequest(string url, IEnumerable<Cookie> cookies)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";

                request.Timeout = timeOutMs;
                AddCookieToRequest(cookies, request);

                //byte[] byteArray = Encoding.UTF8.GetBytes(queryToSend);
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentLength = byteArray.Length;
                //request.Timeout = mercadoTimeout;
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();
                // Get the response.
                WebResponse webResponse = request.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = webResponse.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                webResponse.Close();

                return responseFromServer;
            }
            catch (Exception ex)
            {
                logger.Log(DateTime.Now + "===========");
                logger.Log(url);
                logger.Log(ex.Message);
                logger.Log(ex.StackTrace);
                throw ex;
            }
        }

        private static void AddCookieToRequest(IEnumerable<Cookie> cookies, HttpWebRequest request)
        {
            if (cookies != null && cookies.Count() > 0)
            {
                request.CookieContainer = new CookieContainer();
                foreach (var cookie in cookies)
                {
                    request.CookieContainer.Add(cookie);
                }
            }
        }

        public static string PostRequest(string url, string data)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = timeOutMs;
                
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                request.Timeout = 30000;//mercadoTimeout;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                // Get the response.
                WebResponse webResponse = request.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = webResponse.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                webResponse.Close();

                return responseFromServer;
            }
            catch (Exception ex)
            {
                logger.Log(DateTime.Now + "===========");
                logger.Log(url);
                logger.Log(ex.Message);
                logger.Log(ex.StackTrace);
                throw ex;
            }
        }
    }
}

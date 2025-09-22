using MessageEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Updater
{
    public class WebClientEx : WebClient
    {
        public WebClientEx(CookieContainer container)
        {
            this.container = container;
        }

        public CookieContainer CookieContainer
        {
            get { return container; }
            set { container = value; }
        }

        private CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                CookieCollection cookies = response.Cookies;
                container.Add(cookies);
            }
        }
    }


    public class HttpHelper
    {

        public CookieContainer cookies = new CookieContainer();


        //[Obfuscation(Feature = "virtualization", Exclude = false)]

        public string DoRequest(string method, string requestURI, string post_dat, bool errmsg = true, int timeout = 5000)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURI);

                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version10;
                request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 70.0.3538.77 Safari / 537.36";
                request.CookieContainer = cookies;

                request.Method = method;

                request.Timeout = timeout;
                request.ContentType = "application/x-www-form-urlencoded";

                if(post_dat != null)
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(post_dat);

                    request.ContentLength = buffer.Length;

                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(buffer, 0, buffer.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream receiveStream = response.GetResponseStream();

                StreamReader readReceiveStream = new StreamReader(receiveStream, Encoding.UTF8);

                string read_data = readReceiveStream.ReadToEnd();

                readReceiveStream.Close();

                response.Close();

                return read_data;

            }
            catch (Exception ex)
            {
                if(errmsg)
                {
                    MessageBoxEx.ShowError(ex.Message, "Error", 10000);
                }
                
            }
            return null;
        }
    }
}

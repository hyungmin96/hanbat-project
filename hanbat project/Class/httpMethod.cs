using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Strategy
{
    public class httpMethod
    {

        String _method, _postData;
        Uri _uri;

        public httpMethod(String _method, Uri _uri, String _postData = "")
        {
            this._method = _method;
            this._postData = _postData;
            this._uri = _uri;
        }

        public String Method()
        {

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(_postData);

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = _method;
            postReq.ContentType = "application/x-www-form-urlencoded";
            postReq.UserAgent = "Mozilla/5.0 (Linux; Android 9.0; MI 8 SE) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.119 Mobile Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/";
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            postReq.ContentLength = byteData.Length;
            postReq.CookieContainer = Singleton.getInstance().getCookie();

            if (_method == "POST" || _method == "post")
            {
                using (Stream sw = postReq.GetRequestStream())
                {
                    sw.Write(byteData, 0, byteData.Length);
                }
            }

            HttpWebResponse response = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }

        }

    }
}

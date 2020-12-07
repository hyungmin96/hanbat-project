using hanbat_project.Class;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Strategy
{
    public abstract class httpMethod
    {
        public string _html;

        public abstract void method(setHttpProtocol protocol);
    }

    public class setPost : httpMethod
    {
        public override void method(setHttpProtocol protocol)
        {
            _html = new HttpWebRequestClass("POST", protocol).Method();
        }
    }

    public class setGet : httpMethod
    {
        public override void method(setHttpProtocol protocol)
        {
            _html = new HttpWebRequestClass("GET", protocol).Method();
        }
    }

    public class returnResult : httpMethod
    {
        public bool _result;

        public override void method(setHttpProtocol protocol)
        {
            _result = new HttpWebRequestClass("POST", protocol).Method().Contains(protocol._query);
        }
    }

    public class HttpWebRequestClass
    {

        private String _method, _postData = "";
        private Uri _uri;

        public HttpWebRequestClass(String _method, setHttpProtocol protocol)
        {
            this._method = _method;
            this._uri = protocol._uri;
            this._postData = protocol._postData;
        }

        public String Method()
        {

            byte[] byteData = UTF8Encoding.UTF8.GetBytes(_postData);

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = _method;
            postReq.ContentType = "application/x-www-form-urlencoded";
            postReq.UserAgent = "Mozilla/5.0 (Linux; Android 9.0; MI 8 SE) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.119 Mobile Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/Report.do";
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

            Singleton.getInstance().setCookie(postReq.CookieContainer);

            HttpWebResponse response = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }

        }

    }

}

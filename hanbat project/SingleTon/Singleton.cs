using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Class
{
    public class Singleton
    {

        public static Singleton Instance = null;

        private static CookieContainer _cookieContainer = new CookieContainer();

        private String Method, postData;

        private Uri uri;

        public static Singleton getInstance(string Method, String postData, Uri uri)
        {

            if (Instance == null)
                Instance = new Singleton(Method, postData, uri);

            return Instance;
            
        }

        private Singleton(String Method, String postData, Uri uri)
        {
            this.Method = Method;
            this.postData = postData;
            this.uri = uri;
        }

        public String httpMethod()
        {

            byte[] dataByte = UTF8Encoding.UTF8.GetBytes(postData);

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(uri);
            postReq.Method = Method;
            postReq.UserAgent = "Mozilla/5.0 (Linux; Android 9.0; MI 8 SE) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.119 Mobile Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/";
            postReq.ContentLength = dataByte.Length;
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            postReq.CookieContainer = _cookieContainer;

            if(Method == "POST" || Method == "post")
            {
                using(Stream sw = postReq.GetRequestStream())
                {
                    sw.Write(dataByte, 0, dataByte.Length);
                }
            }

            HttpWebResponse res = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }

        }

    }
}

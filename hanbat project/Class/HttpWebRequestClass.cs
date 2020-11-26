using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace hanbat_project.Class
{
    public class HttpWebRequestClass
    {

        Main main;
        CookieContainer cookies = new CookieContainer();

        #region [ Construct ]

        public HttpWebRequestClass(Main main)
        {
            this.main = main;
        }

        #endregion

        #region [ hanbat Login ]

        public bool Login(String Id, String Pw)
        {

            String html = "";

            Uri _uri = new Uri("https://cyber.hanbat.ac.kr/User.do?cmd=loginUser");

            String PostData = "cmd=loginUser&userId=" + Id + "&password=" + Pw;
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(PostData);

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = "POST";
            postReq.ContentType = "application/x-www-form-urlencoded";
            postReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/";
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            postReq.ContentLength = byteData.Length;
            postReq.CookieContainer = cookies;

            using(Stream sw = postReq.GetRequestStream())
            {
                sw.Write(byteData, 0, byteData.Length);
            }

            HttpWebResponse response = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                html = sr.ReadToEnd();
                MessageBox.Show(cookies.GetCookieHeader(_uri));
            }


            if (html.Contains("한밭대학교, 사이버캠퍼스"))
            {
                gotoUrl(); 
                return true;
            }
            else
                return false;

        }

        #endregion

        #region test

        public void gotoUrl()
        {
            String html;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome");

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = "GET";
            postReq.Headers.Add("Cookie", cookies.GetCookieHeader(_uri));
            postReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome";
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";

            HttpWebResponse response = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                html = sr.ReadToEnd();
                cookies.GetCookieHeader(_uri);
                MessageBox.Show(cookies.GetCookieHeader(_uri));
                new Selenium(main).openChrome("", cookies.GetCookieHeader(_uri));
            }
        }

        #endregion

    }
}

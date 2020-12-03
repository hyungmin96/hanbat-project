using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hanbat_project.Facade
{
    public class getIdInfo
    {

        Main main;
        CookieContainer cookies;

        public getIdInfo(Main main, CookieContainer cookies)
        {
            this.main = main;
            this.cookies = cookies;
        }

        public void getInfo()
        {
            // 로그인한 계정의 학번과 이름을 가져옴
            String html;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome&userDTO.localeKey=ko");

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = "GET";
            postReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/";
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            postReq.CookieContainer = cookies;

            HttpWebResponse res = (HttpWebResponse)postReq.GetResponse();
            using(StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                html = sr.ReadToEnd();
            }

            main.label2.Text = Regex.Replace(Regex.Split(Regex.Split(html, "<p class=\"mt5\"><span>")[1], "</p>")[0], "</span>", String.Empty);

        }

    }

}

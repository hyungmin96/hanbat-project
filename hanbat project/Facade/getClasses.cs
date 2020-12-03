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
    public class getClasses
    {

        Main main;
        CookieContainer cookies;

        public getClasses(Main main, CookieContainer cookies)
        {
            this.main = main;
            this.cookies = cookies;
        }


        public void getClassList()
        {

            // 로그인한 계정이 수강중인 과목을 불러옴

            String html;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome");

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

            foreach (String _class in html.Split(new String[] { "<option value = '" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!(_class.Contains("강의실 선택") || _class.Contains("한밭대학교 이러닝 캠퍼스")))
                {
                    String uri = Regex.Split(_class, ",")[0];
                    String type = "사이버 한밭";
                    String professor = Regex.Split(Regex.Split(_class, ",")[1], ",")[0];
                    String className = Regex.Split(Regex.Split(_class, "> ")[1], "<")[0];

                    String[] _content = { "", Convert.ToString(main.customListView2.Items.Count + 1), type, professor, className, uri };
                    main.addItems(main.customListView2, _content);
                }
            }

        }


    }

}

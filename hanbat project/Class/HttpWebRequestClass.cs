using hanbat_project.CustomClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hanbat_project.Class
{
    public class HttpWebRequestClass
    {

        object obj;
        CookieContainer cookies = new CookieContainer();

        #region [ Construct ]

        public HttpWebRequestClass(object obj)
        {
            this.obj = obj;
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
            }


            if (html.Contains("한밭대학교, 사이버캠퍼스"))
            {
                return true;
            }
            else
                return false;

        }

        #endregion

        #region [ get List of Classes ]

        public void getClasses(Main main)
        {

            List<classData> _list = new List<classData>();

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

        #endregion

        #region [ inquiry Class ] 

        public Dictionary<String, List<CustomItem>> inquiryClass(String _classNum)
        {

            Dictionary<String, List<CustomItem>> _dict = new Dictionary<String, List<CustomItem>>();

            String html;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=viewStudyHome&courseDTO.courseId=" + _classNum  + "&boardInfoDTO.boardInfoGubun=study_home&boardGubun=study_course&gubun=study_course");

            HttpWebRequest postReq = (HttpWebRequest)HttpWebRequest.Create(_uri);
            postReq.Method = "GET";
            postReq.UserAgent = "Mozilla/5.0 (Linux; Android 9.0; MI 8 SE) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.119 Mobile Safari/537.36";
            postReq.Referer = "http://cyber.hanbat.ac.kr/";
            postReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            postReq.CookieContainer = cookies;

            HttpWebResponse res = (HttpWebResponse)postReq.GetResponse();
            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                html = sr.ReadToEnd();
            }

            foreach (String _date in html.Split(new String[] { "icon-time mr5" }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (_date.Contains("boxTable"))
                {

                    List<CustomItem> _lst = new List<CustomItem>();

                    String _weekNum = Regex.Split(Regex.Split(_date, "></i>")[1], "<")[0];
                    String _deadline = Regex.Split(Regex.Split(_date, "<span>")[1], "</span>")[0];

                    String _keyValue = Regex.Replace(Regex.Replace((_weekNum + "\n" + _deadline), "\t", String.Empty), "\r\n", String.Empty).Trim();

                    _dict.Add(_keyValue, null);

                    foreach (String _info in _date.Split(new String[] { "boxTable" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (_info.Contains("viewStudyContents")){
                            String _Uri = Regex.Split(Regex.Split(_info, "'")[1], "'")[0];
                            String _name = StripHTML(Regex.Split(Regex.Split(_info, "<li><span class=\"fcBluesky\">")[1], "</li>")[0]);

                            String _curTime;
                            String _endTime;

                            if (!_info.Contains("학습안함"))
                            {
                                _curTime = Regex.Split(Regex.Split(Regex.Split(_info, "<ul class=\"bar\">")[1], ">")[1], "/")[0].Trim();
                                _endTime = Regex.Split(Regex.Split(Regex.Split(_info, "<ul class=\"bar\">")[1], "/ ")[2], "<")[0].Trim();
                            }
                            else
                            {
                                _curTime = "0";
                                _endTime = "1";
                            }

                            int _progressedVal = (getTime(_curTime) / getTime(_endTime) * 100);

                            CustomItem _item = new CustomItem();
                            _item._uri = _Uri;
                            _item._ClassName = _name;
                            _item._curTime = _curTime;
                            _item._endTime = _endTime;
                            _item._progress = (_progressedVal > 100) ? 100 : _progressedVal;

                            _lst.Add(_item);
                        }
                    }

                    _dict[_keyValue] = _lst;

                }

            }

            return _dict;

        }

        private int getTime(String time)
        {
            if (time.Length > 0 && time != "0")
            {

                int _m;

                if(time.Contains("분") && time.Contains("초"))
                    _m = int.Parse(Regex.Split(time, "분")[0]);
                else if (time.Contains("분"))
                    _m = int.Parse(Regex.Split(time, "분")[0]);
                else
                    _m = int.Parse(Regex.Split(time, "초")[0]);

                int _s;
                if (time.Contains("분") && time.Contains("초"))
                    _s = int.Parse(Regex.Split(time, "분")[0]);
                else if (time.Contains("분"))
                    _s = int.Parse(Regex.Split(time, "분")[0]);
                else
                    _s = int.Parse(Regex.Split(time, "초")[0]);

                int total = (_m * 60) + _s;

                return total;
            }

            return 0;

        }

        private static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        #endregion

        #region [ takeClass ]

        public void takeClass()
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
                new Selenium(obj).openChrome("", cookies.GetCookieHeader(_uri));
            }
        }

        #endregion

    }
}

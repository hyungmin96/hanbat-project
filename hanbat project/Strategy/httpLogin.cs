using hanbat_project.Class;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace hanbat_project.Strategy
{
    public class httpLogin
    {

        public CookieContainer _cookieContainer = new CookieContainer();

        public String _cookie = null;

        public bool LoginMethod(String Id, String Pw)
        {


            Uri _uri = new Uri("https://cyber.hanbat.ac.kr/User.do?cmd=loginUser");

            String PostData = "cmd=loginUser&userId=" + Id + "&password=" + Pw;

            String html = Singleton.getInstance("POST", PostData, _uri).httpMethod();

            if (html.Contains("한밭대학교, 사이버캠퍼스"))
            {
                _cookie = _cookieContainer.GetCookieHeader(_uri);
                return true;
            }
            else
                return false;

        }

    }

}

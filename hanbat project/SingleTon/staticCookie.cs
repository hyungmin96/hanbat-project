using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Class
{
    public class staticCookie
    {

        public static staticCookie Instance = null;

        public static CookieContainer _container = new CookieContainer();
        
        private staticCookie() { }

        public static staticCookie getInstance()
        {
            if (Instance == null)
                Instance = new staticCookie();

            return Instance;
        }

        public void setCookie(CookieContainer _cook)
        {
            _container = _cook;
        }

        public CookieContainer getCookie()
        {
            return _container;
        }

    }

}

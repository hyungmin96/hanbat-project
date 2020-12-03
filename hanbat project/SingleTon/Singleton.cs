using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Class
{
    public class Singleton
    {

        public static Singleton Instance = null;

        public static CookieContainer _container = new CookieContainer();
        
        private Singleton()
        {

        }

        public static Singleton getInstance()
        {
            if (Instance == null)
                Instance = new Singleton();

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

using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Class
{
    public class s_Singleton
    {

        public static Selenium Instance = null;

        private s_Singleton() { }

        public static Selenium getInstance()
        {
            if (Instance == null)
                Instance = new Selenium();

            return Instance;
        }

    }

}

using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Class
{
    public class staticSelenium
    {

        public static takeClass Instance = null;

        private staticSelenium() { }

        public static takeClass getInstance()
        {
            if (Instance == null)
                Instance = new takeClass();

            return Instance;
        }

    }

}

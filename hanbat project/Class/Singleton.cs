using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Class
{
    public class Singleton
    {

        private static Selenium Instance = null;

        public static Selenium getInstance()
        {

            if (Instance == null)
                Instance = new Selenium();

            return Instance;
            
        }

    }
}

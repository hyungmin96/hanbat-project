using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Facade
{
    public class getDate
    {

        Main main;

        public getDate(Main main)
        {
            this.main = main;
        }

        public void getDateMethod()
        {
            main.label6.Text = "오늘 날짜 : " + DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss");
        }


    }
}

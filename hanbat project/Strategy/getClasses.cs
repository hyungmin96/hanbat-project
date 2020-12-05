using hanbat_project.Class;
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


        public getClasses() { }

        public void getClassList()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome");

            Class.HttpWebRequestClass http = new Class.HttpWebRequestClass("GET", _uri);

            String html = http.Method();

            foreach (String _class in html.Split(new String[] { "<option value = '" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!(_class.Contains("강의실 선택") || _class.Contains("한밭대학교 이러닝 캠퍼스")))
                {
                    String uri = Regex.Split(_class, ",")[0];
                    String type = "사이버 한밭";
                    String professor = Regex.Split(Regex.Split(_class, ",")[1], ",")[0];
                    String className = Regex.Split(Regex.Split(_class, "> ")[1], "<")[0];

                    String[] _content = { "", Convert.ToString(MainForm.main.customListView2.Items.Count + 1), type, professor, className, uri };
                    MainForm.main.addItems(MainForm.main.customListView2, _content);
                }
            }

        }


    }

}

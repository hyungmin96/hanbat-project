using System;
using System.Text.RegularExpressions;
using hanbat_project.Strategy;

namespace hanbat_project.Facade
{
    public class getClasses
    {

        public getClasses() { }

        public void getClassList()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome");

            setGet setget = new setGet();
            setget.method(new setHttpProtocol(_uri));

            foreach (String _class in setget._html.Split(new String[] { "<option value = '" }, StringSplitOptions.RemoveEmptyEntries))
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

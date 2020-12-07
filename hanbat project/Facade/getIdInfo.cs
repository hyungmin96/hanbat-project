using System;
using System.Text.RegularExpressions;

namespace hanbat_project.Facade
{
    public class getIdInfo
    {

        public getIdInfo()  {  }

        public void getInfo()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome&userDTO.localeKey=ko");

            Strategy.setGet setget = new Strategy.setGet();
            setget.method(new Strategy.setHttpProtocol(_uri));

            MainForm.main.label2.Text = Regex.Replace(Regex.Split(Regex.Split(setget._html,
                "<p class=\"mt5\"><span>")[1], "</p>")[0], "</span>", String.Empty);

        }

    }

}

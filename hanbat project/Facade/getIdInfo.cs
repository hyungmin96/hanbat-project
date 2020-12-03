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
    public class getIdInfo
    {

        Main main;

        public getIdInfo(Main main)
        {
            this.main = main;
        }

        public void getInfo()
        {
            // 로그인한 계정의 학번과 이름을 가져옴

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Main.do?cmd=viewHome&userDTO.localeKey=ko");

            //String html = Singleton.getInstance("GET", _uri).httpMethod();

            //main.label2.Text = Regex.Replace(Regex.Split(Regex.Split(html, "<p class=\"mt5\"><span>")[1], "</p>")[0], "</span>", String.Empty);

        }

    }

}

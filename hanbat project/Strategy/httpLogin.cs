using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Strategy
{
    public class httpLogin : httpMethod
    {

        public bool result = false;

        public override void method()
        {
            Uri _uri = new Uri("https://cyber.hanbat.ac.kr/User.do?cmd=loginUser");
            String postData = "cmd=loginUser&userId=" + new LoginForm().customTextbox1.val + "&password=" + new LoginForm().customTextbox2.val + "";

            String html = new HttpWebRequestClass("POST", _uri, postData).Method();

            if (html.Contains("한밭대학교, 사이버캠퍼스입니다"))
                result = true;
            else
                result = false;
        }

    }

}

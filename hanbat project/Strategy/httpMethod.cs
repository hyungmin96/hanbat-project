using hanbat_project.Class;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace hanbat_project.Strategy
{
    public abstract class httpMethod
    {
        public string _result;
        public abstract void method(string uri, string postData = null);
    }

    public class setPost : httpMethod
    {
        public override void method(string uri, string postData = null)
        {
            _result = new HttpWebRequestClass("POST", new Uri(uri), postData).Method();
        }
    }

    public class setGet : httpMethod
    {
        public override void method(string uri, string postData = null)
        {
            _result = new HttpWebRequestClass("GET", new Uri(uri)).Method();
        }
    }
}

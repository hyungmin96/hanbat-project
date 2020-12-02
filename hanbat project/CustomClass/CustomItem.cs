using hanbat_project.Class;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hanbat_project.CustomClass
{
    public partial class CustomItem : UserControl
    {

        String _cookie;

        public CustomItem(String _cookie)
        {
            this._cookie = _cookie;
            InitializeComponent();
        }

        String  ClassName, uri, curTime, endTime;
        double progress;

        public CustomItem(String uri, String ClassName, String curTime, String endTime, double progress)
        {
            this.uri = uri;
            this.ClassName = ClassName;
            this.curTime = curTime;
            this.endTime = endTime;
            this.progress = progress;
            InitializeComponent();
        }

        private void CustomItem_Load(object sender, EventArgs e)
        {
            if (progress != 0)
                label1.Text = curTime + " / " + endTime + "(" + progress + "%)";
            else
                label1.Text = "학습안함(0%)";
        }

        public String _uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public String _ClassName
        {
            get { return ClassName; }
            set { ClassName = value; classLbl.Text = value; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Singleton.getInstance().openChrome(uri, _cookie);
        }

        public String _curTime
        {
            get { return curTime; }
            set { curTime = value; }
        }

        public String _endTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public double _progress
        {
            get { return progress; }
            set { progress = value; progressBar1.Value = (int)value; }
        }

    }

}

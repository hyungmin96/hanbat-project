using hanbat_project.Strategy;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace hanbat_project
{
    public partial class LoginForm : Form
    {

        #region [ Gloval Variable ]

        private bool On;
        private Point Pos;

        public static LoginForm loginForm;

        #endregion

        #region [ Form Method ]

        #region [ Form License & Load ]

        private void Login_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        #endregion

        #region [ Form Move ]
        public LoginForm()
        {
            InitializeComponent();
            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

        #endregion

        #region [ Form Minimize / Exit ] 
        private void button13_Click(object sender, EventArgs e)
        {
            exitChrome();
            Environment.Exit(Environment.ExitCode);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #endregion

        #region [ Kill Chrome Process ] 

        public static void exitChrome()
        {

            Process[] Chrome = Process.GetProcessesByName("chromedriver");

            foreach (var ch in Chrome)
            {
                ch.Kill();
            }

        }


        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

            Uri _uri = new Uri("https://cyber.hanbat.ac.kr/User.do?cmd=loginUser");
            String postData = "cmd=loginUser&userId=" + customTextbox1.val + "&password=" + customTextbox2.val + "";

            setHttpProtocol protocol = new setHttpProtocol(_uri, postData, false, "한밭대학교, 사이버캠퍼스입니다");
            returnResult _result = new returnResult();
            _result.method(protocol);

            if (_result._result)
            {
                new MainForm().Show();
                this.Hide();
            }
            else
                MessageBox.Show("로그인에 실패하였습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            loginForm = this;
        }
    }
}

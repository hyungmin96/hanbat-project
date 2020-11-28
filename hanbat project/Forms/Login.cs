using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanbat_project
{
    public partial class Login : Form
    {

        #region [ Gloval Variable ]

        private bool On;
        private Point Pos;

        Thread mThread;

        #endregion

        #region [ Form Method ]

        #region [ Form License & Load ]

        private void Login_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        #endregion

        #region [ Form Move ]
        public Login()
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

            HttpWebRequestClass http = new HttpWebRequestClass(this);

            if (http.Login(customTextbox1.val, customTextbox2.val))
            {
                new Main(http).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("잘못된 계정입니다. 다시 시도해주세요.", "로그인 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }
}

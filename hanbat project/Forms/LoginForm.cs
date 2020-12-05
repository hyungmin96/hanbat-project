using hanbat_project.Class;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
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

            Strategy.httpLogin login = new Strategy.httpLogin();

            Strategy.Context context = new Strategy.Context(login);

            context.methodExecute();

            if (login.result)
            {
                new uploadFile().uploadMethod();
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

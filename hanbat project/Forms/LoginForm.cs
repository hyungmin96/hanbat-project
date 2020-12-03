using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanbat_project
{
    public partial class LoginForm : Form
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
                new MainForm().Show();
                this.Hide();
            }
            else
                MessageBox.Show("로그인에 실패하였습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://storage1.upload.pe/upload.php");
            FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length); fs.Close();

            Stream DataStream = new MemoryStream();
            string boundary = "------WebKitFormBoundaryWj22ldOndlVt5lA8";
            string postData = boundary + "\r\nContent-Disposition: form-data; name=\"referer\"\r\n\r\nhttps://www.google.com/";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"file\"; filename=\"" + Path.GetFileName(ofd.FileName) + "\"\r\nContent-Type: image/png\r\n\r\n";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"email\"" + "\r\n\r\n";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"password\"" + "\r\n\r\n123412341234";
            string footer = "\r\n------WebKitFormBoundaryWj22ldOndlVt5lA8--\r\n";
            DataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
            DataStream.Write(data, 0, data.Length);
            DataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, 2);
            DataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

            DataStream.Position = 0;
            byte[] formData = new byte[DataStream.Length];
            DataStream.Read(formData, 0, formData.Length); DataStream.Close();

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundaryWj22ldOndlVt5lA8";
            request.ContentLength = formData.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(resStream);
            string result = readStream.ReadToEnd();

            readStream.Close();
            resStream.Close();
        }


    }
}

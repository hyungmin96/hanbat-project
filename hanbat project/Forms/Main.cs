using ExtendedControls;
using hanbat_project.Class;
using hanbat_project.CustomClass;
using hanbat_project.Facade;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace hanbat_project
{
    public partial class Main : Form
    {

        #region [ Gloval Variable ]

        private bool On;

        private Point Pos;

        public static Dictionary<String, List<CustomItem>> _dict = new Dictionary<string, List<CustomItem>>();

        public static Main main;

        private String _classId;

        int currentPage = 0;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void Main_Load(object sender, EventArgs e)
        {

            main = this;

            FacadeClass facade = new FacadeClass(this);
            facade.displayInfo();
        }

        #endregion

        #region [ Form Constructor ]

        public Main()
        {

            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

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

        #region [ methods ]

        #region addItem

        public void addItems(CustomListView customlistview, String[] _vaules)
        {
            customlistview.BeginUpdate();

            ListViewItem _item = new ListViewItem(_vaules);
            customlistview.Items.Add(_item);

            if (customlistview.Items.Count * 35 >= customlistview.Height) customlistview.Columns[customlistview.Columns.Count - 1].Width = 0;

            customlistview.EndUpdate();
        }

        #endregion

        #endregion

        private void customListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            _classId = customListView2.FocusedItem.SubItems[5].Text;

            Strategy.Context context;

            context = new Strategy.Context(new Strategy.displayClasses());
            context.methodExecute();

            currentPage = _dict.Count - 1;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // 이전버튼
            if (currentPage == 0)
                MessageBox.Show("가장 처음 주차의 수업입니다.", "정보없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                currentPage--;

                String _key = _dict.Keys.ToList()[currentPage];

                label17.Text = _key.Split('\n')[0];
                label15.Text = _key.Split('\n')[1].Trim();

                flowLayoutPanel1.Controls.Clear();

                foreach (CustomItem _item in _dict[_key])
                {
                    flowLayoutPanel1.Controls.Add(_item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // 다음버튼
            if (currentPage == _dict.Count - 1)
                MessageBox.Show("가장 최신 주차의 수업입니다.", "정보없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                currentPage++;

                String _key = _dict.Keys.ToList()[currentPage];

                label17.Text = _key.Split('\n')[0];
                label15.Text = _key.Split('\n')[1].Trim();

                flowLayoutPanel1.Controls.Clear();

                foreach (CustomItem _item in _dict[_key])
                {
                    flowLayoutPanel1.Controls.Add(_item);
                }
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            exitChrome();
            Environment.Exit(Environment.ExitCode);
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Board().ShowDialog();
        }
    }

}


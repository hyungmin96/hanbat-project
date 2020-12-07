using ExtendedControls;
using hanbat_project.CustomClass;
using hanbat_project.Facade;
using hanbat_project.Strategy;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using hanbat_project.Observer;

namespace hanbat_project
{
    public partial class MainForm : Form
    {

        #region [ Gloval Variable ]

        private bool On;

        private Point Pos;

        public static MainForm main;

        int currentPage = 0;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void MainForm_Load(object sender, EventArgs e)
        {
            main = this;

            FacadeClass facade = new FacadeClass();
            facade.displayInfo();
            facade.displayItems();
        }

        #endregion

        #region [ Form Constructor ]

        public MainForm()
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

            String _classId = customListView2.FocusedItem.SubItems[5].Text;

            new displayClasses().getClassedList();

            currentPage = displayClasses._dict.Count - 1;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // 이전버튼
            if (currentPage == 0)
                MessageBox.Show("가장 처음 주차의 수업입니다.", "정보없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                currentPage--;

                String _key = displayClasses._dict.Keys.ToList()[currentPage];

                label17.Text = _key.Split('\n')[0];
                label15.Text = _key.Split('\n')[1].Trim();

                flowLayoutPanel1.Controls.Clear();

                foreach (CustomItem _item in displayClasses._dict[_key])
                {
                    flowLayoutPanel1.Controls.Add(_item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // 다음버튼
            if (currentPage == displayClasses._dict.Count - 1)
                MessageBox.Show("가장 최신 주차의 수업입니다.", "정보없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                currentPage++;

                String _key = displayClasses._dict.Keys.ToList()[currentPage];

                label17.Text = _key.Split('\n')[0];
                label15.Text = _key.Split('\n')[1].Trim();

                flowLayoutPanel1.Controls.Clear();

                foreach (CustomItem _item in displayClasses._dict[_key])
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

            new getNotice().getNoticeList();

            new Board(getNotice._dict).ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AssignmentForm(getAssignment._dict).ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FacadeClass().displayItems();
        }
    }

}


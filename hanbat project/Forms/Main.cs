﻿using ExtendedControls;
using hanbat_project.Class;
using hanbat_project.CustomClass;
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
    public partial class Main : Form
    {

        #region [ Gloval Variable ]

        private bool On;
        private Point Pos;

        HttpWebRequestClass http;
        Thread mThread;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void Main_Load(object sender, EventArgs e)
        {
            http.getClasses(this);
        }

        #endregion

        #region [ Form Constructor ]

        public Main(HttpWebRequestClass http)
        {
            InitializeComponent();

            this.http = http;

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

            if (customlistview.Items.Count % 2 == 0)
                customlistview.Items[customlistview.Items.Count - 1].BackColor = Color.FromArgb(29, 30, 31);
            else
                customlistview.Items[customlistview.Items.Count - 1].BackColor = Color.FromArgb(34, 36, 38);

            if (customlistview.Items.Count * 35 >= customlistview.Height) customlistview.Columns[customlistview.Columns.Count - 1].Width = 0;

            customlistview.EndUpdate();
        }

        #endregion

        #endregion

        #region [ Run ]

        private void Run()
        {

        }

        #endregion

        Dictionary<String, List<CustomItem>> _dict = new Dictionary<string, List<CustomItem>>();
        int currentPage = 0;
        private void customListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            _dict = http.inquiryClass(customListView2.FocusedItem.SubItems[5].Text);

            label13.Text = customListView2.FocusedItem.SubItems[3].Text;
            label11.Text = customListView2.FocusedItem.SubItems[4].Text;

            flowLayoutPanel1.Controls.Clear();

            if (_dict.Count > 0)
            {
                currentPage = _dict.Count - 1;

                String _key = _dict.Keys.ToList()[currentPage];

                label17.Text = _key.Split('\n')[0];
                label15.Text = _key.Split('\n')[1].Trim();


                foreach (CustomItem _item in _dict[_key])
                {
                    flowLayoutPanel1.Controls.Add(_item);
                }
            }
            else
            {
                MessageBox.Show("수업목록이 존재하지 않습니다.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 이전버튼
            if (currentPage == 0)
                MessageBox.Show("가장 마지막 주차의 수업입니다.", "정보없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}


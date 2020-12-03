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
    public partial class Board : Form
    {

        #region [ Gloval Variable ]

        private bool On;

        private Point Pos;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void Board_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region [ Form Constructor ]

        public Board()
        {

            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };
        }

        #endregion

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

        private void button13_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }

}


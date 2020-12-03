using ExtendedControls;
using hanbat_project.Class;
using hanbat_project.CustomClass;
using hanbat_project.dataClass;
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
    public partial class AssignmentForm : Form
    {

        #region [ Gloval Variable ]

        private bool On;

        private Point Pos;

        public static Board board;

        public static String BoardId;

        Dictionary<String, List<AssignmentData>> _dict;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void AssignmentForm_Load(object sender, EventArgs e)
        {

            customComboBox1.DataSource = _dict.Keys.ToArray();

        }

        #endregion

        #region [ Form Constructor ]

        public AssignmentForm(Dictionary<String, List<AssignmentData>> _dict)
        {

            this._dict = _dict;

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

        private void customListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label3.Text = customListView2.FocusedItem.SubItems[2].Text;

            Strategy.Context context = new Strategy.Context(new Strategy.getContent());
            context.methodExecute();
        }

        private void customComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach(AssignmentData _item in _dict[customComboBox1.Text])
            {
                String[] arr = new string[] { "", Convert.ToString(customListView2.Items.Count + 1), _item._title, _item._date, "", "" };
                addItems(customListView2, arr);
            }

        }

    }

}


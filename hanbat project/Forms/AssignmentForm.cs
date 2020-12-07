using ExtendedControls;
using hanbat_project.Class;
using hanbat_project.dataClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using hanbat_project.Observer;
using System.IO;

namespace hanbat_project
{
    public partial class AssignmentForm : Form
    {

        #region [ Gloval Variable ]

        private bool On;

        private Point Pos;

        public static AssignmentForm assignmentForm;

        Dictionary<String, List<AssignmentData>> _dict;

        #endregion

        #region [ Form Method ]

        #region [ Form Load ]

        private void AssignmentForm_Load(object sender, EventArgs e)
        {
            assignmentForm = this;
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
            int _totalNum = 0;
            MainForm.main.label8.Text = Facade.getAssignment._dict.Keys.ToArray().Sum(x => _totalNum += Facade.getAssignment._dict[x].Count).ToString() + "건";
            this.Close();
        }

        private void customListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            label3.Text = customListView2.FocusedItem.SubItems[2].Text;

            int selectidx = customListView2.FocusedItem.Index;

            linkLabel1.Text = _dict[customComboBox1.Text][selectidx]._f_name;

            customRichTextBox1.setValue = _dict[customComboBox1.Text][selectidx]._content;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            String[] _value = _dict[customComboBox1.Text][customListView2.FocusedItem.Index]._uri.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            String url = "http://cyber.hanbat.ac.kr/fileDownServlet?rFileName=" + _value[0] + "&sFileName=" + _value[1] + "&filePath=" + _value[2];

            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Cookie, Singleton.getInstance().getCookie().GetCookieHeader(new Uri(url)));
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 9.0; MI 8 SE) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.119 Mobile Safari/537.36");
            webClient.Headers.Add("Referer", "http://cyber.hanbat.ac.kr/MReport.do?cmd=viewReportInfoPageList&boardInfoDTO.boardInfoGubun=report&courseDTO.courseId=H020382002003200502513011");
            webClient.DownloadFile(url, linkLabel1.Text);

        }
        
        private void customComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            customListView2.Items.Clear();
            try
            {
                foreach (AssignmentData _item in _dict[customComboBox1.Text])
                {
                    String[] arr = new string[] { "", Convert.ToString(customListView2.Items.Count + 1), _item._title, _item._date, "", "" };
                    addItems(customListView2, arr);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("현재 진행중인 과제가 없습니다", "과제없음", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "업로드 할 파일을 선택해주세요.";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                customTextbox1.val = ofd.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            String _courseId = _dict[customComboBox1.Text][customListView2.FocusedItem.Index]._courseId;
            String _reportId = _dict[customComboBox1.Text][customListView2.FocusedItem.Index]._reportUri;

            Template.ExistFile existFile = null;
            Template.noneFile noneFile = null;

            if (File.Exists(customTextbox1.val))
            {
                existFile = new Template.ExistFile(_courseId, _reportId);
                existFile.run();
            }
            else
            {
                noneFile = new Template.noneFile(_courseId, _reportId);
                noneFile.run();
            }

            if((existFile != null && existFile._result) || (noneFile != null && noneFile._result))
            {
                o_concrete o = new o_concrete();

                o.add(new o_dictionary(o, customComboBox1.Text, customListView2.FocusedItem.Index));

                o.notify();

                o.clear();
            }

        }

        private void customComboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            customComboBox1.DataSource = _dict.Keys.ToArray();
        }

    }

}


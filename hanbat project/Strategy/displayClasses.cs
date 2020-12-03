using hanbat_project.Class;
using hanbat_project.CustomClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.Class;

namespace hanbat_project.Strategy
{
    public class displayClasses : StrategyClass
    {

        public override void method()
        {

            MainForm._dict.Clear();

            String _classNum = MainForm.main.customListView2.FocusedItem.SubItems[5].Text;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=viewStudyHome&courseDTO.courseId=" + _classNum + "&boardInfoDTO.boardInfoGubun=study_home&boardGubun=study_course&gubun=study_course");

            String html = new httpMethod("GET", _uri).Method();

            foreach (String _date in html.Split(new String[] { "icon-time mr5" }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (_date.Contains("boxTable"))
                {

                    List<CustomItem> _lst = new List<CustomItem>();

                    String _weekNum = Regex.Split(Regex.Split(_date, "></i>")[1], "<")[0];
                    String _deadline = Regex.Split(Regex.Split(_date, "<span>")[1], "</span>")[0];

                    String _keyValue = Regex.Replace(Regex.Replace((_weekNum + "\n" + _deadline), "\t", String.Empty), "\r\n", String.Empty).Trim();

                    MainForm._dict.Add(_keyValue, null);

                    foreach (String _info in _date.Split(new String[] { "boxTable" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (_info.Contains("viewStudyContents"))
                        {

                            String _Uri = Regex.Split(Regex.Split(_info, "'")[1], "'")[0];
                            String _name = Option.StripHTML(Regex.Split(Regex.Split(_info, "<li><span class=\"fcBluesky\">")[1], "</li>")[0]);

                            String _curTime;
                            String _endTime;

                            if (!_info.Contains("학습안함"))
                            {
                                _curTime = Regex.Split(Regex.Split(Regex.Split(_info, "<ul class=\"bar\">")[1], ">")[1], "/")[0].Trim();
                                _endTime = Regex.Split(Regex.Split(Regex.Split(_info, "<ul class=\"bar\">")[1], "/ ")[2], "<")[0].Trim();
                            }
                            else
                            {
                                _curTime = "0";
                                _endTime = "1";
                            }

                            if (_curTime == "") _curTime = "1";

                            var a = (double)(getTime(_curTime));
                            var b = (double)(getTime(_endTime));

                            double _progressedVal = double.Parse(String.Format("{0:0.#}", (a / b) * 100));

                            CustomItem _item = new CustomItem();

                            _item._uri = _Uri;
                            _item._classId = _classNum;
                            _item._ClassName = _name;
                            _item._curTime = _curTime;
                            _item._endTime = _endTime;
                            _item._progress = (_progressedVal >= 100) ? 100 : _progressedVal;

                            _lst.Add(_item);

                        }
                    }

                    MainForm._dict[_keyValue] = _lst;

                }
            }

            showClasses();

        }

        private void showClasses()
        {
            MainForm.main.label13.Text = MainForm.main.customListView2.FocusedItem.SubItems[3].Text;
            MainForm.main.label11.Text = MainForm.main.customListView2.FocusedItem.SubItems[4].Text;

            MainForm.main.flowLayoutPanel1.Controls.Clear();

            if (MainForm._dict.Count > 0)
            {
                String _key = MainForm._dict.Keys.ToList()[MainForm._dict.Count - 1];

                MainForm.main.label17.Text = _key.Split('\n')[0];
                MainForm.main.label15.Text = _key.Split('\n')[1].Trim();

                foreach (CustomItem _item in MainForm._dict[_key])
                {
                    MainForm.main.flowLayoutPanel1.Controls.Add(_item);
                }
            }
            else
                MessageBox.Show("수업목록이 존재하지 않습니다.");
        }

        private int getTime(String time)
        {
            if (time.Length > 0 && time != "0")
            {

                int _m;

                if (time.Contains("분") && time.Contains("초"))
                    _m = int.Parse(Regex.Split(time, "분")[0]);
                else if (time.Contains("분"))
                    _m = int.Parse(Regex.Split(time, "분")[0]);
                else
                    _m = int.Parse(Regex.Split(time, "초")[0]);

                int _s;
                if (time.Contains("분") && time.Contains("초"))
                    _s = int.Parse(Regex.Split(time, "분")[0]);
                else if (time.Contains("분"))
                    _s = int.Parse(Regex.Split(time, "분")[0]);
                else
                    _s = int.Parse(Regex.Split(time, "초")[0]);

                int total = (_m * 60) + _s;

                return total;
            }

            return 0;

        }

    }

}
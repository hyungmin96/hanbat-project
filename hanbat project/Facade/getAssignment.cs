using hanbat_project.dataClass;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp2.Class;

namespace hanbat_project.Facade
{
    public class getAssignment
    {

        public static Dictionary<String, List<AssignmentData>> _dict = new Dictionary<String, List<AssignmentData>>();

        int _number = 0;

        public getAssignment() { }

        public void getAssignmentMethod()
        {

            foreach (ListViewItem _item in MainForm.main.customListView2.Items)
            {

                String _classId = _item.SubItems[5].Text;

                Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Report.do?cmd=viewReportInfoPageList&boardInfoDTO.boardInfoGubun=report&courseDTO.courseId=" + _classId + "&mainDTO.parentMenuId=menu_00104&mainDTO.menuId=menu_00063");

                Class.httpMethod http = new Class.httpMethod("GET", _uri);

                String html = http.Method();

                _dict.Add(_item.SubItems[4].Text, null);

                List<AssignmentData> _lst = new List<AssignmentData>();

                foreach (String _class in html.Split(new String[] { "<i class=\"icon-openbook-color mr10\"></i>" }, StringSplitOptions.RemoveEmptyEntries))
                {

                    String f_name = null, file = null;

                    String _value = Regex.Replace(_class.Trim(), "&nbsp;", String.Empty);

                    if (_value.Contains("javascript:submitReport") && (_value.Contains("[진행중]") && !_value.Contains("제출완료")))
                    {

                        String _title = Regex.Split(_value, "\n")[0];
                        String _content = Option.StripHTML(Regex.Split(Regex.Split(_value, "<div class=\"cont pb0\" style=\"min-height:0;word-break:break-all;\">")[1], "</div>")[0].Trim());
                        String _date = Regex.Split(Regex.Split(Regex.Split(_value, "<td>")[1], "~ ")[1], "</td>")[0].Trim();
                       
                        if (_value.Contains("title='Download: "))
                        {
                            f_name = Regex.Split(Regex.Split(_value, "title='Download: ")[1], "'")[0];
                            file = Regex.Replace(Regex.Split(_value, "fileDownload")[1].Split('(', ')')[1], "'", String.Empty);
                        }

                        AssignmentData data = new AssignmentData(_title, _content, _date, f_name, file);
                        _lst.Add(data);

                        _dict[_item.SubItems[4].Text] = _lst;

                        _number += _lst.Count;

                    }

                }

                if (_dict[_item.SubItems[4].Text] == null || _dict[_item.SubItems[4].Text].Count < 1) 
                    _dict.Remove(_item.SubItems[4].Text);

                MainForm.main.label8.Text = Convert.ToString(_number) + "건";

            }


        }

    }

}

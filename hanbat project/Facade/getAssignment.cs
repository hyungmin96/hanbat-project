﻿using hanbat_project.dataClass;
using hanbat_project.Strategy;
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

        public void getAssignmentList()
        {

            foreach (ListViewItem _item in MainForm.main.customListView2.Items)
            {

                String _classId = _item.SubItems[5].Text;

                Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Report.do?cmd=viewReportInfoPageList&boardInfoDTO.boardInfoGubun=report&courseDTO.courseId=" + _classId + "&mainDTO.parentMenuId=menu_00104&mainDTO.menuId=menu_00063");

                setGet setget = new setGet();
                setget.method(new setHttpProtocol(_uri));

                _dict.Add(_item.SubItems[4].Text, null);

                List<AssignmentData> _lst = new List<AssignmentData>();

                String courseId = Regex.Split(Regex.Split(setget._html, "study_home&courseDTO.courseId=")[1], "\"")[0];

                foreach (String _class in setget._html.Split(new String[] { "<i class=\"icon-openbook-color mr10\"></i>" }, StringSplitOptions.RemoveEmptyEntries))
                {

                    String f_name = null, file = null;

                    String _value = Regex.Replace(_class.Trim(), "&nbsp;", String.Empty);

                    //if (_value.Contains("javascript:submitReport"))
                    if (_value.Contains("javascript:submitReport") && (_value.Contains("[진행중]") && !_value.Contains("제출완료")))
                    {

                        String _title = Regex.Split(_value, "\n")[0];
                        String _content = Option.StripHTML(Regex.Split(Regex.Split(_value, "<div class=\"cont pb0\" style=\"min-height:0;word-break:break-all;\">")[1], "</div>")[0].Trim());
                        String _date = Regex.Split(Regex.Split(Regex.Split(_value, "<td>")[1], "~ ")[1], "</td>")[0].Trim();
                        String _reportUri = Regex.Split(Regex.Split(Regex.Split(_value, "submitReport")[1], "'")[1], "'")[0].Trim();
                       
                        if (_value.Contains("title='Download: "))
                        {
                            f_name = Regex.Split(Regex.Split(_value, "title='Download: ")[1], "'")[0];
                            file = Regex.Replace(Regex.Split(_value, "fileDownload")[1].Split('(', ')')[1], "'", String.Empty);
                        }

                        AssignmentData data = new AssignmentData(courseId, _title, _content, _date, _reportUri, f_name, file);
                        _lst.Add(data);

                        _dict[_item.SubItems[4].Text] = _lst;

                        _number += 1;

                    }

                }

                if (_dict[_item.SubItems[4].Text] == null || _dict[_item.SubItems[4].Text].Count < 1) 
                    _dict.Remove(_item.SubItems[4].Text);

                MainForm.main.label8.Text = Convert.ToString(_number) + "건";

            }


        }

    }

}

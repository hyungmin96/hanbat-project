using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace hanbat_project.Strategy
{
    public class getNotice : StrategyClass
    {

        public static String BoardId;

        public Dictionary<String, Tuple<String, String>> _dict = new Dictionary<String, Tuple<String, String>>();

        public override void method()
        {

            String _classNum = MainForm.main.customListView2.FocusedItem.SubItems[5].Text;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=viewStudyHome&courseDTO.courseId=" + _classNum  + "&boardInfoDTO.boardInfoGubun=study_home&boardGubun=study_course&gubun=study_course");

            String html = new httpMethod("GET", _uri).Method();

            BoardId = Regex.Split(Regex.Split(html, "boardInfoGubun=notice&boardInfoDTO.boardInfoId=")[1], "&")[0];

            _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=mviewBoardContentsList&boardInfoDTO.boardInfoGubun=notice&boardInfoDTO.boardInfoId=" + BoardId + "&boardInfoDTO.boardClass=notice&boardInfoDTO.boardType=course&courseDTO.courseId=" + _classNum);

            html = new httpMethod("GET", _uri).Method();

            foreach (String _notice in html.Split(new String[] { "<li class=\"aa\" >" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (_notice.Contains("javascript:moveBoardView"))
                {

                    String _title = Regex.Split(Regex.Split(_notice, ";\">")[1], "<")[0];
                    String _addr = Regex.Split(Regex.Split(_notice, "'")[1], "'")[0];
                    String _date = Regex.Split(Regex.Split(_notice, "<li>")[2], "<")[0];

                    _dict.Add(_title, Tuple.Create<String, String>(_addr, _date));

                }

            }

        }

    }

}

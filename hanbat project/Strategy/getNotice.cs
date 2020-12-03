using System;
using System.Net;
using System.Text.RegularExpressions;

namespace hanbat_project.Strategy
{
    public class getNotice : StrategyClass
    {

        public override void method()
        {

            String _classNum = Main.main.customListView2.FocusedItem.SubItems[5].Text;

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=viewStudyHome&courseDTO.courseId=H020382002003200502513011&boardInfoDTO.boardInfoGubun=study_home&boardGubun=study_course&gubun=study_course");

            String html = new httpMethod("GET", _uri).Method();

            String _boardId = Regex.Split(Regex.Split(html, "boardInfoGubun=notice&boardInfoDTO.boardInfoId=")[1], "&")[0];

            _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do?cmd=mviewBoardContentsList&boardInfoDTO.boardInfoGubun=notice&boardInfoDTO.boardInfoId=" + _boardId + "&boardInfoDTO.boardClass=notice&boardInfoDTO.boardType=course&courseDTO.courseId=" + _classNum);

            html = new httpMethod("GET", _uri).Method();



        }

    }

}

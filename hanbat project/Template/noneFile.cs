using hanbat_project.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Template
{
    public class noneFile : ExistFile
    {

        String courseId, reportId;

        public bool _result;

        public noneFile(String courseId, String reportId) : base(courseId, reportId) { }

        public override void up_or_none_File() { }

        public override void submit()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Report.do");

            String _postData = "courseId=" + courseId + "" +
                "&courseDTO.courseId=" + courseId + "" +
                "&courseDTO.shareCourseYn=N&courseDTO.shareCourseId=&courseDTO.shareCourseItem=" +
                "&reportInfoDTO.reportInfoId=" + reportId + "" +
                "&reportSubmitDTO.reportSubmitId=&reportInfoDTO.maxPostSize=500&reportSubmitDTO.fileNum=1&reportInfoDTO.teamCategoryId=&boardInfoDTO.boardInfoGubun=report&gubun=A&editGubun=&mainDTO.parentMenuId=menu_00104&mainDTO.menuId=menu_00063" +
                "&userId=" + LoginForm.loginForm.customTextbox1.val + "" +
                "&reportSubmitDTO.submitNum=1&uploadSeqIds=&uploadFileNames=&uploadFileIds=&uploadFileSizes=&deleteFileList=&boardInfoDTO.boardClass=bbs&reportInfoDTO.teamReportYn=N&cmd=addReportSubmit&reportSubmitDTO.reportSummary=" + AssignmentForm.assignmentForm.customRichTextBox2.setValue + "&atchuploader=&fileIdData=&fileNameData=&fileSizeData=&maskFileNameData=&filePathData=&totalFileSizeData=0";

            setPost setpost = new setPost();
            setpost.method(new setHttpProtocol(_uri, _postData));

            if (setpost._html.Contains("<th class=\"head\"><label for=\"subject\">제출자</label></th>"))
                _result = true;
            else
                _result = false;

        }

    }
}

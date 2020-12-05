using System;
using hanbat_project.Strategy;

namespace hanbat_project.Class
{
    public class connectAssignment
    {

        String courseId, reportId;

        public connectAssignment(String courseId, String reportId)
        {
            this.courseId = courseId;
            this.reportId = reportId;
        }

        public void Method()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MReport.do");

            String postData = "courseDTO.courseId=" + courseId +
                "&gubun=A&cmd=viewReportSubmitForm&userType=learner&searchTxt=" +
                "&reportInfoDTO.reportInfoId=" + reportId + "&reportInfoDTO.randomYn=N&boardInfoDTO.boardInfoGubun=report" +
                "&reportSubmitDTO.submitId=" + LoginForm.loginForm.customTextbox1.val  + "&appGubun=";

            setPost setpost = new setPost();
            setpost.method(new setHttpProtocol(_uri, postData));

        }

    }

}

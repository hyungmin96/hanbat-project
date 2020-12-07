using hanbat_project.Class;
using hanbat_project.Strategy;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hanbat_project.Template
{
    public class ExistFile
    {

        String _courseId, _reportId;

        String _SeqId, _fileName, _fileId, _offset, _fileList;

        public bool _result;

        public ExistFile(String _courseId, String _reportId)
        {
            this._courseId = _courseId;
            this._reportId = _reportId;
        }

        public void run()
        {
            connectUri();
            up_or_none_File();
            submit();
        }

        public virtual void connectUri()
        {
            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MReport.do");

            String postData = "courseDTO.courseId=" + _courseId +
                "&gubun=A&cmd=viewReportSubmitForm&userType=learner&searchTxt=" +
                "&reportInfoDTO.reportInfoId=" + _reportId + "&reportInfoDTO.randomYn=N&boardInfoDTO.boardInfoGubun=report" +
                "&reportSubmitDTO.submitId=REPT_201204232133a4590479&appGubun=";

            setPost setpost = new setPost();
            setpost.method(new setHttpProtocol(_uri, postData));



        }

        public virtual void up_or_none_File()
        {

            string html = "";

            String path = AssignmentForm.assignmentForm.customTextbox1.val;
            //String path = @"C:\Users\bana_\OneDrive\바탕 화면\3-2 학기\데이터베이스 프로그래밍\11주차\#11 데이터베이스_20187097_변형민.hwp";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "http://cyber.hanbat.ac.kr/FileUpload.do?cmd=multiUploadFile&moduleName=COMF&path=/business/report/" +
                "" + _courseId + "/" + _reportId + "&osType=windows");

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length); fs.Close();

            Stream DataStream = new MemoryStream();
            string boundary = "-----------------------------36932931913641";
            string postData = boundary + "\r\nContent-Disposition: form-data; name=\"repository\"\r\n\r\nFORUM";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"organization\"\r\n\r\nORG0000001";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"type\"\r\n\r\nfile";
            postData += "\r\n" + boundary + "\r\nContent-Disposition: form-data; name=\"file\"; filename=\"" + Path.GetFileName(path) + "\"\r\nContent-Type: application/haansofthwp\r\n\r\n";

            string footer = "\r\n-----------------------------36932931913641--\r\n";

            DataStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
            DataStream.Write(data, 0, data.Length);
            DataStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, 2);
            DataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));

            DataStream.Position = 0;
            byte[] formData = new byte[DataStream.Length];
            DataStream.Read(formData, 0, formData.Length); DataStream.Close();

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=---------------------------36932931913641";
            request.Referer = "http://cyber.hanbat.ac.kr/Report.do";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
            request.ContentLength = formData.Length;
            request.CookieContainer = Singleton.getInstance().getCookie();

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream resStream = response.GetResponseStream())
            {
                StreamReader readStream = new StreamReader(resStream);
                html = readStream.ReadToEnd();
            }

            _SeqId = Regex.Split(Regex.Split(html, "fileSn\":\"")[1], "\"")[0];
            _fileName = Path.GetFileName(path);
            _fileId = Regex.Split(Regex.Split(html, "maskFileName\":\"")[1], "\"")[0];
            _offset = Regex.Split(Regex.Split(html, "filesize\":")[1], ",")[0];
            _fileList = Regex.Split(Regex.Split(html, "")[1], "")[0];

        }

        public virtual void submit()
        {

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/Report.do");

            String _postData = "courseId=" + _courseId + "" +
                "&courseDTO.courseId=" + _courseId + "" +
                "&courseDTO.shareCourseYn=N&courseDTO.shareCourseId=&courseDTO.shareCourseItem=" +
                "&reportInfoDTO.reportInfoId=" + _reportId + "" +
                "&reportSubmitDTO.reportSubmitId=&reportInfoDTO.maxPostSize=500&reportSubmitDTO.fileNum=0&reportInfoDTO.teamCategoryId=&boardInfoDTO.boardInfoGubun=report&gubun=A&editGubun=&mainDTO.parentMenuId=menu_00104&mainDTO.menuId=menu_00063" +
                "&userId=" + LoginForm.loginForm.customTextbox1.val + "" +
                "&reportSubmitDTO.submitNum=0" +
                "&uploadSeqIds=" + _SeqId + "" +
                "&uploadFileNames=" + _fileName + "" +
                "&uploadFileIds=" + _fileId + "" +
                "&uploadFileSizes=" + _offset  + 
                "&deleteFileList=" + _fileList  + "&boardInfoDTO.boardClass=bbs&reportInfoDTO.teamReportYn=N&cmd=addReportSubmit&reportSubmitDTO.reportSummary=" + AssignmentForm.assignmentForm.customRichTextBox2.setValue + "&atchuploader=";


            setPost setpost = new setPost();
            setpost.method(new setHttpProtocol(_uri, _postData));
            if (setpost._html.Contains("제출완료"))
            {
                MessageBox.Show(_fileName + "\n 파일을 업로드하였습니다.");
                _result = true;
            }
            else
                _result = false;
        }

    }
}

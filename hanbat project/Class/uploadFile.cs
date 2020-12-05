using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Class
{
    class uploadFile
    {

        public void uploadMethod()
        {

            //String path = AssignmentForm.assignmentForm.customTextbox1.val;
            String path = @"C:\Users\82105\Downloads\#11 데이터베이스_20187097_변형민.hwp";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "http://cyber.hanbat.ac.kr/FileUpload.do?cmd=multiUploadFile&moduleName=COMF&path=/business/report/" +
                "H020382002003201204853021/REPT_201119155717c7295688&osType=windows");

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
                string result = readStream.ReadToEnd();
            }

        }
    }
}

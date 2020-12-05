using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.dataClass
{
    public class AssignmentData
    {

        String courseId, title, content, date, reportUri, f_name, uri;

        public AssignmentData(String courseId, String title, String content, String date, String reportUri, String f_name, String uri)
        {
            this.courseId = courseId;
            this.title = title;
            this.content = content;
            this.date = date;
            this.reportUri = reportUri;
            this.f_name = f_name;
            this.uri = uri;
        }

        public String _courseId
        {
            get { return courseId; }
            set { courseId = value; }
        }

        public String _title
        {
            get { return title; }
            set { title = value; }
        }

        public String _content
        {
            get { return content; }
            set { content = value; }
        }

        public String _date
        {
            get { return date; }
            set { date = value; }
        }
        public String _f_name
        {
            get { return f_name; }
            set { f_name = value; }
        }      

        public String _reportUri
        {
            get { return reportUri; }
            set { reportUri = value; }
        }

        public String _uri
        {
            get { return uri; }
            set { uri = value; }
        }

    }

}

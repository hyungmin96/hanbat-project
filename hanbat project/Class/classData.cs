﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Class
{
    public class classData
    {

        String type, ClassName, professor, assignment, datestr, uri;
        int num;

        public classData()
        {

        }

        public String _type
        {
            get { return type; }
            set { type = value; }
        }

        public String _uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public String _ClassName
        {
            get { return ClassName; }
            set { ClassName = value; }
        }
        public String _professor
        {
            get { return professor; }
            set { professor = value; }
        }
        public String _assignment
        {
            get { return assignment; }
            set { assignment = value; }
        }

        public String _datestr
        {
            get { return datestr; }
            set { datestr = value; }
        }

        public int _num
        {
            get { return num; }
            set { num = value; }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Strategy
{
    public class setHttpProtocol
    {

        public String _postData { get; private set; }

        public Uri _uri { get; private set; }

        public bool _result { get; private set; }

        public String _query { get; private set; }

        public setHttpProtocol(Uri _uri, String _postData = "", bool _result = false, String _query = "")
        {
            this._uri = _uri;
            this._result = _result;
            this._postData = _postData;
            this._query = _query;
        }

    }
}

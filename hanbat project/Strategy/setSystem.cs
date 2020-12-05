using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Strategy
{
    public class setSystem
    {

        private httpMethod _strategy;
        public bool _result;

        public void setFomular(httpMethod _strategy)
        {
            this._strategy = _strategy;
        }

        public void methodExecute()
        {
            _strategy.method();
        }

    }

}

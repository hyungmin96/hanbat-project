using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Strategy
{
    public class Context
    {

        public StrategyClass _strategy;

        public Context(StrategyClass _strategy)
        {
            this._strategy = _strategy;
        }

        public void methodExecute()
        {
            _strategy.method();
        }

    }

}

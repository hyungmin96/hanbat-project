using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Observer
{
    public abstract class o_method
    {

        private List<o_dictionary> _observers = new List<o_dictionary>();

        public void add(Observer o)
        {
            _observers.Add((o_dictionary)o);
        }

        public void clear()
        {
            _observers.Clear();
        }

        public void notify()
        {
            foreach(o_dictionary o in _observers)
                o.Update();
        }

    }


}

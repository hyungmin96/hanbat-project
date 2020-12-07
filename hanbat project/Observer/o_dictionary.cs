using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanbat_project.Observer
{
    public class o_dictionary : Observer
    {

        public String _keyName { get; set; }
        public int _index { get; set; }

        public o_concrete set_o { get; set;}

        public o_dictionary(o_concrete set_o, String _keyName, int _index)
        {
            this.set_o = set_o;
            this._keyName = _keyName;
            this._index = _index;
        }

        public override void Update()
        {
            Facade.getAssignment._dict[_keyName].RemoveAt(_index);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Account
    {
        public Account(string name)
        {
            Name = name;

        }

        //// Fields...

        private int _ID;
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }


        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }
        }

    }
}

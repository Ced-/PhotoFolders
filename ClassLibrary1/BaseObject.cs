using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISBusinessLayer;

namespace ISBusinessLayer
{
    public abstract class BaseObject
    {
        internal System.Guid _ID;
        internal Type objectType;
        //internal Guid ID;
       // internal DataClasses1DataContext startObj;
        internal bool managementAccess=false;
        internal bool doubleValue = false;
        internal List<object> hierachy;


        public bool hasManagementAccess
        {
            get
            {
    
                return managementAccess;
            }

            internal set
            {
                managementAccess = value;
            }
        }


        public bool isDouble
        {
            get
            {
                return doubleValue;
            }

            internal set
            {
                doubleValue = value;
            }
        }


        public String ID
        {
            get
            {
                return this._ID.ToString();
            }
            internal set
            {
                try
                {
                    _ID = new Guid(value);
                }
                catch (Exception e)
                {
                    throw new Exception("7");
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBusinessLayer
{
    public class ISOrder : BaseObject
        //!!INTERNAL CONSTRUCTOR!!!
    {

          internal ISOrder()
        { }
#region normalVariables
        internal ISImage _Image;
        internal ISProductType _ProductType;
        internal ISUser _User;
        internal int _counter;

#endregion

#region PROPERTIES
        public ISImage Image
        {
            get
            {
                return this._Image;
            }
            internal set
            {
                _Image = value;
            }
        }

        public ISProductType ProductType
        {
            get
            {
                return this._ProductType;
            }
            internal set
            {
                _ProductType = value;
            }
        }

        public ISUser User
        {
            get
            {
                return this._User;
            }
            internal set
            {
                _User = value;
            }
        }

        public int counter
        {
            get
            {
                return _counter;
            }
            set
            {
                 _counter=value;
            }
        }


        public float DerivedAmountPrice
        {
            get
            {
                return (this.ProductType.Price * _counter);
            }
        }

        
#endregion


#region KONSTRUKTOR
public ISOrder(ISImage image, ISProductType productType, ISUser user)
{
     ProductType = productType;
     Image = image;
     ID = Guid.NewGuid().ToString();
     User = user;
     _counter = 1;
}
#endregion

#region DELETE
public void delete()
{
    this.User.removeOrder(this);
}
#endregion


        /*
public int getCounter()
    {
        return _counter;
    }

public void setCounter(int newCounter)
    {
        _counter=newCounter;
    }
        */
    }
}

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
//using ISBusinessLayer.ConnectionClasses;
using System.Collections.ObjectModel;

namespace ISBusinessLayer
{

    public partial class ISUser : BaseObject
    {
          internal ISUser()
        { }


        




          #region normalVariables
          //  internal System.Guid _ID;

        internal string _Username;

        internal string _Password;

        internal string _Firstname;

        internal string _Lastname;

        internal string _Address1;

        internal string _Address2;

        internal string _Address3;

        internal string _Email;

        internal int _PermissionLevel;


        internal List<ISOrder> IntOrders=new List<ISOrder>();
          #endregion


        #region PROPERTIES

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "Password", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Password = value;
            }
        }
   

public int PermissionLevel
        {
            get
            {
                return this._PermissionLevel;
            }
            internal set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                if (value != 0 && value != 1)
                {
                    throw new Exception("29");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "PermissionLevel", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _PermissionLevel = value;
            }
        }
   

public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "Email", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Email = value;
            }
        }


public string Lastname
        {
            get
            {
                return this._Lastname;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "Lastname", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Lastname = value;
            }
        }


public string Firstname
        {
            get
            {
                return this._Firstname;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "Firstname", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Firstname = value;
            }
        }


public string Address1
{
    get
    {
        return this._Address1;
    }
    set
    {
        if (!this.hasManagementAccess)
        {
            throw new Exception("8");
        }
        try
        {
            BaseFunctions.updateMethod<ISUser>(this, "Address1", value.ToString());
        }
        catch (Exception e)
        {
            throw e;
        }
        _Address1 = value;
    }
}


public string Address2
{
    get
    {
        return this._Address2;
    }
    set
    {
        if (!this.hasManagementAccess)
        {
            throw new Exception("8");
        }
        try
        {
            BaseFunctions.updateMethod<ISUser>(this, "Address2", value.ToString());
        }
        catch (Exception e)
        {
            throw e;
        }
        _Address2 = value;
    }
}


public string Address3
{
    get
    {
        return this._Address3;
    }
    set
    {
        if (!this.hasManagementAccess)
        {
            throw new Exception("8");
        }
        try
        {
            BaseFunctions.updateMethod<ISUser>(this, "Address3", value.ToString());
        }
        catch (Exception e)
        {
            throw e;
        }
        _Address3 = value;
    }
}

public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISUser>(this, "Username", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Username = value;
            }
        }


public int DerivedOrderCount
{
    get
    {
        int retval = 0;
        foreach (ISOrder order in this.IntOrders)
        {
            retval += order.counter;
        }
        return retval;
    }
}
public float DerivedOrderSum
{
    get
    {
        float retval = 0;
        foreach (ISOrder order in this.IntOrders)
        {
            retval += order.DerivedAmountPrice;
        }
        return retval;
    }
}









internal int UserFolderCount
{
    get
    {
        try
        {
            ReadOnlyCollection<ISFolder> tempFolders = new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISUser, ISFolder>(this, 1, null));
            return tempFolders.Count();
        }
        catch (Exception e)
        {
            if (e.Message == "0")
            {
                return 0;
            }
            else
            {
                throw e;
            }
        }
    }
}


#endregion



#region LISTS
        public ReadOnlyCollection<ISFolder> Folders
        {
            get
            {
                try
                {
                    return new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISUser, ISFolder>(this, 1,null,"OR FolderType='0'"));
                }
                catch (Exception e)
                {
                    if (e.Message == "0")
                    {
                        return new ReadOnlyCollection<ISFolder>(new List<ISFolder>());
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }





        public ReadOnlyCollection<ISOrder> Orders
        {
            get
            {
                ReadOnlyCollection<ISOrder> retCollection = new ReadOnlyCollection<ISOrder>(IntOrders);
              
                  if(retCollection.Count==0)
                    {    
                       return new ReadOnlyCollection<ISOrder>(new List<ISOrder>());
                    }
               
                  else
                    {
                        return retCollection;
                    }
              }
            }
        


#endregion



 #region DELETE
        public bool delete()
        {
            if (!this.hasManagementAccess)
            {
                throw new Exception("8");
            }
            //foreach (ISImage i in this.Images)

            try
            {
                this.removeAllFolders();
            }
            catch (Exception e)
            {
                throw new Exception("11");
            }


            try
            {
                BaseFunctions.deleteMethod<ISUser>(this);
            }
            catch (Exception e)
            {
                throw new Exception("12");
            }
            return true;
        }
#endregion


#region ELEMENTSBYID

        public ISFolder Folder(String id)
        {
            ISFolder retObj = null;
            try
            {
                retObj = BaseFunctions.selectMethod<ISUser, ISFolder>(this, 1, id, "OR FolderType=0")[0];
            }
            catch (Exception e)
            {
                throw new Exception("10");
            }
            return retObj;
        }

        public ISOrder Order(String id)
        {
            ISOrder retObj = null;
            try
            {
                for (int i = 0; i < IntOrders.Count; i++)
                {
                    if (IntOrders[i].ID == id)
                    {
                        //IntOrders.RemoveAt(i);
                        retObj = IntOrders[i];
                        return retObj;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("19");
            }
            throw new Exception("19");
        }

#endregion

 #region FOLDERS

        public bool addFolder(ISFolder folder)
        {
            if (!this.hasManagementAccess)
            {
                throw new Exception("8");
            }

            try
            {
                BaseFunctions.selectMethod<ISUser, ISFolder>(this, 1, folder.ID);
            }
            catch (Exception ex)
            {
                if (ex.Message == "0")
                {
                    try
                    {
                        BaseFunctions.insertJoin<ISUser, ISFolder>(this, folder, 2);
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
           
            return false;
        }

        public bool removeFolder(ISFolder folder)
        {
            if (!this.hasManagementAccess)
            {
                throw new Exception("8");
            }

            try
            {
                BaseFunctions.deleteJoin<ISUser, ISFolder>(this, folder, 2);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }


#endregion


#region ORDERS

        public bool addOrder(ISProductType producttype, ISImage image)
        {
            foreach (ISOrder ordr in this.Orders)
            {
                if (ordr._ProductType.ID == producttype.ID && ordr._Image.ID == image.ID)
                {
                    ordr.counter++;
                    return true;
                }
            }

            try
            {
                ISOrder tmpOrder = new ISOrder(image, producttype, this);
                IntOrders.Add(tmpOrder);
            }
            catch (Exception e)
            {
                throw new Exception("17");
            }
            return true;
        }

        public bool removeOrder(ISOrder order)
        {  
            try
            {
                IntOrders.Remove(order);
            }
            catch (Exception e)
            {
                throw new Exception("18");
            }
            return true;
        }


        #endregion


#region removeAllFunctions
        public bool removeAllFolders()
        {
            if (!this.hasManagementAccess)
            {
                throw new Exception("8");
            }
            while (this.UserFolderCount>0)
            {
                try
                {
                    removeFolder(this.Folders[0]);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return true;
        }


        public bool removeAllOrders()
        {
                try
                {
                    IntOrders.RemoveRange(0, IntOrders.Count);
                }
                catch (Exception e)
                {
                    throw new Exception("18");
                }
           return true;
        }

 #endregion



        #region sendOrders
        public bool sendOrders(float additionalCosts, String additionalCostsText)
        {
            if (this.DerivedOrderCount>0)
            {
                string sendTextCopy;
                string sendText="<html>\n<head>\n<title>Neue Bestellung</title></head><body\n><h1> Eine neue Bestellung wurde über den ImageShop versandt</h1>\n<br><br>";
                sendText+="Besteller:  " + this.Firstname + " " + this.Lastname  + " - Email-Adresse: " + this.Email + " - Zustelladresse: " + this.Address1 + " , " + this.Address2 + " , " + this.Address3;
                sendText+="<br><br>";
                sendTextCopy=sendText;
                try
                {
                    string additionalText;
                    foreach(ISOrder singleOrder in this.Orders)
                    {
                        sendText+="<img src='" + singleOrder.Image.getImageInOriginalResolution() + "'><br>";
                        sendTextCopy+="<img src='" + singleOrder.Image.getImageInResolution(new ISResolution(208,117)) + "'><br>";
                        additionalText="<b>Produkt:</b> " + singleOrder.ProductType.Name + " <b>Stückzahl:</b> " + singleOrder.counter + " <b>Einzelpreis:</b> " + singleOrder.ProductType.Price + " <b>Gesamtpreis:</b> " + singleOrder.DerivedAmountPrice;
                        additionalText+="<br><br>";
                        sendText+=additionalText;
                        sendTextCopy+=additionalText;
                    }
                    additionalText="<br><b>Zusätzliche Kosten (" + additionalCostsText + "):</b> " + additionalCosts.ToString();
                    additionalText+="<br><b>Gesamtsumme:</b> " + (this.DerivedOrderSum + additionalCosts).ToString();
                    sendText+=additionalText;
                    sendTextCopy+=additionalText;
                }
                catch (Exception ex)
                {
                   throw new Exception("33");
                }
                try
                {
                    BaseFunctions.sendMail(sendText, "Neue Bestellung im ImageShop");
                    BaseFunctions.sendMail(sendTextCopy, "Neue Bestellung im ImageShop", this.Email);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
            else
            {
                throw new Exception("33");
            }
        }

        #endregion
    }
}

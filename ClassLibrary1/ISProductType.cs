using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
//using ISBusinessLayer.ConnectionClasses;

namespace ISBusinessLayer
{
    public partial class ISProductType : BaseObject
    {
          internal ISProductType()
        { }

        // internal System.Guid _ID;

#region normalVariables
        internal string _Name;

        internal float _Price;
#endregion

#region PROPERTIES
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                try
                {
                    BaseFunctions.updateMethod<ISProductType>(this, "Name", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Name = value;
            }
        }



        public float Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                if (!this.hasManagementAccess)
                {
                    throw new Exception("8");
                }
                if (value < 0)
                {
                    throw new Exception("28");
                }
                try
                {
                    BaseFunctions.updateMethod<ISProductType>(this, "Price", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Price = value;
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
                    return new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISProductType, ISFolder>(this, 2));
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
                BaseFunctions.deleteMethod<ISProductType>(this);
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
                retObj = BaseFunctions.selectMethod<ISProductType, ISFolder>(this, 2, id)[0];
            }
            catch (Exception e)
            {
                throw new Exception("10");
            }
            return retObj;
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
                BaseFunctions.selectMethod<ISProductType, ISFolder>(this, 2, folder.ID);
            }
            catch (Exception ex)
            {
                if (ex.Message == "0")
                {
                    try
                    {
                        BaseFunctions.insertJoin<ISProductType, ISFolder>(this, folder, 1);
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
                BaseFunctions.deleteJoin<ISProductType, ISFolder>(this, folder, 1);
            }
            catch (Exception e)
            {
                throw e;
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
            while (this.Folders.Count>0)
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


        #endregion


    }
}

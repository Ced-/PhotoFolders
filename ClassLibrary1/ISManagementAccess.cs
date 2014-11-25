using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISBusinessLayer
{
    public class ISManagementAccess:BaseObject
    {

        public ISManagementAccess(ISUser user)
        {
            if (user.PermissionLevel != 1)
            {
                throw new Exception("20");
            }
            this.hasManagementAccess = true;
            
        }
        #region normalVariables
        internal ISImage _Image;
        internal ISProductType _ProductType;
        internal ISUser _User;
        internal int _counter;

        #endregion


        #region USERS
        public ISUser getUserByID(string id)
        {
            try
            {
                return BaseFunctions.selectMethod<ISManagementAccess, ISUser>(this,5,id, "AND Username != 'admin' AND Username != 'public'")[0]; 
            }
            catch (Exception e)
            {
                if (e.Message=="0")
                {
                    return null;
                }
                else
                {
                    throw e;
                }
            }
        }

        public ReadOnlyCollection<ISUser> getAllUsers()
        {
            try
            {
                return new ReadOnlyCollection<ISUser>(BaseFunctions.selectMethod<ISManagementAccess, ISUser>(this, 5, null, "AND Username != 'admin' AND Username != 'public'"));
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return new ReadOnlyCollection<ISUser>(new List<ISUser>());
                }
                else
                {
                    throw e;
                }
            }
        }


        public ISUser createUser(String Username, String Password, String Firstname, String Lastname, String EMail, String Address1, String Address2, String Address3, int Permissionlevel)
        {


            if (Permissionlevel != 0 && Permissionlevel != 1)
            {
                throw new Exception("29");
            }
            try
            {
                List<ISUser> il = BaseFunctions.selectMethod<ISManagementAccess, ISUser>(this, 5, null, "AND Username='" + Username + "'");
            }
            catch (Exception ex)
            {
                if (ex.Message == "0")
                {
                    try
                    {
                        return BaseFunctions.insertMethod<ISUser>(Username, Password, Firstname, Lastname, EMail, Address1, Address2, Address3, Permissionlevel);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            throw new Exception("23");
        }
        #endregion

        #region FOLDERS
        public ISFolder getFolderByID(string id)
        {
            try
            {
                return BaseFunctions.selectMethod<ISManagementAccess, ISFolder>(this, 5, id)[0];
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return null;
                }
                else
                {
                    throw e;
                }
            }
        }

        public ReadOnlyCollection<ISFolder> getAllFolders()
        {
            try
            {
                return new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISManagementAccess, ISFolder>(this, 5, null));
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

        public ReadOnlyCollection<ISFolder> getAllPublicFolders()
        {
            try
            {
                return new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISManagementAccess, ISFolder>(this, 5, null,"AND FolderType='0'"));
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

        public ReadOnlyCollection<ISFolder> getAllNonPublicFolders()
        {
            try
            {
                return new ReadOnlyCollection<ISFolder>(BaseFunctions.selectMethod<ISManagementAccess, ISFolder>(this, 5, null, "AND FolderType='1'"));
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


        public ISFolder createFolder(String Foldername, int FolderType)
        {
            if (FolderType != 0 && FolderType != 1)
            {
                throw new Exception("30");
            }
            try
            {
                return BaseFunctions.insertMethod<ISFolder>(Foldername, FolderType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
 #endregion

        #region PRODUCTTYPES
        public ISProductType getProductTypeByID(string id)
        {
            try
            {
                return BaseFunctions.selectMethod<ISManagementAccess, ISProductType>(this, 5, id)[0];
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return null;
                }
                else
                {
                    throw e;
                }
            }
        }

        public ReadOnlyCollection<ISProductType> getAllProductTypes()
        {
            try
            {
                return new ReadOnlyCollection<ISProductType>(BaseFunctions.selectMethod<ISManagementAccess, ISProductType>(this, 5, null));
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return new ReadOnlyCollection<ISProductType>(new List<ISProductType>());
                }
                else
                {
                    throw e;
                }
            }
        }


        public ISProductType createProductType(String Name, float Price)
        {
            if (Price < 0)
            {
                throw new Exception("28");
            }

            try
            {
                return BaseFunctions.insertMethod<ISProductType>(Name,Price);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region RESOLUTIONS
        public ISResolution getResolutionByID(string id)
        {
            try
            {
                return BaseFunctions.selectMethod<ISManagementAccess, ISResolution>(this, 5, id)[0];
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return null;
                }
                else
                {
                    throw e;
                }
            }
        }

        public ReadOnlyCollection<ISResolution> getAllResolutions()
        {
            try
            {
                return new ReadOnlyCollection<ISResolution>(BaseFunctions.selectMethod<ISManagementAccess, ISResolution>(this, 5, null));
            }
            catch (Exception e)
            {
                if (e.Message == "0")
                {
                    return new ReadOnlyCollection<ISResolution>(new List<ISResolution>());
                }
                else
                {
                    throw e;
                }
            }
        }


        public ISResolution createResolution(int Height, int Width)
        {
            if (Height <= 0)
            {
                throw new Exception("28");
            }
            if (Width <= 0)
            {
                throw new Exception("28");
            }
            try
            {
                return BaseFunctions.insertMethod<ISResolution>(Height, Width);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion


        /*
ReadOnlyCollection<ISFolder getAllUsers();
ReadOnlyCollection<ISFolder> ISManagementAccess.getFolderByID(id);
ISFolder ISManagementAccess.getAllFolders;
ReadOnlyCollection<ISProductType>
ISManagementAccess.getProductTypeByID(id);
ISProductType ISManagementAccess.getAllProductTypes;
ReadOnlyCollection<ISResolution> ISManagementAccess.getProductTypeByID(id);
ISResolution ISManagementAccess.getAllResolutions;
ISFolder ISManagementAccess.createFolder(String FolderName, int FolderType);
[für int Foldertype gilt: 1 macht einen normalen Folder, jeder andere Wert erzeugt einen public Folder]
ISProductType ISManagementAccess.createProductype(String ProductTypeName);
ISResolution ISManagementAccess.createResolution(String ResolutionName);
ISUser ISManagementAccess.createUser(String Username, String EMail, String
FirstName, String LastName, String Password, optional int PermissionLevel=0);
*/    }
}

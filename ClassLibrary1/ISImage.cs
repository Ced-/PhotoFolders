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
using System.Drawing;
using System.IO;

//using ISBusinessLayer.ConnectionClasses;


namespace ISBusinessLayer
{
    public partial class ISImage : BaseObject
    {
          internal ISImage()
          { }
          #region normalVariables



         
       // internal System.Guid _ID;

        internal string _Name;

        internal System.Guid _ISFolderID;
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
                    BaseFunctions.updateMethod<ISImage>(this, "Name", value.ToString());
                }
                catch (Exception e)
                {
                    throw e;
                }
                _Name = value;
            }
        }

        public int OriginalWidth
        {
            get
            {
                Image i;
                String openImage;
                if (this.ID.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    openImage=BaseFunctions.storePath + "default.png";
                }
                else
                {
                    openImage=BaseFunctions.storePath + this.ID;
                }
                try
                {
                    i = Image.FromFile(openImage);
                    int retVal;
                    if (i.Width > i.Height)
                    {
                        retVal=i.Width;
                    }
                    else
                    {
                        retVal=i.Height;
                    }
                    i.Dispose();
                    return retVal;
                 }

                 catch (Exception ex)
                 {
                     throw new Exception("34");
                 }
            }
        }


        public int OriginalHeight
        {
            get
            {
                Image i;
                String openImage;
                if (this.ID.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    openImage=BaseFunctions.storePath + "default.png";
                }
                else
                {
                    openImage=BaseFunctions.storePath + this.ID;
                }
                try
                {
                    i = Image.FromFile(openImage);
                    int retVal;
                    if (i.Width > i.Height)
                    {
                        retVal=i.Height;
                    }
                    else
                    {
                        retVal=i.Width;
                    }
                    i.Dispose();
                    return retVal;
                 }

                 catch (Exception ex)
                 {
                     throw new Exception("34");
                 }
            }
        }


        public ISFolder Folder
        {
            get
            {
                ISFolder retObj = null;
                try
                {
                    retObj = BaseFunctions.selectMethod<ISImage, ISFolder>(this, 4)[0];
                }
                catch (Exception e)
                {
                    throw new Exception("10");
                }
                return retObj;
            }
        }


       /* public string FolderID
        {
            get
            {
                return this._ISFolderID.ToString();
            }
            internal set
            {
                try
                {
                    _ISFolderID = new Guid(value);
                }
                catch (Exception e)
                {
                    throw new Exception("7");
                }
            }
        }*/
        #endregion


        #region DELETE
        public bool delete()
        {
            if (BaseFunctions.isDemo) throw new Exception("5000");
            if (!this.hasManagementAccess)
            {
                throw new Exception("8");
            }
            //foreach (ISImage i in this.Images)



            try
            {
               /* string path;
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = new Uri(path).LocalPath;*/
                File.Delete(BaseFunctions.storePath + this.ID);
               
            }
            catch (Exception ex)
            { }

            try
            {
                BaseFunctions.deleteMethod<ISImage>(this);
            }
            catch (Exception e)
            {
                throw new Exception("12");
            }
            return true;
        }
        #endregion


 #region GETIMAGEINRESOLUTION

        public String getImageInOriginalResolution()
        {
            try
            {
                ISResolution res=new ISResolution(this.OriginalWidth, this.OriginalHeight);
                return getImageInResolution(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String getImageInResolution(ISResolution imageRes)
        {
          /*  string path;
            path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = new Uri(path).LocalPath;*/
            
            
            Image i;
            String openImage;
            if (this.ID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                openImage=BaseFunctions.storePath + "default.png";
            }
            else
            {
                openImage=BaseFunctions.storePath + this.ID;
            }
                try
                {
                    Bitmap a;
                    i = Image.FromFile(openImage);
                    if (i.Width >= i.Height)
                    {
                         a = new Bitmap(i, new Size(imageRes.Width, imageRes.Height));
                    }
                    else
                    {
                         a = new Bitmap(i, new Size(imageRes.Height, imageRes.Width));
                    }
                      String retString="data:image/png;base64," + Convert.ToBase64String(BaseFunctions.imageToByteArray(a));
                      a.Dispose();
                      i.Dispose();
                      return retString;
                }
                catch (Exception ex)
                {
                    return "";
                }
     
      //   return "";
        }
#endregion


    }
}

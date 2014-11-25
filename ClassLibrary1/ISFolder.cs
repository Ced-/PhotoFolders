	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
    using System.Drawing;

  //  using ISBusinessLayer.ConnectionClasses;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
namespace ISBusinessLayer
{
    public partial class ISFolder : BaseObject
    {

        internal ISFolder()
        { }
        // internal System.Guid _ID;

#region normalVariables
        internal string _Foldername;

        internal int _FolderType;
#endregion
        
#region PROPERTIES
        public string Foldername
        {
            get
            {
                return this._Foldername;
            }
            set
            {
                  if (!this.hasManagementAccess)
                  {
                         throw new Exception("8");
                  }
                  try
                  {
                      BaseFunctions.updateMethod<ISFolder>(this, "Foldername", value.ToString());
                  }
                  catch (Exception e)
                  {
                      throw e;
                  }
                  _Foldername=value;
            }
        }

        
         
        public int FolderType
        {
            get
            {
                return this._FolderType;
            }
            internal set
            {
                  if (!this.hasManagementAccess)
                  {
                         throw new Exception("8");
                  }
                  if (value != 0 && value != 1)
                  {
                      throw new Exception("30");
                  }
                  try
                  {
                      BaseFunctions.updateMethod<ISFolder>(this, "FolderType", value.ToString());
                  }
                  catch (Exception e)
                  {
                      throw e;
                  }
                  _FolderType = value;
            }
        }


        public int DerivedImageCount
        {
            get
            {
                try
                {

                    if (this.Images.Count == 1 && this.Images[0].ID == "00000000-0000-0000-0000-000000000000")
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                { }
                return Images.Count;
            }
        }

#endregion

#region LISTS
        public ReadOnlyCollection <ISUser> Users
        {
            get
            {
                try
                {
                    return new ReadOnlyCollection<ISUser>(BaseFunctions.selectMethod<ISFolder,ISUser>(this,2));
                }
                catch (Exception e)
                {
                    if (e.Message=="0")
                    {
                     return new ReadOnlyCollection<ISUser>(new List<ISUser>());
                    }
                    else
                    {
                      throw e;
                    }
                }
            }
        }


        public ReadOnlyCollection <ISProductType> ProductTypes
        {
            get
            {
                try
                {
                    return new ReadOnlyCollection<ISProductType>(BaseFunctions.selectMethod<ISFolder,ISProductType>(this,1));
                }
               catch (Exception e)
                {
                    if (e.Message=="0")
                    {
                     return new ReadOnlyCollection<ISProductType>(new List<ISProductType>());
                    }
                    else
                    {
                      throw e;
                    }
                }
            }
        }

        public ReadOnlyCollection <ISResolution> Resolutions
        {
            get
            {
                try
                {
                    return new ReadOnlyCollection<ISResolution>(BaseFunctions.selectMethod<ISFolder,ISResolution>(this,1));
                }
                 catch (Exception e)
                {
                    if (e.Message=="0")
                    {
                      return new ReadOnlyCollection<ISResolution>(new List<ISResolution>());
                    }
                    else
                    {
                      throw e;
                    }
                }
            }
        }

        public ReadOnlyCollection <ISImage> Images
        {
            get
            {
                try
                {
                    return new ReadOnlyCollection<ISImage>(BaseFunctions.selectMethod<ISFolder,ISImage>(this,3));
                }
                catch (Exception e)
                {
                    if (e.Message=="0")
                    {
                        List<ISImage> tmpRetList = new List<ISImage>();
                        ISImage tmpISImage=new ISImage();
                        tmpISImage._ID=new Guid("00000000-0000-0000-0000-000000000000");
                        tmpRetList.Add(tmpISImage);
                        return new ReadOnlyCollection<ISImage>(tmpRetList);
                     //return null;
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
            this.removeAllImages();
        }
        catch (Exception e)
        {
            throw new Exception("11");
        }
   

   
        try
        {
            this.removeAllUsers();
        }
        catch (Exception e)
        {
            throw new Exception("11");
        }
    


 
        try
        {
            this.removeAllResolutions();
        }
        catch (Exception e)
        {
            throw new Exception("11");
        }
    



        try
        {
            this.removeAllProductTypes();
        }
        catch (Exception e)
        {
            throw new Exception("11");
        }
    

        try
        {
            BaseFunctions.deleteMethod<ISFolder>(this);   
        }
        catch (Exception e)
        {
             throw new Exception("12");
        }
 return true;     
}
#endregion

#region ELEMENTSBYID

    public ISUser User(String id)
{

    ISUser retObj=null;
    try
    {
        retObj = BaseFunctions.selectMethod<ISFolder, ISUser>(this, 2, id)[0];
    }
    catch (Exception e)
    {
        throw new Exception ("10");
    }
    return retObj;
}

public ISProductType ProductType(String id)
{
    ISProductType retObj=null;
    try
    {
        retObj = BaseFunctions.selectMethod<ISFolder, ISProductType>(this, 1, id)[0];
    }
    catch (Exception e)
    {
        throw new Exception ("10");
    }
    return retObj;
}

public ISResolution Resolution(String id)
{
    ISResolution retObj = null;
    try
    {
        retObj = BaseFunctions.selectMethod<ISFolder, ISResolution>(this, 1, id)[0];
    }
    catch (Exception e)
    {
        throw new Exception("10");
    }
    return retObj;
}


public ISImage Image(String id)
{
    ISImage retObj = null;
    try
    {
        retObj = BaseFunctions.selectMethod<ISFolder, ISImage>(this, 3, id)[0];
    }
    catch (Exception e)
    {
        throw new Exception("10");
    }
    return retObj;
}
#endregion

#region RESOLUTIONS

public bool addResolution(ISResolution resolution)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.selectMethod<ISFolder, ISResolution>(this, 1, resolution.ID);
    }
    catch (Exception ex)
    {
        if (ex.Message == "0")
        {
            try
            {
                BaseFunctions.insertJoin<ISFolder, ISResolution>(this, resolution, 2);
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

public bool removeResolution(ISResolution resolution)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.deleteJoin<ISFolder, ISResolution>(this, resolution, 2);
    }
    catch (Exception e)
    {
        throw e;
    }
    return true;
}


#endregion

#region PRODUCTTYPES

public bool addProductType(ISProductType productType)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.selectMethod<ISFolder, ISProductType>(this, 1, productType.ID);
    }
    catch (Exception ex)
    {
        if (ex.Message == "0")
        {
            try
            {
                BaseFunctions.insertJoin<ISFolder, ISProductType>(this, productType, 2);
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

public bool removeProductType(ISProductType productType)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.deleteJoin<ISFolder, ISProductType>(this, productType, 2);
    }
    catch (Exception e)
    {
        throw e;
    }
    return true;
}


#endregion

#region USER

public bool addUser(ISUser user)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.selectMethod<ISFolder, ISUser>(this, 2, user.ID);
    }
    catch (Exception ex)
    {
        if (ex.Message == "0")
        {
            try
            {
                BaseFunctions.insertJoin<ISFolder, ISUser>(this, user, 1);
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

public bool removeUser(ISUser user)
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    try
    {
        BaseFunctions.deleteJoin<ISFolder, ISUser>(this, user, 1);
    }
    catch (Exception e)
    {
        throw e;
    }
    return true;
}

        
#endregion

#region IMAGES



public ISImage attachImage(byte[] ImageData, string Name)
{
    if (BaseFunctions.isDemo) throw new Exception("5000");
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }

    ISImage retImg = null;
    Image img;
    try
    {
       img= BaseFunctions.byteArrayToImage(ImageData);
    }
    catch (Exception ex)
    {
        throw new Exception("25");
    }

    try
    {
        retImg=internalAttachImage(Name, this.ID); 
    }
    catch (Exception e)
    {
        throw e;
    }
    if (retImg == null)
    {
        throw new Exception("9");
    }
    else
    {
        try
        {
            BaseFunctions.saveImage(img, retImg);
            return retImg;
        }
        catch (Exception e)
        {
            try
            {
                retImg.delete();
            }
            catch (Exception ex)
            {
                throw new Exception("15");
            }
            throw new Exception("14");
        }
    }

}

internal ISImage internalAttachImage(String Name, String ISFolderID)
{
    try
    {
        return BaseFunctions.insertMethod<ISImage>(Name, ISFolderID);
    }   
    catch (Exception e)
    {
        throw e;
    }
}

public bool removeImage(ISImage image)
{
    if (BaseFunctions.isDemo) throw new Exception("5000");
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }
    if (image.ID != "00000000-0000-0000-0000-000000000000")
    {
        try
        {
            image.delete();          //deleteJoin<ISFolder, ISUser>(this, user, 1);
        }
        catch (Exception e)
        {
            throw new Exception("13");
        }
    }
    return true;
}
#endregion


#region removeAllFunctions
public bool removeAllUsers()
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }
    while (this.Users.Count>0) 
    {
        try
        {
     
            removeUser(this.Users[0]);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    return true;
}


public bool removeAllResolutions()
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }
    while (this.Resolutions.Count>0)
    {
        try
        {
            removeResolution(this.Resolutions[0]);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    return true;
}



public bool removeAllProductTypes()
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }
    while (this.ProductTypes.Count>0)
    {
        try
        {
            removeProductType(this.ProductTypes[0]);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    return true;
}


public bool removeAllImages()
{
    if (!this.hasManagementAccess)
    {
        throw new Exception("8");
    }
    while (this.Images.Count>0)
    {
        if (this.Images[0].ID == "00000000-0000-0000-0000-000000000000")
        {
            break;
        }
        try
        {
            removeImage(this.Images[0]);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    return true;
}

#endregion




public byte[] getZIPDownload(ISResolution requestedResulotion)
{
    /*send download: http://stackoverflow.com/questions/2239623/download-file-from-webservice-in-asp-net-site */

    String openImage;
    String RandomPath = BaseFunctions.storePath + Guid.NewGuid().ToString();
    DirectoryInfo di = System.IO.Directory.CreateDirectory(RandomPath);
    int k = 0;
    foreach (ISImage singleImage in this.Images)
    {
        k++;
        openImage = BaseFunctions.storePath + singleImage.ID;
        try
        {
            Bitmap a;
            System.Drawing.Image i = System.Drawing.Image.FromFile(openImage);
            if (i.Width >= i.Height)
            {
                a = new Bitmap(i, new Size(requestedResulotion.Width, requestedResulotion.Height));
            }
            else
            {
                a = new Bitmap(i, new Size(requestedResulotion.Height, requestedResulotion.Width));
            }
            i.Dispose();
            a.Save(RandomPath + "\\(" + k.ToString() + ")" + singleImage.Name.Substring(0,singleImage.Name.Length-4) + ".png", System.Drawing.Imaging.ImageFormat.Png);
            a.Dispose();

        }
        catch (Exception ex)
        {
            throw new Exception("38");
        }
    }

    try
    {
  
        System.IO.Compression.ZipFile.CreateFromDirectory(RandomPath, RandomPath + ".zip");
        byte[] bt = File.ReadAllBytes(RandomPath + ".zip");
        File.Delete(RandomPath + ".zip");
        Directory.Delete(RandomPath, true);
        return bt;
    }
    catch (Exception ex)
    {
        throw new Exception("39");
    }
    /* byte[] imageData=BaseFunctions.imageToByteArray(a);
       File.WriteAllBytes(RandomPath, imageData);*/
}






        /*   String getZIPDownloadPath(ISResolution), */



    }

}
using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ISUser user;
        ISFolder currFolder;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user = (ISUser)Session["User"];
                currFolder = user.Folder((string)Session["myFolderID"]);
                string currImageID = (String)Request.QueryString["reqImageID"];
                if (Request.QueryString["productType"] == null)
                {
                    int requestedHeight = Convert.ToInt32(Request.QueryString["reqHeight"]);
                    int requestedWidth = Convert.ToInt32(Request.QueryString["reqWidth"]);
                    string retImage = currFolder.Image(currImageID).getImageInResolution(new ISResolution(requestedWidth, requestedHeight));
                    Response.Write(retImage);
                }
                else
                {
                    string productType = (String)Request.QueryString["productType"];
                    ISProductType pt = currFolder.ProductType(productType);
                    ISImage ig= currFolder.Image(currImageID);
                    user.addOrder(pt, ig);
                    Response.Write("(" + user.DerivedOrderCount + ")");
                }
            }
            catch(Exception ex)
            {
                 Response.Write("");
            }
           
        }
    }
}
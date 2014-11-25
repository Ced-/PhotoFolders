using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String resID = (String)Session["myResolutionID"];
            String folderID = (String)Session["myFolderID"];
            ISUser user = (ISUser)Session["User"];
            try
            {
                byte[] sendBytes = user.Folder(folderID).getZIPDownload(user.Folder(folderID).Resolution(resID));
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + user.Folder(folderID).Foldername + ".zip\"");
                Response.AddHeader("Content-Length", sendBytes.Length.ToString());
                Response.OutputStream.Write(sendBytes, 0, sendBytes.Length);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
            Response.Redirect("/" + BaseFunctions.basePath + "ufolderview.aspx");
        }
    }
}
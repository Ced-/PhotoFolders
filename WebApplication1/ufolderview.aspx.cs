using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{



public partial class ufolderview : System.Web.UI.Page
{
    ISUser user;
    protected void Page_Load(object sender, EventArgs e)
    {

        /**IMMER: Wenn Session["User"]==null --> Umleitung auf login.aspx */
        if (Session["User"] == null)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
        }

        try
        {
            //Aktuellen Usernamen im Header anzeigen
            user = (ISUser)Session["User"];
           
            string curr_username = user.Username;
            user_lbl.Text = curr_username;
            ReadOnlyCollection<ISFolder> myFolders = user.Folders;

            //Warenkorb: ANzahl der Waren ermitteln und Anzeigen
            int cart_amount;
            if (user.Orders.Count == 0)
            {
                cart_amount = 0;
            }
            else
            {
                cart_amount = user.DerivedOrderCount;
            }

            warenkorb_lbl.Text += "(" + cart_amount + ")";


            if (!IsPostBack)
            {
                ListView1.DataSource = myFolders;
                ListView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            error_div.Visible = true;
            error_meldung.Text=ex.Message;
        }
     }

        protected void ansehen_btn_Click(object sender, EventArgs e)
        {
            ImageButton ansehen_btn = (ImageButton)sender;
            String albumID = ansehen_btn.Attributes["AlbumID"];

            Session["myFolderID"] = albumID;
            Response.Redirect("/" + BaseFunctions.basePath + "/uimageview.aspx");
        }

        /*ÜBERALL REINKOPIEREN!!!!!!!*/
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            if (user.Username == "admin")
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/folderview.aspx");
            }
            else
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/ufolderview.aspx");
            }
        }

        protected void ausloggen_btn_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("User");
            Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
        }

        protected void ok_btn_Click(object sender, EventArgs e)
        {
            error_div.Visible = false;

        }

    /*ÜBERALL REINKOPIEREN!!!!!!!*/

    }
}

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


public partial class folderview : System.Web.UI.Page
{
    ISUser user;
    ISManagementAccess newMA;
        protected void Page_Load(object sender, EventArgs e)
        {

            /**IMMER: Wenn Session["User"]==null --> Umleitung auf login.aspx */
            if (Session["User"] == null)
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
            }

            /* BEI MANAGEMNENT SEITEN ZUNÄCHST IMMER MAL EIN MANAGEMENTACCESS OBJEKT ERZEUGEN, BEVOR WAS ANDERES GEMACHT WIRD.
              * GEHT DAS NICHT (TRY/CATCH) -> UMLEITUNG AUF LOGIN!!*/
            try
            {
                user = (ISUser)Session["User"];
                newMA = new ISManagementAccess(user);
            }
            catch (Exception ex)
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
            }
            //--------------Folder-----------------------------

            try
            {
                ReadOnlyCollection<ISFolder> myFolders = newMA.getAllFolders();
                if (!IsPostBack)
                {
                    ListView1.DataSource = myFolders;
                    ListView1.DataBind();
                }
                string curr_username = user.Username;
                user_lbl.Text = curr_username;
            }
            catch (Exception ex)
            {
                error_meldung.Text = ex.Message;
                error_div.Visible = true;
            
            }
            user_lbl.Text = user.Username;
      

            //Aktuellen Usernamen im Header anzeigen
     



        }

        /*protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ListView1.SelectedIndex;
            Session["albumID"] = id;
            Response.Redirect("/" + BaseFunctions.basePath + "/imageview.aspx");
        }*/

      


        //----------------Buttons der Albenansicht--------------------------------------------------
        protected void edit_btn_Click(object sender, EventArgs e)
        {
            ImageButton tempImageButton = (ImageButton)sender;
            string id = tempImageButton.Attributes["FolderID"];
            Session["myFolderID"] = id;
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/imageview.aspx");
            
        }


        protected void saveFoldername_btn_Click(object sender, EventArgs e)
        {
            Button tempButton = (Button)sender;
            string id = tempButton.Attributes["FolderID"];
            string listElement = tempButton.ClientID.Substring("ListView1_lbn_".Length);
            TextBox myTextBox = (TextBox)this.ListView1.Items[Convert.ToInt32(listElement)].FindControl("ltb");
            //FindControl("ListView1_lbn_" + listElement);
            string newFoldername = myTextBox.Text;

                try
                {
                    newMA.getFolderByID(id).Foldername = newFoldername;
                }
                catch (Exception ex)
                {
                    error_meldung.Text = ex.Message;
                    error_div.Visible = true;
                }
            
        }

        protected void deleteFolder_btn_Click(object sender, ImageClickEventArgs e)
        {
           ImageButton folderDel=(ImageButton)sender;
           string folderID_del = folderDel.Attributes["FolderID"];
           try
           {
               newMA.getFolderByID(folderID_del).delete();
               Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/folderview.aspx");
           }
           catch (Exception ex)
           {
               error_meldung.Text = ex.Message;
               error_div.Visible = true;
           }
        
           /*ReadOnlyCollection<ISFolder> delFolder = ISManagementAccess.getFolderByID(folderID_del);
           delete(delFolder);*/
        }

        //------------------Steuerkonsole-----------------------------------------------
        protected void manageUsers_btn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/UserMan/userManagement.aspx");
        }


        protected void addFolder_btn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/addFolder.aspx");
        }


        protected void productType_btn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/ProductMan/categoryManagement.aspx");
        }

        protected void resolution_btn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/ResMan/resolutionManagement.aspx");
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
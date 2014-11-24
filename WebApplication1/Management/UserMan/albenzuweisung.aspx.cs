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
    public partial class albenzuweisung : System.Web.UI.Page
  {
      String selected_user;

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
                selected_user = (String)Session["selectedUserID"];
                user_lbl.Text = newMA.getUserByID(selected_user).Username;
              
            }

            catch (Exception ex)
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
            }

            try
            {
                ReadOnlyCollection<ISFolder> allFolders=newMA.getAllNonPublicFolders();
                ReadOnlyCollection<ISFolder> usersFolders=newMA.getUserByID(selected_user).Folders;
                ReadOnlyCollection<ISFolder> checkedList=BaseFunctions.getDoubleEntries<ISFolder>(allFolders,usersFolders);
                if (!IsPostBack)
                {
                    SimpleCheckBoxList1.DataSource = checkedList;
                    SimpleCheckBoxList1.DataBind();
                }
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;

            }
            username_lbl.Text = user.Username;
            
         }

         protected void save_btn_Click(object sender, ImageClickEventArgs e)
         {
             try
             {
                 foreach (ListItem i in SimpleCheckBoxList1.Items)
                 {
                     if (i.Selected)
                     {
                         newMA.getUserByID(selected_user).addFolder(newMA.getFolderByID(i.Value));
                     }
                     else
                     {
                         newMA.getUserByID(selected_user).removeFolder(newMA.getFolderByID(i.Value));
                     }
                 }
                 Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/folderview.aspx");

             }
             catch (Exception ex)
             {
                 error_div.Visible = true;
                 error_meldung.Text = ex.Message;
             }

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

        protected void back_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/" + BaseFunctions.basePath + "userManagement.aspx");
        }

         }
    }

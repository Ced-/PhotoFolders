using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{
    public partial class userDetails : System.Web.UI.Page
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

            user_lbl.Text = user.Username;


            try
            {
                if (!IsPostBack)
                {
                  
                   Username.Text = newMA.getUserByID((String)Session["selectedUserID"]).Username;
                   Firstname.Text = newMA.getUserByID((String)Session["selectedUserID"]).Firstname;
                   Lastname.Text = newMA.getUserByID((String)Session["selectedUserID"]).Lastname;
                   Password.Text = newMA.getUserByID((String)Session["selectedUserID"]).Password;
                   EMail.Text = newMA.getUserByID((String)Session["selectedUserID"]).Email;
                   Address1.Text = newMA.getUserByID((String)Session["selectedUserID"]).Address1;
                   Address2.Text = newMA.getUserByID((String)Session["selectedUserID"]).Address2;
                   Address3.Text = newMA.getUserByID((String)Session["selectedUserID"]).Address3;
                }
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
            }

        }



        protected void save(object sender, ImageClickEventArgs e)
        {
            try
            {
                ISUser changeUser = newMA.getUserByID((String)Session["selectedUserID"]);

                changeUser.Username= Username.Text;
                changeUser.Firstname = Firstname.Text;
                changeUser.Lastname = Lastname.Text;
                changeUser.Password = Password.Text;
                changeUser.Email = EMail.Text;
                changeUser.Address1 = Address1.Text;
                changeUser.Address2 = Address2.Text;
                changeUser.Address3 = Address3.Text;

                Response.Redirect("/" + BaseFunctions.basePath + "/Management/UserMan/userManagement.aspx");
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


        protected void back_btn_Click(object sender, EventArgs e){
            Response.Redirect("/" + BaseFunctions.basePath + "userManagement.aspx");
        }
    }
}
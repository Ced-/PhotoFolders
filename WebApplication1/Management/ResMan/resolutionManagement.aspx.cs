﻿using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{
    public partial class resolutionManagement : System.Web.UI.Page
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
            try
            {
                if (!IsPostBack)
                {

                    ReadOnlyCollection<ISResolution> allRes = newMA.getAllResolutions();
                    res_list.DataSource = allRes;
                    res_list.DataBind();
                }
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
         
            }
            user_lbl.Text = user.Username;
        }

     

        protected void edit_btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            String ResID = btn.Attributes["ResID"];
            Session["ResID"] = ResID;
            Response.Redirect("/" + BaseFunctions.basePath + "/Management/ResMan/resolutionEdit.aspx");
        }

        protected void delete_btn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            String ResID = btn.Attributes["ResID"];
            try
            {
                ISResolution delRes = newMA.getResolutionByID(ResID);
                delRes.delete();
                Response.Redirect("/" + BaseFunctions.basePath + "/Management/ResMan/resolutionManagement.aspx");
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

    }
}
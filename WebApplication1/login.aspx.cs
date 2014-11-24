using ISBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{



public partial class login : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            demoshow.Visible = BaseFunctions.isDemo;
        }

        protected void Unnamed3_Click(object sender, ImageClickEventArgs e)
        {
            //Überprüft ob username und passwort passen, wenn ja in Session speichern
            string user=username.Text;
            string password = passwort.Text;
            try
            {
                ISUser myUser = BaseFunctions.login(user, password);
                Session["User"] = myUser;

                if (user == "admin") {
                    Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/folderview.aspx");
                } else Response.Redirect("/" + BaseFunctions.basePath + "/ufolderview.aspx");
               
                
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                //wenn fehler, error_div anzeigen und fehlermeldung ausgeben
                //error_div = "true";
                error_meldung.Text = ex.Message;
               //  Response.Redirect("/" + BaseFunctions.basePath + "/ufolderview.aspx");
            }

        }



        /*ÜBERALL REINKOPIEREN!!!!!!!*/


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
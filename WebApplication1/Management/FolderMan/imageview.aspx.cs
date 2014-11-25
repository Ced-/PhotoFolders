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



public partial class imageview : System.Web.UI.Page
{
    ISUser user;
    String myFolderID;
    ISManagementAccess newMA;
    ISFolder currFolder;

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


            //Aktuellen Usernamen im Header anzeigen
        try
        {
            string curr_username = user.Username;
            username_lbl.Text = curr_username;

            //Albumtitel in der Überschrift
            myFolderID = (String)Session["myFolderID"];
            currFolder = newMA.getFolderByID(myFolderID);
            albumtitel_lbl.Text = currFolder.Foldername;
            if (!IsPostBack)
            {

                //fotos des aktuellen albums laden
                ReadOnlyCollection<ISImage> currImages = currFolder.Images;
                images_listview.DataSource = currImages;
                images_listview.DataBind();


                //resolution des aktuellen albums laden
                /*ReadOnlyCollection<ISResolution> Session["myFolderID"].Resolutions;

                resolutions.Text = Eval("getResolution()");
                resolutions.Text = Session["myFolderID"].Resolutions
                resolutions.DataSource = ;
                resolutions.DataBind(); */

                //Anzahl Fotos
                int amount = currFolder.DerivedImageCount;
                anzahlFotos_lbl.Text = amount.ToString();

                //Alle Auflösungen und Produktkategorien laden


                ReadOnlyCollection<ISResolution> all_res = newMA.getAllResolutions();


                ReadOnlyCollection<ISResolution> currFolder_res = currFolder.Resolutions;

                ReadOnlyCollection<ISResolution> res_double = BaseFunctions.getDoubleEntries<ISResolution>(all_res, currFolder_res);
                SimpleCheckBoxList1.DataSource = res_double;
                SimpleCheckBoxList1.DataBind();


                //Alle ProductTypes des albums laden
                ReadOnlyCollection<ISProductType> allTypes = newMA.getAllProductTypes();
                ReadOnlyCollection<ISProductType> currFolder_types = currFolder.ProductTypes;

                ReadOnlyCollection<ISProductType> types_double = BaseFunctions.getDoubleEntries<ISProductType>(allTypes, currFolder_types);
                SimpleCheckBoxList2.DataSource = types_double;
                SimpleCheckBoxList2.DataBind();

            }
         }
        catch (Exception ex)
        {
            error_meldung.Text = ex.Message;
            error_div.Visible = true;
        }
    }



        protected void addImage_btn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                currFolder.attachImage(FileUpload1.FileBytes, FileUpload1.FileName);
                Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/imageview.aspx");
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

        protected void speichern_btn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                
                foreach (ListItem i in SimpleCheckBoxList1.Items)
                {
                    if (i.Selected)
                    {
                        currFolder.addResolution(newMA.getResolutionByID(i.Value));
                    }
                    else
                    {
                        currFolder.removeResolution(newMA.getResolutionByID(i.Value));
                    }
                }
                foreach (ListItem i in SimpleCheckBoxList2.Items)
                {
                    if (i.Selected)
                    {
                        currFolder.addProductType(newMA.getProductTypeByID(i.Value));
                    }
                    else
                    {
                        currFolder.removeProductType(newMA.getProductTypeByID(i.Value));
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

        protected void delImage_btn_Click(object sender, EventArgs e)
        {
            Button delBtn = (Button)sender;
            String delImageID = delBtn.Attributes["ImageID"];

            try
            {
                currFolder.Image(delImageID).delete();
                Response.Redirect("/" + BaseFunctions.basePath + "/Management/FolderMan/imageview.aspx");
            }
            catch (Exception ex)
            {
                error_meldung.Text = ex.Message;
                error_div.Visible = true;
            }

       

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


    }
}
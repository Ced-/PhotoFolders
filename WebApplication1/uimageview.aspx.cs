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



public partial class uimageview : System.Web.UI.Page
{
    ISUser user;
    String myFolderID;
    ISFolder currFolder;
    String enlargedImageID;
    ISImage currImage;



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


            int cart_amount;
            if (user.Orders.Count == 0)
            {
                cart_amount = 0;
            }
            else
            {
                cart_amount = user.DerivedOrderCount;
            }

            warenkorb_lbl.Text = "(" + cart_amount + ")";

            if (!IsPostBack)
            {
                //Albumtitel in der Überschrift
                currFolder = user.Folder((string)Session["myFolderID"]);
                albumtitel_lbl.Text = currFolder.Foldername;



                //fotos des aktuellen albums laden
                ReadOnlyCollection<ISImage> currImages = currFolder.Images;
                images_listview.DataSource = currImages;
                images_listview.DataBind();
                /*for (int i = 0; i < currImages.Count; i++)
                {
                    Response.Write("<tr><td>");
                    Response.Write("<img src=");
                    Response.Write(currImages[i]);
                    Response.Write("</td></tr>");
                }*/

                //resolution des aktuellen albums laden
                /*ReadOnlyCollection<ISResolution> Session["myFolderID"].Resolutions;

                resolutions.Text = Eval("getResolution()");
                resolutions.Text = Session["myFolderID"].Resolutions
                resolutions.DataSource = ;
                resolutions.DataBind(); */

                //Anzahl Fotos
                int amount = currFolder.DerivedImageCount;
                anzahlFotos_lbl.Text += amount;

                ReadOnlyCollection<ISResolution> curr_resolutions = currFolder.Resolutions;
                resolutions.DataSource = curr_resolutions;
                resolutions.DataBind();

                ReadOnlyCollection<ISProductType> curr_types = currFolder.ProductTypes;
                productType_view.DataSource = curr_types;
                productType_view.DataBind();
            }
        }
        catch (Exception ex)
        {
            error_div.Visible = true;
            error_meldung.Text = ex.Message;
        }

        }
    

        protected void images_imgbtn_Click(object sender, ImageClickEventArgs e)
        {
            //string z=ImageButton1.Attributes["ImageID"];
            ImageButton t = (ImageButton)sender;
            string imageID= t.Attributes["ImageID"];
            Session["imageID"] = imageID;
            //FUNCTION NOT USED!?
        }
        /*
                protected void image_update(object sender, EventArgs e)
                {
                    try
                    {
                        //Aktuellen Usernamen im Header anzeigen
                        user = (ISUser)Session["User"];
                        currFolder = user.Folder((string)Session["myFolderID"]);
                        string imagedata=currFolder.Image(labelID.Text).getImageInResolution(new ISResolution(Convert.ToInt32(labelWidth.Text), Convert.ToInt32(labelHeight.Text)));
                        imageLabel.Text = "<img src='" + imagedata + "'>";
                        //Aktuellen Usernamen im Header anzeigen

                        int cart_amount;
                        if (user.Orders.Count == 0)
                        {
                            cart_amount = 0;
                        }
                        else
                        {
                            cart_amount = user.DerivedOrderCount;
                        }

                        warenkorb_lbl.Text = "(" + cart_amount + ")";
                    }
                    catch (Exception ex)
                    {
                        imageLabel.Text = "";
                    }
                }*/
        /*  protected void enlargeImage_Click(object sender, ImageClickEventArgs e)
          {
              try
              {

                  ImageButton image = (ImageButton)sender;
                  enlargedImageID = image.Attributes["ImageID"];

                  //richtiges Image anzeigen
                  singleImage.Visible = true;
                  imagePlaceholder.ImageUrl = currFolder.Image(enlargedImageID).getImageInResolution(new ISResolution(400,400));

                  //dazugehörige ProductTypes anzeigen
                  ReadOnlyCollection<ISProductType> currFolderProd = user.Folder(myFolderID).ProductTypes;
                  productType_view.DataSource = currFolderProd;
                  productType_view.DataBind();
              }
              catch (Exception ex)
              {
                  error_div.Visible = true;
                  error_meldung.Text=ex.Message;
              }

          }*/

        protected void addToCart_Click(object sender, EventArgs e)
        {
           /* Button add = (Button)sender;
            String addTypeID = add.Attributes["TypeID"];

            String currImageID = labelID.Text;

            try
            {
                ISImage currImage = currFolder.Image(currImageID);
                ISProductType Type = (ISProductType)user.Folder(myFolderID).ProductType(addTypeID);
                user.addOrder(Type, currImage);
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
            }*/
           
           }


        protected void download_btn_Click(object sender, ImageClickEventArgs e)
        {
            Session["myResolutionID"] = resolutions.SelectedValue;
            Response.Redirect("/" + BaseFunctions.basePath + "/Download.aspx");
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
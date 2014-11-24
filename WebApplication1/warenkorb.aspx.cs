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
    public partial class warenkorb : System.Web.UI.Page
    {
      ISUser user;
      double count;
        protected void Page_Load(object sender, EventArgs e)
        {

            /**IMMER: Wenn Session["User"]==null --> Umleitung auf login.aspx */
            if (Session["User"] == null)
            {
                Response.Redirect("/" + BaseFunctions.basePath + "/login.aspx");
            }

            //Aktuellen Usernamen im Header anzeigen
            try
            {
                user = (ISUser)Session["User"];
                string curr_username = user.Username;
                username_lbl.Text = curr_username;
                firstname_txtbox.Text = user.Firstname + " " + user.Lastname;
                address_txtbox.Text = user.Address1 + " " + user.Address2 + " " + user.Address3;
                //Orders
                ReadOnlyCollection<ISOrder> userOrders = user.Orders;
                if (!IsPostBack)
                {
                    orders_listview.DataSource = userOrders;
                    orders_listview.DataBind();
                }
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
                return;
            }
            try
            {

                //OrderID herausfinden des geklickten buttons
              //  ImageButton OrderIDbtn = (ImageButton)sender;
                

               


            //Wenn aktualisierenbtn geklickt, preis des producttypes auslesen, (zahl)aus inputfeld lesen zusammenrechnen
            //preis_lbl setzen
        
         /*   float singlePrice = order.ProductType.Price;

            float preis = amount * singlePrice;

            preis_lbl.Text = preis;*/

              float summe = user.DerivedOrderSum;
              summe_lbl.Text = Convert.ToString(summe);
              versand.Text = RadioButtonList1.SelectedValue + ",00 €";
              int versandkosten = Convert.ToInt32(RadioButtonList1.SelectedValue);
              float gesamtsumme = summe+versandkosten;
              gesamtsumme_lbl.Text = Convert.ToString(gesamtsumme);
            }

            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
                return;
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

        protected void refresh_btn_Click(object sender, ImageClickEventArgs e) {
           
            int amount=0;
            ImageButton tempImageButton = (ImageButton)sender;
            //string id = tempImageButton.Attributes["OrderID"];
            string listElement = tempImageButton.ClientID.Substring("orders_listview_ctrl1_rbn_".Length);
            TextBox myTextBox = (TextBox)this.orders_listview.Items[Convert.ToInt32(listElement)].FindControl("rtb");
            //FindControl("ListView1_lbn_" + listElement);
           // string newAmount = myTextBox.Text;
          //  string id = tempImageButton.Attributes["OrderID"];
            try
            {
                amount = Convert.ToInt32(myTextBox.Text);
            }
            catch (Exception ex)
            {

                error_meldung.Text = "42";
                return;
            }

            try
            {

                //OrderID herausfinden des geklickten buttons
              //  ImageButton OrderIDbtn = (ImageButton)sender;
                String OrderID = (String)tempImageButton.Attributes["OrderID"];

                ISOrder order = user.Order(OrderID);

                //löschen wenn amount=0
                if (amount == 0)
                {
                    order.delete();
                }
                else
                {
                    order.counter = amount;
                }

   


            //Wenn aktualisierenbtn geklickt, preis des producttypes auslesen, (zahl)aus inputfeld lesen zusammenrechnen
            //preis_lbl setzen
        
         /*   float singlePrice = order.ProductType.Price;

            float preis = amount * singlePrice;

            preis_lbl.Text = preis;*/

             
              Response.Redirect("/" + BaseFunctions.basePath + "/warenkorb.aspx");

            }
           
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
            }

        }

        protected void order_btn_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                float versandkosten = Convert.ToSingle(RadioButtonList1.SelectedValue);
                user.sendOrders(versandkosten, "Versandkosten");
                user.removeAllOrders();
                Response.Redirect("/" + BaseFunctions.basePath + "/ufolderview.aspx");
            }
            catch (Exception ex)
            {
                error_div.Visible = true;
                error_meldung.Text = ex.Message;
            }
        }
    }
}



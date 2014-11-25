using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ISPresentationLayer
{
    public partial class userFolderAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /*ÜBERALL REINKOPIEREN!!!!!!!*/
        protected void ausloggen_btn_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("User");
            Response.Redirect("login.aspx");
        }


        protected void ok_btn_Click(object sender, EventArgs e)
        {
            error_div.Visible = false;
        }


        /*ÜBERALL REINKOPIEREN!!!!!!!*/

    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Markfed_PDY : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {
            lblColl_DIO.Text = "State LogIn";
            LblName.Text = Session["st_Name"].ToString();
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}

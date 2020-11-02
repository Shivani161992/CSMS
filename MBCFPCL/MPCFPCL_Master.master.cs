using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBCFPCL_MPCFPCL_Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (Session["DistID"] != null)
        {
            DateTime today = DateTime.Today;
            lbldate.Text = Convert.ToString(today.ToString("dd/MM/yyyy"));

            lblDistName.Text = Session["DistName"].ToString();
        }
        else
        {
            Response.Redirect("~/MBCFPCL/MBCFPCL_Login.aspx");
        }
    }
}

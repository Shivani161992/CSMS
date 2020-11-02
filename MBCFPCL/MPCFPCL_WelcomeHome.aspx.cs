using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MBCFPCL_MPCFPCL_WelcomeHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["DistID"] != null)
        {
            string samepassword = Session["NotchangePassword"].ToString();
            if (samepassword == "MBC123")
            {
                Response.Redirect("~/MBCFPCL/MBCFPCL_ChangePassword.aspx");
            }
            else if (samepassword != "MBC123")
            {

            }


        }
        else
        {
            Response.Redirect("~/MBCFPCL/MBCFPCL_Login.aspx");
        }
    }
}
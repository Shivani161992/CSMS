using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Millers_Login_Miller_LoginHomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BttLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Millers_Login/Miller_LoginHomePage.aspx");
    }
    protected void bttRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Miller_Registration/Miller_registration.aspx");
    }
}
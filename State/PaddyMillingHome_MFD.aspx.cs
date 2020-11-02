using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class State_PaddyMillingHome_MFD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["st_Name"] != null)
        {

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}
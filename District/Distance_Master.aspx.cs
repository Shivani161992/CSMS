using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Doorstep_Selection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void rdDist_to_IC_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/basedepo_toIC.aspx");
    }
    protected void rd_ICto_FPS_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/IssueCenterto_FPS.aspx");
    }
}
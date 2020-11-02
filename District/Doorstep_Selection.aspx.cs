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

    protected void rdprabhari_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Dwarpraday_prabhari.aspx");
    }
    protected void rdtrans_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Doorstep_Transporter.aspx");
    }
    protected void rdlink_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/LinkCooperativesociety.aspx");
    }
    protected void rd_TO_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/DPY_TransportOrder.aspx");
    }
    protected void rddistmstr_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Distance_Master.aspx");
    }
    protected void drroutechart_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Routechart_master.aspx");
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/Print_TO.aspx");
    }
    protected void rdfpsSuspend_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/District/SuspendedFPS.aspx");
    }
}
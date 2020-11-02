using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_PDSMORoadStatusHome_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    protected void btnSendDist_Click(object sender, EventArgs e)
    {
        Session["MODist"] = "Send";
        Response.Redirect("~/ReportForms_District/PDSMORoadStatus_Rpt.aspx");
    }
    protected void btnRecdDist_Click(object sender, EventArgs e)
    {
        Session["MODist"] = "Recd";
        Response.Redirect("~/ReportForms_District/PDSMORoadStatus_Rpt.aspx");
    }
    protected void btnDivDist_Click(object sender, EventArgs e)
    {
        //Session["MODist"] = "Recd";
        Response.Redirect("~/Reports_State/PDS_Diverted_District.aspx");
    }
}
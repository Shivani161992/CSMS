using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IssueCenter_frm_printpages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void rdprintac_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Reprint_AcceptanceNote.aspx");
    }
    protected void rdprintwhr_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Reprint_Whr_request.aspx");
    }
    protected void rdprintpaddy_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Reprint_AcceptanceNote.aspx");
    }
    protected void rdprintpaddyrecipt_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/mpsc_gatepass_page.aspx");
    }
    protected void rdprintRej_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/Reprint_PartialRejection.aspx");

    }
    protected void rdprintrecp_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/mpsc_gatepass_page.aspx");
    }

    protected void rdbReceiptKharif2016_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/RePrintReceipt_Kharif2016.aspx");
    }
    protected void rdbAcptKharif2016_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/RePrintAcpt_Kharif2016.aspx");
    }
    protected void rdbdepositerKharif2016_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/RePrintDepositer_Kharif2016.aspx");
    }
    protected void rdbPartialRejection_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/RePrintPartial_Kharif2016.aspx");
    }
}

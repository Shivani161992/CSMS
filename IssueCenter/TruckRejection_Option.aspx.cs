using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class IssueCenter_TruckRejection_Option : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (!IsPostBack)
            {
                string opid = Session["OperatorId"].ToString();

                if (opid == "BM" || opid == "DMO")
                {
                    rdPartialPdy2016.Visible = rdFullPdy2016.Visible = true;
                }
                else
                {
                    rdPartialPdy2016.Visible = rdFullPdy2016.Visible = false;
                }
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    protected void rdpartial_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Partial_Rejection.aspx");
    }
    protected void rdfull_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Issue_RejectionNote.aspx");
    }

    protected void rdpartial_15_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Partial_rejection_Lastyear.aspx");
    }
    protected void rdfull_15_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Issue_RejectionNote_lastyear.aspx");
    }

    protected void rdPartialPdy2016_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Partial_Rejection_Pdy2016.aspx");
    }
    protected void rdFullPdy2016_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("Full_Rejection_Pdy2016.aspx");
    }
}

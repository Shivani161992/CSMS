using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class IssueCenter_DOTypes_Selection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void rdIssue_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("issueagainst_do.aspx");
    }
    protected void rdDist_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("IssueAgainst_OpenSales_DO.aspx");
    }

    //rdDPY_CheckedChanged

    protected void rdDPY_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("DPY-IssueAgainstDo.aspx");
    }
}

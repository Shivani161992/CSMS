using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IssueCenter_PaddyMillingHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            if (Session["Markfed"].ToString() == "Y")
            {
                li_Recpt.Visible = li_ReRecpt.Visible = RCMRDO.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}
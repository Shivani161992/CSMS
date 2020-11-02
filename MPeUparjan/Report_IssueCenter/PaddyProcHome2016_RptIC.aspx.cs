using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_IssueCenter_PaddyProcHome2016_RptIC : System.Web.UI.Page
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
}
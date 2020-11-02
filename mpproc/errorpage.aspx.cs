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

public partial class errorpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Ag_Name"] == null || Session["Ag_id"] == null || Session["Mark_Seas"] == null || Session["Markseas_id"] == null || Session["dist_id"] == null || Session["dist_name"] == null || Session["cropyear"] == null || Session["pc_name"] == null || Session["pcId"] == null)
        {
            Response.Redirect("sessionexpired.aspx");
        }
        else
        {


            Label1.Text = "Error Occured....";
        
        }

    }
}

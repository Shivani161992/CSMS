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

public partial class GenericErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Session["App"] != null)
        {
            if (Session["App"].ToString() == "WPMS")
            {
                Response.Redirect("~/WPMS/MainPage.aspx");
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }
        }
        else if (Session["App_test"] != null)
        {
            if (Session["App_test"].ToString() == "test_WPMS")
            {
                Response.Redirect("~/test_WPMS/MainPage.aspx");
            }
            else
            {
                Response.Redirect("~/MainLogin.aspx");
            }

        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

    }
}

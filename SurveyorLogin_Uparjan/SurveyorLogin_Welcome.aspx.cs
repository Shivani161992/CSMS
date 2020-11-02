using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyorLogin_Uparjan_SurveyorLogin_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            if (!IsPostBack)
            {
               
            }
        }
        else
        {
            Response.Redirect("~/SurveyorLogin_Uparjan/Surveyor_Login.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CSMSSurveyorLogin_CSMS_SurveyorLogin_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userGodown"] != null)
        {
            if (!IsPostBack)
            {
                string samepassword = Session["NotchangePassword"].ToString();
                if (samepassword == "SMSSUR")
                {
                    Response.Redirect("~/CSMSSurveyorLogin/GodownSurveyor_ChangePassword.aspx");
                }
                else if (samepassword != "SMSSUR") 
                {
                
                }

            }
        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CSMSSurveyorLogin_Godown_SurveyorMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime today = DateTime.Today;
        lbldate.Text = Convert.ToString(today.ToString("dd/MM/yyyy"));
        lblsurveyorname.Text = Session["SurveyorName"].ToString();
    }
}

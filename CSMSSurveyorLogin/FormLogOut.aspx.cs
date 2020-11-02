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
using Data;
using DataAccess;
using System.Data.SqlClient;


public partial class CSMSSurveyorLogin_FormLogOut : System.Web.UI.Page
{
    string UserName = "";

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userGodown"] != null)
        {
            UserName = Session["userGodown"].ToString();
         
            SqlConnection.ClearAllPools();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");

        }
        else
        {
            Response.Redirect("~/CSMSSurveyorLogin/SurveyorLogin_Godown.aspx");
        }
    }

   
}
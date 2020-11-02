using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MasterPage_AgencyMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] == null)
        {
            Response.Redirect("../sessionexpired.aspx");
        
        }


       if (Session["Ag_Name"] == null || Session["Ag_id"] == null || Session["Mark_Seas"] == null || Session["Markseas_id"] == null || Session["dist_id"] == null || Session["dist_name"] == null || Session["cropyear"] == null || Session["pc_name"] == null || Session["pcId"] == null)
        {
            Response.Redirect("../sessionexpired.aspx");
        }
   

        lbl_logInfo.Text = "Agency:" + Session["Ag_Name"].ToString() + "||" + "District:" + Session["dist_name"].ToString() + "||" + "Purchase Center:" + Session["pc_name"].ToString();

      
    }
    
    protected void LinkButton_et_allooc_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Procurement/allocationEstimatedToPurchaseCenter.aspx");

    }
    protected void LinkButton_FarDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Masters/FarmerDetails.aspx");
    }
    protected void Link_Logout_Click(object sender, EventArgs e)
    {


        Response.Redirect("../mpproc/Logout.aspx");
    }
}

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

public partial class IssueCenter_GunnyBagsDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
       

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnaddrect_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("~/IssueCenter/Movement_Challan_WLC.aspx");
    }
    protected void btnaddrect_Click1(object sender, EventArgs e)
    {
        IssueCenter_GunnyBagsDetails mychildwindow = new IssueCenter_GunnyBagsDetails();

        //mychildwindow.Closed += new EventHandler(mychildwindow_Closed);
        //mychildwindow.Show();

       
       
        

    }
    void mychildwindow_Closed(object sender, EventArgs e)

        {
            Response.Redirect("~/IssueCenter/Movement_Challan_WLC.aspx");
        }
  
}

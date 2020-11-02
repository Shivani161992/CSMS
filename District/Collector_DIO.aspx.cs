using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Collector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Collector/DIO"] == "Collector")
        {
            Master.BodyTitle = "Collector";
        }
        
        else if(Session["Collector/DIO"] == "DIO")
        {
            Master.BodyTitle = "DIO";
        }
        
        Master.Name = Session["dist_name"].ToString();

        Response.Redirect("~/District/frmReport_Coll_DIO.aspx");
    }

}
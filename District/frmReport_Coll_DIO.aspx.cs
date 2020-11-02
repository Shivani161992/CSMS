using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_frmReport_Coll_DIO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.SetPanel1.GetType();
        //Master.SetPanel2.GetType();

        if (Session["Collector/DIO"] == "Collector")
        {
            Master.BodyTitle = "Collector";
            Master.Name = Session["dist_name"].ToString();
        }

        else if (Session["Collector/DIO"] == "DIO")
        {
            Master.BodyTitle = "DIO";
            Master.Name = Session["dist_name"].ToString();
        }

        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
}
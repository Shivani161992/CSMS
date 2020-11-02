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

public partial class District_Rackpoint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (rdfrmCenter.Checked)
        {
            Response.Redirect("RackDispatch_IC.aspx");
        }

        else if (rdfrmuparjan.Checked)
        {
            Response.Redirect("RackPointEntry_frmProcurement.aspx");
        }

        else if (rdbFrmRack.Checked)
        {
            Response.Redirect("RecFromRack.aspx");
        }

        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select One Option.....'); </script> ");
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
       

        Response.Redirect("~/District/Dist_Welcome.aspx");
    }
}

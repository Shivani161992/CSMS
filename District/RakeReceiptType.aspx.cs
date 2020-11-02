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

public partial class District_RakeReceiptType : System.Web.UI.Page

{
    public string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Page.IsPostBack == false)
        {
            //if (version == "H")
            //{
            //    lbldotype.Text = Resources.LocalizedText.lbldotype;
            //    lbldoselect.Text = Resources.LocalizedText.lbldoselect;
            //    btnok.Text = Resources.LocalizedText.btnok;

            //}

        }

    }
    protected void ddl_issueto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ddlraketype.SelectedItem.Value == "S")
        {
            Response.Redirect("~/District/Rack_Receipt_Dtl_Sugar.aspx");
        }
        if (ddlraketype.SelectedItem.Value == "O")
        {
            Response.Redirect("~/District/Rack_Receipt_Dtl.aspx");
        }
           
    }
    protected void ddlraketype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

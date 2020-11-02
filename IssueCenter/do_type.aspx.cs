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

public partial class IssueCenter_do_type : System.Web.UI.Page
{
    public string version = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        version = Session["hindi"].ToString();
        if (Page.IsPostBack == false)
        {
            if (version == "H")
            {
                lbldotype.Text = Resources.LocalizedText.lbldotype;
                lbldoselect.Text = Resources.LocalizedText.lbldoselect;
                btnok.Text = Resources.LocalizedText.btnok;
            }
        }

    }
    protected void ddl_issueto_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (ddl_issueto.SelectedItem.Value == "LF")
        {
            Session["Society"] = "LF";
            Response.Redirect("~/IssueCenter/delivery.aspx");
        }
        if (ddl_issueto.SelectedItem.Value == "L")
        {
            Response.Redirect("~/IssueCenter/DeliveryOrder_lead.aspx");
        }
        if (ddl_issueto.SelectedItem.Value == "F")
        {
            Session["Society"] = "F";
            Response.Redirect("~/IssueCenter/delivery.aspx");
        }
        if (ddl_issueto.SelectedItem.Value == "O")
        {
            Response.Redirect("~/IssueCenter/DeliveryOrder_other.aspx");
        }

        if (ddl_issueto.SelectedItem.Value == "MP")
        {
            Session["Society"] = "LF";
            Response.Redirect("~/IssueCenter/DeliveryOrder_mpscsc.aspx");
        }

        if (ddl_issueto.SelectedItem.Value == "DS")
        {
            Session["Society"] = "DS";
            Response.Redirect("~/IssueCenter/DO_DoorStep_Delivery.aspx");
        }

        if (ddl_issueto.SelectedItem.Value == "Pay")
        {
           Response.Redirect("~/IssueCenter/Transporter_Order_Payment_detail.aspx");
        }
        if (ddl_issueto.SelectedItem.Value == "TO")
        {
            Response.Redirect("~/IssueCenter/IssueAgainst_TO.aspx");
        }
    }


}

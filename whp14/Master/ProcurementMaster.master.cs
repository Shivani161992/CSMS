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

public partial class WHP14_Master_ProcurementMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["App"].ToString() != null && Session["App"].ToString() == "WPMS2014")
            {
                if (Session["District_Code"].ToString() != null && Session["Society_Id"].ToString() != null)
                {
                    if (Session["User"] != null)
                    {
                        if (Session["UserName"] != null && Session["Society_Name"] != null)
                        {
                            if (Session["UserName"].ToString() == "PurchaseCenter")
                            {

                                Label1.Text = Session["Society_Name"].ToString() + ",जिला -" + Session["DistrictName"].ToString();
                                if (!IsPostBack)
                                {
                                    //CheckSoc(Session["District_Code"].ToString(), Session["Society_Id"].ToString());
                                }
                            }
                        }


                    }
                    else
                    {
                        Response.Redirect("../Login1.aspx");

                    }
                }
                else
                {
                    Response.Redirect("../Login1.aspx");

                }
            }
            else
            {
                Response.Redirect("../Login1.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Login1.aspx");
        }
    }
    protected void Link_AnajPrapti_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WHP14/Procurement_Wheat/frm_AnajPrapti_FromFarmer.aspx");
    }
    protected void LB_AnajPwati_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WHP14/Procurement_Wheat/Procurement_Report/frm_Anaj_Prapti_Pawati_Exp_Receipt.aspx");
    }

 
}

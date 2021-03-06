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
using System.Data.SqlClient;
using System.Globalization;

public partial class WHP14_Procurement_Wheat_Procurement_Report_frm_Anaj_Prapti_Pawati_Exp_Receipt : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014_Test"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["District_Code"] != null && Session["Society_Id"] != null)
            {
                if (!IsPostBack)
                {
                    Button1.Attributes.Add("onClick", "CallPrint('divPrint')");
                    Button2.Attributes.Add("onClick", "CallPrint('divPrint1')");
                }
            }
            else
            {
                Response.Redirect("../../Login1.aspx");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPR_Date.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति दिनांक का चयन करे |'); </script> ");
                return;
            }
            GetRCPTNo(txtPR_Date.Text);
        }
        catch (Exception)
        {

        }
    }
    protected void GetRCPTNo(string datevalue)
    {
        try
        {
            ddl_PR_No.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("Select ReceivedID From [dbo].[CommodityReceivedFromFarmer] Where CONVERT(varchar(50),Date_Of_Receipt,103) ='" + datevalue + "' and Society_Id='" + Session["Society_Id"].ToString() + "'", con);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_PR_No.DataSource = ds;
                ddl_PR_No.DataTextField = "ReceivedID";
                ddl_PR_No.DataValueField = "ReceivedID";
                ddl_PR_No.DataBind();
                ddl_PR_No.Items.Insert(0, "--चुने--");
            }
            else
            {
                ddl_PR_No.Items.Insert(0, "--चुने--");
            }
        }
        catch (Exception)
        { }
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            DateTime d;
            //args.IsValid =
            if (!DateTime.TryParseExact(args.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('dd/MM/yyyy फोर्मेट मे ही दिनांक चुने |'); </script> ");
                txtPR_Date.Text = "";
                return;
            }

        }
        catch (Exception)
        { }
    }
    protected void ddl_PR_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtPR_Date.Text.Trim() == String.Empty)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति का चयन करे'); </script> ");
                return;
            }
            if (ddl_PR_No.Items.Count <= 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति क्रमांक उपलब्ध नहीं है'); </script> ");
                return;
            }
            if (ddl_PR_No.SelectedIndex == 0)
            {
                Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('प्राप्ति क्रमांक चुने'); </script> ");
                return;
            }

            getdata(ddl_PR_No.SelectedItem.Value.Trim(), txtPR_Date.Text.Trim(), Session["Society_Id"].ToString());
        }
        catch (Exception)
        { }
    }
    protected void getdata(string RID, string RDATE, string soc)
    {
        try
        {
            DataSet ds = Get_Receipt_Detail(ddl_PR_No.SelectedItem.Value.Trim(), txtPR_Date.Text.Trim(), Session["Society_Id"].ToString());
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (rb9.Checked == true)
                {
                    lbltdate.Text = "दिनाँक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                    DataList1.DataSource = ds;
                    DataList1.DataBind();
                    foreach (DataListItem di in DataList1.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिनाँक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn.Visible = true;
                        pn1.Visible = false;
                        Button1.Visible = true;
                        Button2.Visible = false;
                    }
                }
                else
                {
                    DataList2.DataSource = ds;
                    DataList2.DataBind();
                    foreach (DataListItem di in DataList2.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिनाँक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn1.Visible = true;
                        pn.Visible = false;
                        Button1.Visible = false;
                        Button2.Visible = true;
                    }
                }
            }
            else
            {
                pn.Visible = false;
                pn1.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
            }
        }
        catch (Exception)
        { }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        //string folder = ConfigurationManager.ConnectionStrings["rptfolder_whtmp"].ProviderName;
        if (Session["Society_Id"] != null)
        {
            Response.Redirect("~/WHP14/Procurement_Wheat/frm_AnajPrapti_FromFarmer.aspx");
        }
        else
        {
            Response.Redirect("~/WHP14/Login1.aspx");
        }

    }
    ///code
    public DataSet Get_Receipt_Detail(object ReceivedID, object Date_Of_Receipt, object Society_Id)
    {
        con.Close();
        con.Open();
        string str = "SELECT dbo.CommodityReceivedFromFarmer.ReceivedID, dbo.Society.Society_Name, dbo.Districts.District_Name,(cast(day(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)+'/'+cast(Month(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)+'/'+cast(year(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)) as Date_Of_Receipt,dbo.FarmerRegistration.RinPustikaNo, dbo.CommodityReceivedFromFarmer.Farmer_Id, dbo.FarmerRegistration.FarmerName,dbo.FarmerRegistration.FatherHusName, dbo.FarmerRegistration.VillageName, dbo.FarmerRegistration.Farmer_BankName_New,dbo.FarmerRegistration.Farmer_BankBranchName, dbo.FarmerRegistration.Farmer_BankAccountNo, dbo.CommodityReceivedFromFarmer.TaulPatrakNo,dbo.Crop_Master.crop, dbo.CommodityReceivedFromFarmer.Bags, dbo.CommodityReceivedFromFarmer.QtyReceived, dbo.CommodityRate.MSPRate,dbo.CommodityRate.CentralBonus, dbo.CommodityRate.StateBonus, dbo.CommodityReceivedFromFarmer.TotaAmountPayableToFarmer,dbo.CommodityReceivedFromFarmer.FarmerLoanFromSc, dbo.CommodityReceivedFromFarmer.AmtAgainstSCCredit,dbo.CommodityReceivedFromFarmer.FarmerLoanFromBank, dbo.CommodityReceivedFromFarmer.AmtAgainstBankCredit,dbo.CommodityReceivedFromFarmer.Irrigation_Loan, dbo.CommodityReceivedFromFarmer.AmtAgIrg_Loan,dbo.CommodityReceivedFromFarmer.NetAmountPayableToFarmer,dbo.FarmerRegistration.Mobileno FROM dbo.Districts RIGHT OUTER JOIN dbo.CommodityReceivedFromFarmer LEFT OUTER JOIN dbo.Society ON dbo.CommodityReceivedFromFarmer.Society_Id = dbo.Society.Society_Id LEFT OUTER JOIN dbo.FarmerRegistration ON dbo.CommodityReceivedFromFarmer.Farmer_Id = dbo.FarmerRegistration.Farmer_Id ON dbo.Districts.District_Code = dbo.CommodityReceivedFromFarmer.District_Id LEFT OUTER JOIN dbo.Crop_Master  ON dbo.Crop_Master.crpcode = dbo.CommodityReceivedFromFarmer.CommodityId INNER JOIN dbo.CommodityRate ON dbo.Crop_Master.crpcode = dbo.CommodityRate.CommodityId WHERE (dbo.CommodityReceivedFromFarmer.ReceivedID  = '" + ReceivedID + "') and CONVERT(varchar(50), dbo.CommodityReceivedFromFarmer.Date_Of_Receipt,103)='" + Date_Of_Receipt + "' and CommodityReceivedFromFarmer.Society_Id='" + Society_Id + "'";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        return ds;
    }

    protected void rb9_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = Get_Receipt_Detail(ddl_PR_No.SelectedItem.Value.Trim(), txtPR_Date.Text.Trim(), Session["Society_Id"].ToString());
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (rb9.Checked == true)
                {
                    lbltdate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                    DataList1.DataSource = ds;
                    DataList1.DataBind();
                    foreach (DataListItem di in DataList1.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn.Visible = true;
                        pn1.Visible = false;
                        Button1.Visible = true;
                        Button2.Visible = false;
                    }
                }
                else
                {
                    DataList2.DataSource = ds;
                    DataList2.DataBind();
                    foreach (DataListItem di in DataList2.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn1.Visible = true;
                        pn.Visible = false;
                        Button1.Visible = false;
                        Button2.Visible = true;
                    }
                }
            }
            else
            {
                pn.Visible = false;
                pn1.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
            }
        }
        catch (Exception)
        { }
    }
    protected void rb12_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = Get_Receipt_Detail(ddl_PR_No.SelectedItem.Value.Trim(), txtPR_Date.Text.Trim(), Session["Society_Id"].ToString());
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (rb9.Checked == true)
                {
                    lbltdate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                    DataList1.DataSource = ds;
                    DataList1.DataBind();
                    foreach (DataListItem di in DataList1.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn.Visible = true;
                        pn1.Visible = false;
                        Button1.Visible = true;
                        Button2.Visible = false;
                    }
                }
                else
                {
                    DataList2.DataSource = ds;
                    DataList2.DataBind();
                    foreach (DataListItem di in DataList2.Items)
                    {
                        Label lblAmtAgainstSCCredit = (Label)di.FindControl("lblAmtAgainstSCCredit");
                        Label lblAmtAgainstBankCredit = (Label)di.FindControl("lblAmtAgainstBankCredit");
                        Label lblAmtAgIrg_Loan = (Label)di.FindControl("lblAmtAgIrg_Loan");
                        Label lblamt = (Label)di.FindControl("lblamt");
                        Label lbldate = (Label)di.FindControl("lbldate");

                        lbldate.Text = "दिंनाक:" + System.DateTime.Now.Date.ToString("dd/MM/yyyy") + " समय " + System.DateTime.Now.ToString("hh:mm:ss");
                        lblamt.Text = Convert.ToString(double.Parse(lblAmtAgainstSCCredit.Text) + double.Parse(lblAmtAgainstBankCredit.Text) + double.Parse(lblAmtAgIrg_Loan.Text));
                        pn1.Visible = true;
                        pn.Visible = false;
                        Button1.Visible = false;
                        Button2.Visible = true;
                    }
                }
            }
            else
            {
                pn.Visible = false;
                pn1.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
            }
        }
        catch (Exception)
        { }
    }
}

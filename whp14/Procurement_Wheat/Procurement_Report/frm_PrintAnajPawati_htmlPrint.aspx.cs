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
public partial class WHP14_Procurement_Wheat_Procurement_Report_frm_PrintAnajPawati_htmlPrint : System.Web.UI.Page
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Appconstr_WPMS2014_Test"].ToString());
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
                    string fid = base64Decode(Request.QueryString["fid"].ToString());
                    string rcvid = base64Decode(Request.QueryString["RCVID"].ToString());
                    lblrid.Text = rcvid;
                    lblfid.Text = fid;
                    getdata(rcvid, fid);
                }
            }
            else
            {
                Response.Redirect("../Login1.aspx");
            }
        }
        catch (Exception ex)
        { }
    }
    private string base64Encode(string sData)
    {
        try
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    public string base64Decode(string sData)
    {
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char); return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    protected void getdata(string RID, string fid)
    {
        try
        {
            DataSet ds = Get_Receipt_Detail(RID, fid);
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
            Response.Redirect("../frm_AnajPrapti_FromFarmer.aspx");
        }
        else
        {
            Response.Redirect("../Login1.aspx");
        }

    }
    ///code
    public DataSet Get_Receipt_Detail(object ReceivedID, object Farmer_Id)
    {
        con.Close();
        con.Open();
        string str = "SELECT dbo.CommodityReceivedFromFarmer.ReceivedID, dbo.Society.Society_Name, dbo.Districts.District_Name,(cast(day(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)+'/'+cast(Month(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)+'/'+cast(year(dbo.CommodityReceivedFromFarmer.Date_Of_Receipt) as varchar)) as Date_Of_Receipt,dbo.FarmerRegistration.RinPustikaNo, dbo.CommodityReceivedFromFarmer.Farmer_Id, dbo.FarmerRegistration.FarmerName,dbo.FarmerRegistration.FatherHusName, dbo.FarmerRegistration.VillageName, dbo.FarmerRegistration.Farmer_BankName_New,dbo.FarmerRegistration.Farmer_BankBranchName, dbo.FarmerRegistration.Farmer_BankAccountNo, dbo.CommodityReceivedFromFarmer.TaulPatrakNo,dbo.Crop_Master.crop, dbo.CommodityReceivedFromFarmer.Bags, dbo.CommodityReceivedFromFarmer.QtyReceived, dbo.CommodityRate.MSPRate,dbo.CommodityRate.CentralBonus, dbo.CommodityRate.StateBonus, dbo.CommodityReceivedFromFarmer.TotaAmountPayableToFarmer,dbo.CommodityReceivedFromFarmer.FarmerLoanFromSc, dbo.CommodityReceivedFromFarmer.AmtAgainstSCCredit,dbo.CommodityReceivedFromFarmer.FarmerLoanFromBank, dbo.CommodityReceivedFromFarmer.AmtAgainstBankCredit,dbo.CommodityReceivedFromFarmer.Irrigation_Loan, dbo.CommodityReceivedFromFarmer.AmtAgIrg_Loan,dbo.CommodityReceivedFromFarmer.NetAmountPayableToFarmer,dbo.FarmerRegistration.Mobileno FROM dbo.Districts RIGHT OUTER JOIN dbo.CommodityReceivedFromFarmer LEFT OUTER JOIN dbo.Society ON dbo.CommodityReceivedFromFarmer.Society_Id = dbo.Society.Society_Id LEFT OUTER JOIN dbo.FarmerRegistration ON dbo.CommodityReceivedFromFarmer.Farmer_Id = dbo.FarmerRegistration.Farmer_Id ON dbo.Districts.District_Code = dbo.CommodityReceivedFromFarmer.District_Id LEFT OUTER JOIN dbo.Crop_Master  ON dbo.Crop_Master.crpcode = dbo.CommodityReceivedFromFarmer.CommodityId INNER JOIN dbo.CommodityRate ON dbo.Crop_Master.crpcode = dbo.CommodityRate.CommodityId WHERE (dbo.CommodityReceivedFromFarmer.ReceivedID  = '" + ReceivedID + "') and dbo.CommodityReceivedFromFarmer.Farmer_Id='" + Farmer_Id + "'";
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
            DataSet ds = Get_Receipt_Detail(lblrid.Text, lblfid.Text);
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
            DataSet ds = Get_Receipt_Detail(lblrid.Text, lblfid.Text);
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

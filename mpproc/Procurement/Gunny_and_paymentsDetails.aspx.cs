using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Security.Cryptography;
using System.Data.SqlClient;

public partial class mpproc_Procurement_Gunny_and_paymentsDetails : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_mpproc"].ToString());
    SqlCommand cmd = new SqlCommand();
    protected Common ComObj = null, cmn = null;
    public string qry = "";
    Districts Dobj = null;
    Agency Aobj = null;
    MarketingSeas Mobj = null;
    cropYear cobj = null;
    comodity CdObj = null;
    SqlString SqlObj = null;
    DataReader objDr = null;
    Districts distObj = null;
    string dist = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pcId"] != null)
        {


            dist = Session["dist_id"].ToString();

            ComObj = new Common(ConfigurationSettings.AppSettings["ConnectionString_mpproc"].ToString());

            txt_AmountPaid.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txt_AmountPaid.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
           // txt_AmountPaid.Attributes.Add("onkeypress", "return CheckIsNumeric(this)");
            txt_AmountPaid.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");


            txtBillAmt.Attributes.Add("onblur", "extractNumber(this,2,false)");
            txtBillAmt.Attributes.Add("onkeyup", "extractNumber(this,2,false)");
            txtBillAmt.Attributes.Add("onkeypress", "return CheckIsNumeric(event,this)");

            txt_GunnyIssued.Attributes.Add("onkeypress", "return CheckIsNumericInt(this)");
            txt_GunnyReturned.Attributes.Add("onkeypress", "return CheckIsNumericInt(this)");
           


            if (!IsPostBack)
            {

                txt_transaction_date.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
             
                GetInfo();
                GetCommodity();
            }
        }
        else
        {

            Response.Redirect("../../frmLogin.aspx");

        }

    }
    private void GetCommodity()
    {

        CdObj = new comodity(ComObj);
        string strcom = Session["Markseas_id"].ToString();
        string strsql = "SELECT * FROM CommodityMaster where MarkSeasId='" + strcom + "'";
        DataSet ds = CdObj.selectAny(strsql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDL_Commodity.DataSource = ds.Tables[0];
            DDL_Commodity.DataTextField = "CommodityName";
            DDL_Commodity.DataValueField = "CommodityId";
            DDL_Commodity.DataBind();
        }
        else
        {

            // nothing

        }

    }
    private void GetInfo()
    {
        lblDistrict_Res.Text = Session["dist_name"].ToString();
        lblAgency_Res.Text = Session["Ag_Name"].ToString();
        lblMar_Seas_Res.Text = Session["Mark_Seas"].ToString();
        lblCrop_Y_Res.Text = Session["cropyear"].ToString();
        DDL_PC_Name.Items.Insert(0, Session["pc_name"].ToString());
        DDLAgency.Items.Insert(0, Session["Ag_Name"].ToString());
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string trnsdate = getDate_MDY(txt_transaction_date.Text);
        string cmdty = DDL_Commodity.SelectedValue.ToString();
        int gunnyIssued = CheckNullInt(txt_GunnyIssued.Text.ToString());
        int gunnyreturnd = CheckNullInt(txt_GunnyReturned.Text.ToString ());
        float payment = CheckFloat(txt_AmountPaid.Text.ToString());
        float billpayment = CheckFloat(txtBillAmt.Text.ToString ());
        string byGodownID = "0";
        string gdnid = "";
        int gdnnum = 0;
        objDr = new DataReader(ComObj);
        string qrey = "select Max(TransactionID) as TransactionID  from AgencyPaymentGunnyDetails_To_PC where DistrictId='" + dist + "' and CropYearId='" + Session["cropyear"].ToString() + "'and MarketingSeason='" + Session["Markseas_id"].ToString() + "' and PCID='" + Session["pcid"].ToString() + "'";
        DataSet dsf = objDr.selectAny(qrey);
        if (dsf == null )
        {
            gdnid = "1";
        }


        else
        {
            //
            if (dsf.Tables[0].Rows.Count == 0)
            {
                gdnid = "1";
            }
            else
            {

                DataRow dr = dsf.Tables[0].Rows[0];
                gdnid = dr["TransactionID"].ToString();
                ComObj.CloseConnection();

                gdnnum = Int32.Parse(CheckNullInt(gdnid.ToString()).ToString ());
                gdnnum = gdnnum + 1;
                gdnid = gdnnum.ToString();
            }
            //
        }
        string qryinsert = "INSERT INTO  AgencyPaymentGunnyDetails_To_PC ( TransactionID,DistrictId , PCType_ID_Agency , CropYearId , MarketingSeason , PCID , EnteredByGodownID , DateofTransaction , PaymentToPC , GunnyBagsIssuedtoPC , GunnyBagsReturnedbyPC , BilAmtRecdFromPC , Date_Of_Creation , Date_Of_Updation , CommodityId ,ip) VALUES(" +gdnid+",'"+ dist +"','"+  Session["Ag_id"].ToString ()+"','"+  Session["cropyear"].ToString ()+"','"+Session["Markseas_id"].ToString ()+"','"  + Session["pcId"].ToString ()+"','"+ byGodownID +"','"+  trnsdate +"'," +payment +","+  gunnyIssued  +","+ gunnyreturnd +","+ billpayment +",getDate(),'','"+ cmdty + "','"+ ip +"')";
        //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + qryinsert + "'); </script> ");
        try
        {
            cmd.Connection = con;
            cmd.CommandText = qryinsert;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Inserted Successfully......'); </script> ");

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message +"'); </script> ");
        }
        finally
        {
        }

    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Procurement/Gunny_and_paymentsDetails.aspx");
    }
    void FillGrid()
    {
        try
        {
            string trnsdate = getDate_MDY(txt_transaction_date.Text);
            SqlObj = new SqlString(ComObj);
            string qry = "Select * from AgencyPaymentGunnyDetails_To_PC where DistrictId='" + dist + "' and CropYearId='" + Session["cropyear"].ToString() + "'and MarketingSeason='" + Session["Markseas_id"].ToString() + "' and PCID='" + Session["pcid"].ToString() + "' and DateofTransaction='" + trnsdate + "' ";
            DataSet ds = SqlObj.selectAny(qry);
            if (ds == null)
            {

            }
            else
            {

                GridView_Wheat_Proc.DataSource = ds;
                GridView_Wheat_Proc.DataBind();

            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FillGrid();
        

    }
    protected void GridView_Wheat_Proc_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_AmountPaid.Text = GridView_Wheat_Proc.SelectedRow.Cells[4].Text.ToString();
        txt_GunnyIssued.Text = GridView_Wheat_Proc.SelectedRow.Cells[5].Text.ToString();
        txt_GunnyReturned.Text  = GridView_Wheat_Proc.SelectedRow.Cells[6].Text.ToString();
        txtBillAmt.Text = GridView_Wheat_Proc.SelectedRow.Cells[7].Text.ToString();
        lbltrans.Text = GridView_Wheat_Proc.SelectedRow.Cells[1].Text.ToString();
        btnAdd.Visible = false;

        btnUpdate.Visible = true;





    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string trnsdate = getDate_MDY(txt_transaction_date.Text);
        string cmdty = DDL_Commodity.SelectedValue.ToString();
        int gunnyIssued = CheckNullInt(txt_GunnyIssued.Text.ToString());
        int gunnyreturnd = CheckNullInt(txt_GunnyReturned.Text.ToString());
        float payment = CheckFloat(txt_AmountPaid.Text.ToString());
        float billpayment = CheckFloat(txtBillAmt.Text.ToString());
        string update = "Update AgencyPaymentGunnyDetails_To_PC set DateofTransaction='" + trnsdate + "', PaymentToPC =" + payment + " , GunnyBagsIssuedtoPC= " + gunnyIssued + ", GunnyBagsReturnedbyPC=" + gunnyreturnd + ", BilAmtRecdFromPC= " + billpayment + ",Date_Of_Updation=getDate() where DistrictId='" + dist + "' and CropYearId='" + Session["cropyear"].ToString() + "'and MarketingSeason='" + Session["Markseas_id"].ToString() + "' and PCID='" + Session["pcid"].ToString() + "' and TransactionID=" + lbltrans.Text ;
        
        try
        {
            cmd.Connection = con;
            cmd.CommandText = update;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data Updated Successfully......'); </script> ");
            FillGrid();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
        finally
        {
        }
    }
}

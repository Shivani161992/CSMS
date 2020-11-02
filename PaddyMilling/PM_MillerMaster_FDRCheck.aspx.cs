using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PaddyMilling_PM_MillerMaster_FDRCheck : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    //string Gdistance = "";
    //string Mdistance = "";
    public string gatepass = "";

    public int getnum;

    string Branch;
    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                txtMillDist.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                GetCropYearValues();
                string FDRDate = Request.Form[txtfdrDate.UniqueID];
                txtfdrDate.Text = FDRDate;

                string MaturityFDRDate = Request.Form[txtMaturityofFDR.UniqueID];
                txtMaturityofFDR.Text = MaturityFDRDate;

                string Chequedate = Request.Form[txtCheckDate.UniqueID];
                txtCheckDate.Text = Chequedate;

                //GetMillName();
                //GetMPIssueCentre();
                // GetDist();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }
    public void GetCropYearValues()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT CropYear FROM PaddyMilling_CropYear order by CropYear desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlCropYear.DataSource = ds.Tables[0];
                    ddlCropYear.DataTextField = "CropYear";
                    ddlCropYear.DataValueField = "CropYear";
                    ddlCropYear.DataBind();
                    ddlCropYear.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex > 0)
        {
            GetMillerName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year '); </script> ");
            return;


        }

    }
    public void GetMillerName()
    {
        string DistCode = Session["dist_id"].ToString();
        ddlMillName.Items.Clear();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                if (DistCode == "38")
                {
                    select = "select R.Mill_ID as MillID, MR.mill_name as MillName from PM_RatioMaster as R inner join Miller_Registration_2017 as MR on MR.Registration_ID=R.Mill_ID and MR.CropYear=R.CropYear where (MillerDist='" + DistCode + "' or   MillerDist in ('505','506','507','508','509'))  and  R.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' order by MR.mill_name";

                }
                else if (DistCode != "38")
                {

                    select = "select R.Mill_ID as MillID, MR.mill_name as MillName from PM_RatioMaster as R inner join Miller_Registration_2017 as MR on MR.Registration_ID=R.Mill_ID and MR.CropYear=R.CropYear where MillerDist='" + DistCode + "' and  R.CropYear='" + ddlCropYear.SelectedValue.ToString() + "' order by MR.mill_name";

                }

                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillID";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर का पंजीकरण स्वीकार नहीं किया गया है|'); </script> ");
                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void ddlMillName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMillName.SelectedIndex > 0)
        {
            GetMillerData();
            GetBankName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name  '); </script> ");
            return;


        }
    }
    public void GetMillerData()
    {
        string DistCode = Session["dist_id"].ToString();

        //string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {

                string select = "";
                if (DistCode == "38")
                {
                    select = "select FDR, checks, LOT_Amount, State_Code, MillerDist from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and (MillerDist='" + DistCode + "' or   MillerDist in ('505','506','507','508','509')) and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";

                }
                else if (DistCode != "38")
                {

                    select = "select FDR, checks, LOT_Amount, State_Code, MillerDist from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MillerDist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";


                }

                // select = "select FDR, checks, LOT_Amount from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MillerDist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtLot.Text = ds.Tables[0].Rows[0]["LOT_Amount"].ToString();
                    lblMinFDR.Visible = true;
                    lblCheck.Visible = true;
                    lblMinFDR.Text = ds.Tables[0].Rows[0]["FDR"].ToString();
                    lblCheck.Text = ds.Tables[0].Rows[0]["checks"].ToString();

                    hdfStateCode.Value = ds.Tables[0].Rows[0]["State_Code"].ToString();
                    hdfMillDist.Value = ds.Tables[0].Rows[0]["MillerDist"].ToString();

                    if (hdfStateCode.Value == "27")
                    {
                        lblmillState.Visible = true;
                        lblmillState.Text = " Miller State:-  Maharashtra";
                        GetOtherDist();
                    }
                    else if (hdfStateCode.Value == "23")
                    {
                        lblmillState.Visible = true;
                        lblmillState.Text = " Miller State:-  Madhya Pradesh";
                        GetOwnDist();
                    }






                    txtBankFDRNumber.Enabled = true;
                    txtChequeNumber.Enabled = true;
                    bttsubmit.Enabled = true;

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Ratio for FDR and Checks is not available'); </script> ");
                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetOtherDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "  select district_code, district_name, State_Id from OtherState_DistrictCode where district_code='" + hdfMillDist.Value + "' and State_Id='" + hdfStateCode.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblmillDist.Visible = true;
                        lblmillDist.Text = "Miller District:- " + ds.Tables[0].Rows[0]["district_name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void GetOwnDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "";
                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + hdfMillDist.Value + "'  Order By district_name";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblmillDist.Visible = true;
                        lblmillDist.Text = "Miller District:- " + ds.Tables[0].Rows[0]["district_name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }



    public void GetBankName()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select Bank_ID, Bank_Name from Bank_Master_New";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlfdrbank.DataSource = ds.Tables[0];
                    ddlfdrbank.DataTextField = "Bank_Name";
                    ddlfdrbank.DataValueField = "Bank_ID";
                    ddlfdrbank.DataBind();
                    ddlfdrbank.Items.Insert(0, "--Select--");

                    ddlcheckbank.DataSource = ds.Tables[0];
                    ddlcheckbank.DataTextField = "Bank_Name";
                    ddlcheckbank.DataValueField = "Bank_ID";
                    ddlcheckbank.DataBind();
                    ddlcheckbank.Items.Insert(0, "--Select--");

                    txtfdrifsc.Enabled = true;
                    txtcheckifsc.Enabled = true;
                    txtvalueFDR.Enabled = true;
                    //txtvalueCheck.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कोई भी बैंक का नाम उपलभ नहीं है|'); </script> ");
                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    protected void txtvalueFDR_TextChanged(object sender, EventArgs e)
    {
        if (txtvalueFDR.Text == txtLot.Text)
        {
            txtvalueCheck.Enabled = true;
            txtvalueCheck.Text = "";
            trmessage.Visible = true;
            trmessageSpace.Visible = true;
            lblmessage.Visible = true;
            decimal cheque, FDR;
            FDR = Convert.ToDecimal(txtvalueFDR.Text);
            cheque = FDR * Convert.ToDecimal(lblCheck.Text);


            lblmessage.Text = "चेक की अधिकतम राशि .." + cheque + " ..से जायदा नहीं हो सकती अथवा 0 हो सकती है| ";

        }
        else if (Convert.ToInt32(txtvalueFDR.Text) < Convert.ToInt32(txtLot.Text))
        {
            trmessage.Visible = true;
            trmessageSpace.Visible = true;
            lblmessage.Visible = true;
            lblmessage.Text = "कम से कम एक लाट की FDR होना अनिवारिये है, जिसकी धन राशि १ लाट के बराबर होगी|";
            txtvalueFDR.Text = "";
            txtvalueFDR.Focus();
            return;

        }
        else if (Convert.ToInt32(txtvalueFDR.Text) > Convert.ToInt32(txtLot.Text))
        {

            txtvalueCheck.Enabled = true;
            txtvalueCheck.Text = "";
            trmessage.Visible = true;
            trmessageSpace.Visible = true;
            lblmessage.Visible = true;
            decimal cheque, FDR;
            FDR = Convert.ToDecimal(txtvalueFDR.Text);
            cheque = FDR * Convert.ToDecimal(lblCheck.Text);


            lblmessage.Text = "चेक की अधिकतम राशि .." + cheque + " ..से जायदा नहीं हो सकती अथवा 0 हो सकती है| ";
        }

    }
    protected void txtvalueCheck_TextChanged(object sender, EventArgs e)
    {
        decimal cheque, FDR;
        FDR = Convert.ToDecimal(txtvalueFDR.Text);
        cheque = FDR * Convert.ToDecimal(lblCheck.Text);

        if (Convert.ToInt32(txtvalueCheck.Text) <= cheque && Convert.ToInt32(txtvalueCheck.Text) >= 0)
        {

        }
        else
        {
            trmessage.Visible = true;
            trmessageSpace.Visible = true;
            lblmessage.Visible = true;
            lblmessage.Text = "चेक की अधिकतम राशि .." + cheque + " ..से जायदा नहीं हो सकती अथवा 0 हो सकती है| ";
            txtvalueCheck.Text = "";
            txtvalueCheck.Focus();
            return;
        }

    }
    protected void bttsubmit_Click(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Crop Year'); </script> ");
            return;
        }
        else if (ddlMillName.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller name'); </script> ");
            return;
        }
        else if (ddlfdrbank.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select FDR Bank Name'); </script> ");
            return;
        }
        else if (ddlcheckbank.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Bank Name'); </script> ");
            return;
        }
        else if (txtfdrifsc.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter FDR Bank IFSC code'); </script> ");
            txtfdrifsc.Focus();
            return;
        }
        else if (txtcheckifsc.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Bank IFSC Code'); </script> ");
            txtcheckifsc.Focus();
            return;
        }
        else if (txtfdrDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter FDR Date'); </script> ");
            return;
        }
        else if (txtCheckDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Date'); </script> ");
            return;
        }
        else if (txtvalueFDR.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter value of FDR '); </script> ");
            txtvalueFDR.Focus();
            return;
        }
        else if (txtvalueCheck.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select value Cheque '); </script> ");
            txtvalueCheck.Focus();
            return;
        }
        else if (txtMaturityofFDR.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Maturity Date of FDR '); </script> ");
            return;
        }
        else if (txtBankFDRNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please enter FDR/BankSecurity Number'); </script> ");
            txtBankFDRNumber.Focus();
            return;
        }
        else if (txtChequeNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Number'); </script> ");
            txtChequeNumber.Focus();
            return;
        }

        else if (txtChequeNumber.Text == txtBankFDRNumber.Text)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('FDR Number and Cheque number can not be same'); </script> ");
            txtBankFDRNumber.Text = "";
            txtChequeNumber.Text = "";
            txtBankFDRNumber.Focus();
           
            return;
        }
        else
        {
            string DistCode = Session["dist_id"].ToString();
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    con.Open();

                    ClientIP objClientIP = new ClientIP();
                    string GetIp = objClientIP.GETIP();
                    ConvertServerDate ServerFDRDate = new ConvertServerDate();
                    string FdrDATE = ServerFDRDate.getDate_MDY(txtfdrDate.Text);

                    ConvertServerDate ServerMatFDRDate = new ConvertServerDate();
                    string MaturityFdrDATE = ServerMatFDRDate.getDate_MDY(txtMaturityofFDR.Text);

                    ConvertServerDate ServerChequeDate = new ConvertServerDate();
                    string chequeDate = ServerChequeDate.getDate_MDY(txtCheckDate.Text);
                    string qrey = "select max(FDR_ChequeID) as FDR_ChequeID from PM_FDR_and_Cheque_Master where LEN(FDR_ChequeID)<15 ";
                    da = new SqlDataAdapter(qrey, con);

                    ds = new DataSet();
                    da.Fill(ds);
                    //mobj1 = new MoveChallan(ComObj);
                    //string qrey = "select isnull(max(Inspector_ID),0) as Transporter_ID  from Inspector_Master_02017 where  Distt_ID='" + sid + "' and LEN(Inspector_ID)<8 ";
                    //DataSet ds = new DataSet();
                    // dmax.Fill(ds);
                    // DataTable dt = ds.Tables[""];
                    DataRow dr = ds.Tables[0].Rows[0];
                    //gatepass = dr["Inspector_ID"].ToString();
                    gatepass = ds.Tables[0].Rows[0]["FDR_ChequeID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "1718" + DistCode + "0";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }



                    int lot_Allowed;
                    decimal cheque, FDR, LOTamt;
                    FDR = Convert.ToDecimal(txtvalueFDR.Text);
                    cheque = Convert.ToDecimal(txtvalueCheck.Text);
                    LOTamt = Convert.ToDecimal(txtLot.Text);

                    decimal total;
                    total = FDR + cheque;
                    decimal lot = total / LOTamt;
                    decimal floor1 = Math.Floor(lot);
                    lot_Allowed = Convert.ToInt32(floor1);


                    //ConvertServerDate ServerDate = new ConvertServerDate();
                    string strselect = "insert into PM_FDR_and_Cheque_Master (FDR_ChequeID, CropYear, Miller_Dist, MillerID, One_LotAmount, MinFDR, FDR_BankID, FDR_IFSC, FDR_Date, FDR_Value, FDR_Maturity, Max_Cheque, CHeque_bankID, Cheque_IFSC, Cheque_date, Cheque_Value, Lots_Allowed, CreatedDate, IP, Updated, FDR_OR_BankSecu, FDR_Number, Check_Number, State_Code, SubDist) values('" + gatepass + "', '" + ddlCropYear.SelectedValue.ToString() + "', '" + DistCode + "','" + ddlMillName.SelectedValue.ToString() + "','" + LOTamt + "','" + lblMinFDR.Text + "','" + ddlfdrbank.SelectedValue.ToString() + "','" + txtfdrifsc.Text + "','" + FdrDATE + "','" + txtvalueFDR.Text + "','" + MaturityFdrDATE + "','" + lblCheck.Text + "','" + ddlcheckbank.SelectedValue.ToString() + "','" + txtcheckifsc.Text + "','" + chequeDate + "','" + txtvalueCheck.Text + "','" + lot_Allowed + "',getdate(),'" + GetIp + "','N','" + ddlFDRBankSecurity.SelectedValue.ToString() + "','" + txtBankFDRNumber.Text + "', '" + txtChequeNumber.Text + "', '" + hdfStateCode.Value + "','" + hdfMillDist.Value + "')";
                    //string instr = "";
                    //instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                    //                   "insert into PM_RatioMaster_LOG select * from PM_RatioMaster as RM  where RM.CropYear='" + ddlCropYear.SelectedValue.ToString() + "'  and ParticularMiller!='Y' ; ";

                    //instr += "update PM_RatioMaster set Total='" + txttotal.Text + "', FDR='" + txtfdr.Text + "', Checks='" + txtcheck.Text + "', IP='" + GetIp + "', CreatedDate=getdate(), ParticularMiller='N', Lot_amount='" + txtlot.Text + "',Frm_Date='" + IssuedFromDate + "', To_Date='" + IssuedToDate + "'  where CropYear='" + ddlCropYear.SelectedValue.ToString() + "' and ParticularMiller!='Y'; ";

                    //instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                    cmd = new SqlCommand(strselect, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully for Crop Year  " + ddlCropYear.SelectedValue.ToString() + "'); </script> ");
                        ddlCropYear.Enabled = false;
                        ddlcheckbank.Enabled = false;
                        ddlMillName.Enabled = false;
                        ddlfdrbank.Enabled = false;
                        ddlFDRBankSecurity.Enabled = false;
                        txtCheckDate.Enabled = false;
                        txtfdrDate.Enabled = false;
                        txtfdrifsc.Enabled = false;
                        txtcheckifsc.Enabled = false;
                        txtvalueCheck.Enabled = false;
                        txtvalueFDR.Enabled = false;
                        txtMaturityofFDR.Enabled = false;
                        txtChequeNumber.Enabled = false;
                        txtBankFDRNumber.Enabled = false;
                        bttsubmit.Enabled = false;

                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                }

                finally
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
        }

    }

    protected void txtBankFDRNumber_TextChanged(object sender, EventArgs e)
    {
        CheckFDRinFDR();
        if (txtBankFDRNumber.Text == "")
        {
            return;
        }

        else if (txtBankFDRNumber.Text != "")
        {
            CheckFDRinCheque();
            if (txtBankFDRNumber.Text == "")
            {
                return;
            }
            else if (txtBankFDRNumber.Text != "")
            {
                CheckFDRinChequeAdditional();
            }

        }




    }

    public void CheckFDRinFDR()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_FDR_and_Cheque_Master where FDR_Number='" + txtBankFDRNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये FDR/ Bank Security नंबर पहले से ही उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtBankFDRNumber.Text = "";
                    txtBankFDRNumber.Focus();
                    return;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void CheckFDRinCheque()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_FDR_and_Cheque_Master where Check_Number='" + txtBankFDRNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये FDR/ Bank Security नंबर पहले से ही चेक में उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtBankFDRNumber.Text = "";
                    txtBankFDRNumber.Focus();
                    return;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void CheckFDRinChequeAdditional()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_Add_ChequeAgainstFDR where Cheque_Number='" + txtBankFDRNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये चेक नंबर पहले से ही उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtBankFDRNumber.Text = "";
                    txtBankFDRNumber.Focus();
                    return;


                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void txtChequeNumber_TextChanged(object sender, EventArgs e)
    {
        CheckChequeinFDR();


        if (txtChequeNumber.Text == "")
        {
            return;
        }

        else if (txtChequeNumber.Text != "")
        {
            CheckChequeincheque();

            if (txtChequeNumber.Text == "")
            {
                return;
            }
            else if (txtChequeNumber.Text != "")
            {
                CheckChequeinAdditional();
            }

        }




    }

    public void CheckChequeinFDR()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_FDR_and_Cheque_Master where FDR_Number='" + txtChequeNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये चेक नंबर पहले से ही FDR में उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtChequeNumber.Text = "";
                    txtChequeNumber.Focus();
                    return;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void CheckChequeincheque()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_FDR_and_Cheque_Master where Check_Number='" + txtChequeNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये चेक नंबर पहले से ही उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtChequeNumber.Text = "";
                    txtChequeNumber.Focus();
                    return;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
    public void CheckChequeinAdditional()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select * from  PM_Add_ChequeAgainstFDR where Cheque_Number='" + txtChequeNumber.Text + "'";


                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('ये चेक नंबर पहले से ही उपलभ है इसलिए ये दुबारा जमा नहीं होगा|'); </script> ");
                    txtChequeNumber.Text = "";
                    txtChequeNumber.Focus();
                    return;

                }
                else
                {

                }
            }

            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}
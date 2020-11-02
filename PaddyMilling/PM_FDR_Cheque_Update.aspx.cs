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

public partial class PaddyMilling_PM_FDR_Cheque_Update : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da, da1, daI, daCMR, daSMO;
    DataSet ds, ds1, dsI, dsCMR, dsSMO;
    string SMO;
    public string gatepass = "";

    public int getnum;
    //string Gdistance = "";
    //string Mdistance = "";


    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                Session["ICGBQ"] = null;

                // txtDistManager.Text = Session["dist_name"].ToString();
                txtMillDist.Text = Session["dist_name"].ToString();
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                string Chequedate = Request.Form[txtCheckDate.UniqueID];
                txtCheckDate.Text = Chequedate;
                GetCropYearValues();
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
            GetChequeDetails();
            //tr2.Visible = true;
            tr4.Visible = false ;
            //chkbalcheque.Checked = false;
            Fillgrid();
           
            // GetBankName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Miller Name'); </script> ");
            return;


        }
    }
    //public void GetMillerData()
    //{
    //    string DistCode = Session["dist_id"].ToString();

    //    //string DistCode = Session["dist_id"].ToString();

    //    using (con = new SqlConnection(strcon))
    //    {
    //        try
    //        {
    //            string select = "";
    //            //Only For Agrmt Dist & Miller Dist.
    //            // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

    //            //Agrmt Dist & Miller Dist & CMR Map. Dist

    //            da = new SqlDataAdapter(select, con);
    //            ds = new DataSet();
    //            da.Fill(ds);

    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //            {


    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने कोई भी FDR जमा नहीं की है जिसको वो अपडेट कर सके|'); </script> ");
    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //        }

    //        finally
    //        {
    //            if (con.State != ConnectionState.Closed)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}
    void Fillgrid()
    {

        string DistCode = Session["dist_id"].ToString();
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();

            string select = "select FDR_ChequeID, FDR_Number, FDR_Value, Check_Number, Cheque_Value, FDR_OR_BankSecu from PM_FDR_and_Cheque_Master where MillerID='" + ddlMillName.SelectedValue.ToString() + "' and Miller_Dist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(select, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                tr4.Visible = true;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर ने कोई भी FDR जमा नहीं की है जिसको वो अपडेट कर सके|'); </script> ");
                tr4.Visible = false;
                return;
            }
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }



    public void GetChequeDetails()
    {
        decimal FDRratio, ChequeRatio, TFDR, TCheque, cheque, AddedCheque, BalanceCheque, SumCheque;
        string DistCode = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "select isnull(sum(isnull(FDR_Value,0)),0) as FDR_Value, isnull(sum(isnull(Cheque_Value,0)),0) as Cheque_Value  from PM_FDR_and_Cheque_Master where MillerID='" + ddlMillName.SelectedValue.ToString() + "' and Miller_Dist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalFDR.Visible = true;
                    lblTotalFDR.Text = ds.Tables[0].Rows[0]["FDR_Value"].ToString();
                    lblTotalCheque.Visible = true;
                    lblTotalCheque.Text = ds.Tables[0].Rows[0]["Cheque_Value"].ToString();

                    
                   
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data is not available'); </script> ");
                }

                string qry = "select isnull(SUM(isnull(ChequeValue,0)),0) as ChequeValue  from PM_Add_ChequeAgainstFDR where Miller_Dist='" + DistCode + "' and Miller_ID='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                     da = new SqlDataAdapter(qry, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblAddChequVal.Visible = true;
                    lblAddChequVal.Text = ds.Tables[0].Rows[0]["ChequeValue"].ToString();
                }
                

                FDRratio = Convert.ToDecimal(lblMinFDR.Text);
                ChequeRatio = Convert.ToDecimal(lblCheck.Text);
                TFDR = Convert.ToDecimal(lblTotalFDR.Text);
                TCheque = Convert.ToDecimal(lblTotalCheque.Text);
                AddedCheque = Convert.ToDecimal(lblAddChequVal.Text);
                SumCheque = TCheque + AddedCheque;

                cheque = TFDR * ChequeRatio;
                BalanceCheque = cheque - SumCheque;
                if (BalanceCheque == 0)
                {
                    tr6.Visible = false ;
                    tr19.Visible = false;
                }
                else
                {
                    tr6.Visible = true;
                    tr19.Visible = true;
                }
                lblBalancecheque.Visible = true;
                lblBalancecheque.Text = Convert.ToString(BalanceCheque);
                

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

                    select = "select FDR, checks, LOT_Amount, State_Code, MillerDist from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and (MillerDist='" + DistCode + "' or   MillerDist in ('505','506','507','508','509'))  and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
                }
                else if (DistCode != "38")
                {
                    select = "select FDR, checks, LOT_Amount, State_Code, MillerDist from PM_RatioMaster where Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and MillerDist='" + DistCode + "' and CropYear='" + ddlCropYear.SelectedValue.ToString() + "'";
         
                }
                    
                    //Only For Agrmt Dist & Miller Dist.
                // select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where (PM.District='" + DistCode + "' or PM.Mill_Addr_District='" + DistCode + "') and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

                //Agrmt Dist & Miller Dist & CMR Map. Dist

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //txtLot.Text = ds.Tables[0].Rows[0]["LOT_Amount"].ToString();
                    lblMinFDR.Visible = true;
                    lblCheck.Visible = true;
                    lblMinFDR.Text = ds.Tables[0].Rows[0]["FDR"].ToString();
                    lblCheck.Text = ds.Tables[0].Rows[0]["checks"].ToString();
                    // txtBankFDRNumber.Enabled = true;
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

                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Ratio for FDR and Checks is not available for this miller'); </script> ");
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

    protected void bttsubmit_Click(object sender, EventArgs e)
    {
        //string DistCode = Session["dist_id"].ToString();
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
        else if (lblMinFDR.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Ratio of FDR is not available'); </script> ");
            
            return;
        }
        else if (lblCheck.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Ratio of Cheque is not available'); </script> ");

            return;
        }
           // ---------------
        else if (lblTotalFDR.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Total Value of FDR is not available'); </script> ");

            return;
        }
        else if (lblTotalCheque.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Total Value of Cheque is not available'); </script> ");

            return;
        }
        else if (lblBalancecheque.Text=="")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Balance Value of cheque is not available'); </script> ");

            return;
        }
       
        else if (ddlcheckbank.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Bank Name'); </script> ");
            return;
        }
       
        else if (txtcheckifsc.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Bank IFSC Code'); </script> ");
            txtcheckifsc.Focus();
            return;
        }
       
        else if (txtCheckDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Date'); </script> ");
            return;
        }
       
        else if (txtvalueCheck.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select value Cheque '); </script> ");
            txtvalueCheck.Focus();
            return;
        }
       
       
        else if (txtChequeNumber.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please select Cheque Number'); </script> ");
            txtChequeNumber.Focus();
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

                    
                    ConvertServerDate ServerChequeDate = new ConvertServerDate();
                    string chequeDate = ServerChequeDate.getDate_MDY(txtCheckDate.Text);

                    string qrey = "select max(FDRCheque_ID) as FDRCheque_ID from PM_Add_ChequeAgainstFDR where LEN(FDRCheque_ID)<20 ";
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
                    gatepass = ds.Tables[0].Rows[0]["FDRCheque_ID"].ToString();

                    if (gatepass == "")
                    {
                        gatepass = "1701800";
                    }
                    else
                    {
                        getnum = Convert.ToInt32(gatepass);
                        //getnum = gatepass;
                        getnum = getnum + 1;
                        gatepass = getnum.ToString();
                    }



                   

                    //ConvertServerDate ServerDate = new ConvertServerDate();
                    string strselect = "insert into PM_Add_ChequeAgainstFDR (FDRCheque_ID, Miller_Dist, Miller_ID, FDR_Ratio ,Cheque_Ratio, Total_FDR, TotaL_Cheque, BalCheque_Qty, SubmitBal,ChequeBank, Cheque_IFSC, Cheque_Number, DateCheque, ChequeValue, CreatedDate, IP, CropYear, State_Code, SubDist) values('" + gatepass + "', '" + DistCode + "','" + ddlMillName.SelectedValue.ToString() + "','" + lblMinFDR.Text + "','" + lblCheck.Text + "','" + lblTotalFDR.Text + "','" + lblTotalCheque.Text + "','" + lblBalancecheque.Text + "','Y','" + ddlcheckbank.SelectedValue.ToString() + "','" + txtcheckifsc.Text + "','" + txtChequeNumber.Text + "','" + chequeDate + "','" + txtvalueCheck.Text + "',getdate(),'" + GetIp + "','" + ddlCropYear.SelectedValue.ToString() + "', '" + hdfStateCode.Value + "','" + hdfMillDist.Value + "')";
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
                        ddlcheckbank.Enabled = false;
                        txtcheckifsc.Enabled = false;
                        txtChequeNumber.Enabled = false;
                        txtCheckDate.Enabled = false;
                        txtvalueCheck.Enabled = false;
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
    protected void chkbalcheque_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbalcheque.Checked == true)
        {
            tr8.Visible = true;
            tr13.Visible = true;
            tr9.Visible = true;
            tr10.Visible = true;
            tr11.Visible = true;
            tr12.Visible = true;
            tr14.Visible = true;
            tr15.Visible = true;
            tr16.Visible = true;
            tr17.Visible = true;
            tr18.Visible = true;
            bttsubmit.Enabled = true;
            GetBankName();

        }
        else if (chkbalcheque.Checked == false)
        {
            tr8.Visible = false;
            tr13.Visible = false;
            tr9.Visible = false;
            tr10.Visible = false;
            tr11.Visible = false;
            tr12.Visible = false;
            tr14.Visible = false;
            tr15.Visible = false;
            tr16.Visible = false;
            tr17.Visible = false;
            tr18.Visible = false;
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


                    ddlcheckbank.DataSource = ds.Tables[0];
                    ddlcheckbank.DataTextField = "Bank_Name";
                    ddlcheckbank.DataValueField = "Bank_ID";
                    ddlcheckbank.DataBind();
                    ddlcheckbank.Items.Insert(0, "--Select--");


                    txtcheckifsc.Enabled = true;

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
    protected void txtvalueCheck_TextChanged(object sender, EventArgs e)
    {
        //lblTotalFDR.Visible = true;
        //lblTotalFDR.Text = ds.Tables[0].Rows[0]["FDR_Value"].ToString();
        //lblTotalCheque.Visible = true;
        //lblTotalCheque.Text = ds.Tables[0].Rows[0]["Cheque_Value"].ToString();

        decimal FDRratio, ChequeRatio, TFDR, TCheque, cheque, BalanceCheque, AddedCheque, SumCheque;
        FDRratio = Convert.ToDecimal(lblMinFDR.Text);
        ChequeRatio = Convert.ToDecimal(lblCheck.Text);
        TFDR = Convert.ToDecimal(lblTotalFDR.Text);
        TCheque = Convert.ToDecimal(lblTotalCheque.Text);
        AddedCheque = Convert.ToDecimal(lblAddChequVal.Text);
        SumCheque = TCheque + AddedCheque;

        cheque = TFDR * ChequeRatio;
        BalanceCheque = cheque - SumCheque;
        lblBalancecheque.Visible = true;
        lblBalancecheque.Text = Convert.ToString(BalanceCheque);

        if (Convert.ToInt32(txtvalueCheck.Text) <= BalanceCheque && Convert.ToInt32(txtvalueCheck.Text) >= 0)
        {

        }
        else
        {
            trmessage.Visible = true;
            trmessageSpace.Visible = true;
            lblmessage.Visible = true;
            lblmessage.Text = "चेक की अधिकतम राशि .." + BalanceCheque + " ..से जायदा नहीं हो सकती अथवा 0 हो सकती है| ";
            txtvalueCheck.Text = "";
            txtvalueCheck.Focus();
            return;
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

            CheckChequeinCheque();
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
    public void CheckChequeinCheque()
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
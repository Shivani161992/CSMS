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

public partial class PaddyMilling_Receipt_CMR_FCI : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                txtAcptNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtAcptNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtAcptNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtWeightMemo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtWeightMemo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtWeightMemo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRecdQty.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRecdQty.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRecdQty.Attributes.Add("onchange", "return chksqltxt(this)");

                txtBags.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtBags.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtBags.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNo0.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo0.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo0.Attributes.Add("onchange", "return chksqltxt(this)");

                txtWhrNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtWhrNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtWhrNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtDistManager.Text = Session["dist_name"].ToString();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                rdbNewJute.Checked = true;
                GetCropYearValues();
                GetMillName();
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
        txtMemoDate.Text = Request.Form[txtMemoDate.UniqueID];
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
                    txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
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

    public void GetMillName()
    {
        ddlMillName.Items.Clear();

        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";
                //Only FCI Miller.
                select = "Select distinct MA.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As MA Left Join Miller_Registration_2017 MR ON(MA.Mill_Name=MR.Registration_ID and MA.Mill_Addr_District=MR.District_Code and MA.CropYear=MR.CropYear) where MA.State_Code!=23 and MA.District='" + DistCode + "' and MA.CropYear='" + txtYear.Text + "' and MA.IsAccepted='Y' and MA.DeliverdToFCI='Y' order by MillName Asc";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlMillName.DataSource = ds.Tables[0];
                    ddlMillName.DataTextField = "MillName";
                    ddlMillName.DataValueField = "MillCode";
                    ddlMillName.DataBind();
                    ddlMillName.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके जिले से किसी भी मिलर ने अनुबंध नहीं किया हैं, इसलिए मिल का नाम उपलब्ध नहीं है|'); </script> ");
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
        string DistCode = Session["dist_id"].ToString();

        lbllot.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAllQty.Value = hdfAdjustCMRDO.Value = hdfFCIState.Value = "";
        txtBags.Text = txtRecdQty.Text = txtTruckNo.Text = txtTruckNo0.Text = txtFromDate.Text = txtMemoDate.Text = txtAcptNo.Text = txtWeightMemo.Text = txtWhrNo.Text = "";
        txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = ddlCMRDist.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled = txtWhrNo.Enabled = false;
        btnRecptSubmit.Enabled = false;

        ddlCMRDist.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlLotNO.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";
                    //Only For FCI Miller's.
                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where State_Code!=23 and  District='" + DistCode + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' and DeliverdToFCI='Y' order by Agreement_ID";

                    da = new SqlDataAdapter(select, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlAgtmtNumber.DataSource = ds.Tables[0];
                        ddlAgtmtNumber.DataTextField = "Agreement_ID";
                        ddlAgtmtNumber.DataValueField = "Agreement_ID";
                        ddlAgtmtNumber.DataBind();
                        ddlAgtmtNumber.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जिस मिल का चुनाव किया है, उसके लिए कोई भी अनुबंध नंबर उपलब्ध नहीं हैं|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया मिल का नाम चुनें|'); </script> ");
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

    protected void ddlAgtmtNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbllot.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAdjustCMRDO.Value = hdfFCIState.Value = "";
        txtBags.Text = txtRecdQty.Text = txtTruckNo.Text = txtTruckNo0.Text = txtFromDate.Text = txtMemoDate.Text = txtAcptNo.Text = txtWeightMemo.Text = txtWhrNo.Text = "";
        txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = ddlCMRDist.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled  = txtWhrNo.Enabled= false;
        btnRecptSubmit.Enabled = false;
        ddlLotNO.Items.Clear();
        ddlCMRDist.Items.Clear();

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetLotNO();
            GetFCIState();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया अनुबंध नंबर चुनें|'); </script> ");
        }
    }

    public void GetLotNO()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct DhanLot, 'Lot' + cast(DhanLot as varchar) As LotNO From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO where CropYear='" + txtYear.Text + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + txtYear.Text + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From Receipt_CMR_FCI Where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlLotNO.DataSource = ds.Tables[0];
                    ddlLotNO.DataTextField = "LotNO";
                    ddlLotNO.DataValueField = "DhanLot";
                    ddlLotNO.DataBind();
                    ddlLotNO.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर के लिए DO के अनुसार कोई भी लॉट नंबर उपलब्ध नहीं है|'); </script> ");
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

    public void GetFCIState()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select State_Code From PaddyMilling_Agreement_2017 Where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfFCIState.Value = ds.Tables[0].Rows[0]["State_Code"].ToString();


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

    public void GetFCIDist()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "SELECT district_code ,district_name FROM OtherState_DistrictCode where State_Id = '" + hdfFCIState.Value + "'  order by district_name ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlCMRDist.DataSource = ds.Tables[0];
                    ddlCMRDist.DataTextField = "district_name";
                    ddlCMRDist.DataValueField = "district_code";
                    ddlCMRDist.DataBind();
                    ddlCMRDist.Items.Insert(0, "--Select--");
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

    protected void ddlLotNO_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnRecptSubmit.Enabled = false;
        lbllot.Text = lblDOQty.Text = lblDORem.Text = lblDONO.Text = lblMillingType.Text = "";
        hdfAgrmtDist.Value = hdfAllQty.Value = hdfAdjustCMRDO.Value = "";
        txtBags.Text = txtRecdQty.Text = txtTruckNo.Text = txtTruckNo0.Text = txtFromDate.Text = txtMemoDate.Text = txtAcptNo.Text = txtWeightMemo.Text = txtWhrNo.Text = "";
        txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = ddlCMRDist.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled = txtWhrNo.Enabled = false;
        ddlCMRDist.Items.Clear();

        if (ddlLotNO.SelectedIndex > 0)
        {
            GetLotData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया लॉट चुनें|'); </script> ");
        }
    }

    public void GetLotData()
    {
        string districtid = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                //string select = "Select SUM(Isnull(Return_CommonRice,0)) As LotQty,(403-SUM(Isnull(Return_CommonRice,0))) As RemLotQty,Max(Check_DO) As Check_DO,MAX(District) As AgrmtDist From PaddyMilling_DO where District=(Select distinct District From PaddyMilling_DO where CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + txtYear.Text + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From CMR_DepositOrder Where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and ((IsAccepted='Y') OR (IsAccepted='N' and IsRejected='N'))) ";

                string select = "Select SUM(Isnull(Return_CommonRice,0)) As LotQty,MAX(Milling_Type) As Milling_Type,Max(Check_DO) As Check_DO,MAX(District) As AgrmtDist,(Case When MAX(Milling_Type)=N'अरवा' Then (403-SUM(Isnull(Return_CommonRice,0))) Else (397-SUM(Isnull(Return_CommonRice,0))) End) As RemLotQty From PaddyMilling_DO_2017 where District=(Select distinct District From PaddyMilling_DO_2017 where CropYear='" + txtYear.Text + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N') and CropYear='" + txtYear.Text + "' and Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and DispatchDhan_IC='N' and DhanLot Not IN (Select Lot_No From Receipt_CMR_FCI Where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and IsAccepted='Y' ) ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lbllot.Text = ddlLotNO.SelectedValue.ToString();
                    lblDOQty.Text = ds.Tables[0].Rows[0]["LotQty"].ToString();
                    lblMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    lblDORem.Text = ds.Tables[0].Rows[0]["RemLotQty"].ToString();
                    lblDONO.Text = ds.Tables[0].Rows[0]["Check_DO"].ToString();
                    float Remlotqty = float.Parse(ds.Tables[0].Rows[0]["RemLotQty"].ToString());
                    hdfAgrmtDist.Value = ds.Tables[0].Rows[0]["AgrmtDist"].ToString();

                    if (Remlotqty <= 1)
                    {
                        btnRecptSubmit.Enabled = true;
                        txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = ddlCMRDist.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled = txtWhrNo.Enabled = true;
                    }
                    else
                    {
                        string select1 = "";
                        select1 = "Select * From Paddy_Adjust_DO where CropYear='" + txtYear.Text + "' and Mill_ID='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and DO_No='" + lblDONO.Text + "' and Lot_No='" + lbllot.Text + "'";
                        da1 = new SqlDataAdapter(select1, con);
                        ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            hdfAdjustCMRDO.Value = "1";
                            btnRecptSubmit.Enabled = true;
                            txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = ddlCMRDist.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled = txtWhrNo.Enabled = true;
                        }
                        else
                        {
                            btnRecptSubmit.Enabled = false;
                            hdfAllQty.Value = "1";
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा धान नहीँ उठाया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
                            return;
                        }
                    }

                    if (hdfFCIState.Value != "")
                    {
                        GetFCIDist();
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस मिलर के लिए DO के अनुसार कोई भी लॉट नंबर उपलब्ध नहीं है|'); </script> ");
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

    protected void btnRecptSubmit_Click1(object sender, EventArgs e)
    {
        decimal CheckRecdQty = 0;
        int CheckBag = 0;

        if (txtRecdQty.Text != "" && txtBags.Text != "")
        {
            CheckRecdQty = decimal.Parse(txtRecdQty.Text);
            CheckBag = int.Parse(txtBags.Text);
        }

        if (ddlLotNO.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर को प्रदायित धान का लॉट नंबर चुने|'); </script> ");
            return;
        }
        else if (txtFromDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Acceptance Note Date'); </script> ");
            return;
        }
        else if (txtMemoDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Weight Check Memo Date'); </script> ");
            return;
        }
        else if (hdfAllQty.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('मिलर ने इस लॉट नंबर के विरुद्ध पूरा धान नहीँ उठाया है, इसलिए इस लॉट नंबर के विरुद्ध CMR जमा नहीं किया जा सकता|'); </script> ");
            return;
        }
        else if (CheckRecdQty < 269 && hdfAdjustCMRDO.Value == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Qty की मात्रा अनुमानित मात्रा से कम होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (CheckBag < 535 && hdfAdjustCMRDO.Value == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Bags की मात्रा अनुमानित मात्रा से कम होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (CheckRecdQty >= 273)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Recd. CMR Qty की मात्रा अनुमानित मात्रा से ज्यादा होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (ddlCMRDist.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Recd. CMR District'); </script> ");
            return;
        }
        else if (txtAcptNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Acceptance Note No.'); </script> ");
            return;
        }
        else if (txtWeightMemo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Weight Check Memo No.'); </script> ");
            return;
        }
        else
        {
            string districtid = Session["dist_id"].ToString();

            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        ClientIP objClientIP = new ClientIP();
                        string GetIp = objClientIP.GETIP();

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string ConvertFromDate = ServerDate.getDate_MDY(txtFromDate.Text);
                        string ConvertMemoDate = ServerDate.getDate_MDY(txtMemoDate.Text);

                        string SubDCDate = "", DCDate = "", instr = "", CMRDO_ID = "", BagType = "";

                        if (rdbNewJute.Checked)
                        {
                            BagType = "9";
                        }
                        else if (rdbOldJute.Checked)
                        {
                            BagType = "10";
                        }
                        else if (rdbOnceJute.Checked)
                        {
                            BagType = "11";
                        }
                        else if (rdbNewPP.Checked)
                        {
                            BagType = "4";
                        }
                        else if (rdbOncePP.Checked)
                        {
                            BagType = "2";
                        }
                        else
                        {
                            BagType = "12";
                        }

                        con.Open();

                        string AcptNo = "Select * from Receipt_CMR_FCI where CMR_RecdState='" + hdfFCIState.Value + "' and  CMR_RecdDist='" + ddlCMRDist.SelectedValue.ToString() + "' and Acceptance_No='" + txtAcptNo.Text + "'";
                        da1 = new SqlDataAdapter(AcptNo, con);
                        ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Acceptance Note No. Already Exist On Your District...'); </script> ");
                            return;
                        }
                        else
                        {
                            string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                            da = new SqlDataAdapter(selectmax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                SubDCDate = DCDate.Substring(2);
                            }

                            if (SubDCDate != "")
                            {
                                decimal RecdCMR = decimal.Parse(txtRecdQty.Text);

                                CMRDO_ID = "CMRAF" + SubDCDate;
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='Y' where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and District='" + hdfAgrmtDist.Value + "' and CropYear='" + txtYear.Text + "' and DhanLot='" + ddlLotNO.SelectedValue.ToString() + "' and Check_DO='" + lblDONO.Text + "'; ";

                                instr += "Update PaddyMilling_Agreement_2017 set Return_CommonRice= (ISNULL(Return_CommonRice,0)+" + RecdCMR + "), Return_TotalRice= (ISNULL(Return_TotalRice,0)+" + RecdCMR + ") where Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' ; ";

                                instr += "Insert Into Receipt_CMR_FCI(Paddy_AgrmtDist,CMR_RecdDist,CMR_RecdState,Receipt_ID,Receipt_Date,CommonRice,GradeARice,Bags,Truck_No,Truck_No1,CropYear,Mill_ID,Milling_Type,Agreement_ID,Lot_No,DO_No,CreatedDate,IP_Address,IsAccepted,Acceptance_No,WeightCheck_No,WeightCheck_Date,BagType,WHR_No) values('" + hdfAgrmtDist.Value + "','" + ddlCMRDist.SelectedValue.ToString() + "','" + hdfFCIState.Value + "','" + CMRDO_ID + "','" + ConvertFromDate + "','" + RecdCMR + "','0','" + CheckBag + "','" + txtTruckNo.Text + "','" + txtTruckNo0.Text + "','" + txtYear.Text + "','" + ddlMillName.SelectedValue.ToString() + "',N'" + lblMillingType.Text + "','" + ddlAgtmtNumber.SelectedItem.ToString() + "','" + ddlLotNO.SelectedValue.ToString() + "','" + lblDONO.Text + "',GETDATE(),'" + GetIp + "','Y','" + txtAcptNo.Text + "','" + txtWeightMemo.Text + "','" + ConvertMemoDate + "','" + BagType + "','"+txtWhrNo.Text.Trim()+"'); ";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnPrint.Enabled = true;
                                btnRecptSubmit.Enabled = false;

                                Label2.Visible = true;
                                Label2.Text = "Your CMR Receipt Number Is : " + CMRDO_ID;

                                Session["CMRDO_ID"] = CMRDO_ID.ToString();

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Saved Successfully and Your CMR Receipt Number Is " + CMRDO_ID.ToString() + "'); </script> ");
                                txtYear.Text = txtDistManager.Text = "";
                                ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlLotNO.Enabled = ddlCMRDist.Enabled = false;
                                txtBags.Enabled = txtRecdQty.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = txtAcptNo.Enabled = txtWeightMemo.Enabled = txtWhrNo.Enabled = false;

                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        btnRecptSubmit.Enabled = false;
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
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "Print/PrintReceipt_CMR_FCI.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
}
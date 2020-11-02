using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccess;
using Data;


public partial class IssueCenter_Receipt_Entry_CMR : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd, cmd1, cmd2, cmd3;
    SqlDataAdapter da, da_MPStorage, da1;
    DataSet ds, ds_MPStorage, ds1;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString; //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage
    public SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()); //For IssueCentre

    public string ICID, DistId;
    public int BookOnlyNumber, Rejected_NumberOf_Times;
    public string strProximate_CommonRice, strProximate_GradeARice, strReturn_CommonRice, strReturn_GradeARice, strPartyname, strAgreement_ID, Daane;
    public string strRProximate_CommonRice, strRProximate_GradeARice, strRReturn_CommonRice, strRReturn_GradeARice;
    public string strDOReturn_CommonRice, strDOReturn_GradeARice, strDOReturn_TotalRice;
    public string strAgrmtReturn_CommonRice, strAgrmtReturn_GradeARice, stragrmtReturn_TotalRice;

    MoveChallan mobj = null;
    protected Common ComObj = null;
    DistributionCenters distobj = null;

    public string districtid = "", IC_Id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["issue_id"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            distobj = new DistributionCenters(ComObj);

            if (!IsPostBack)
            {
                ICID = Session["issue_id"].ToString();
                DistId = Session["dist_id"].ToString();
                // rdbNewJute.Checked =
                rdbTagYes.Checked = true;

                txtTruckNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo.Attributes.Add("onchange", "return chksqltxt(this)");

                txtTruckNo0.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtTruckNo0.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtTruckNo0.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkTota.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkTota.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkTota.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkCTote.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkCTote.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkCTote.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkVijatiye.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkVijatiye.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkVijatiye.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkDaane.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkDaane.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkDaane.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkBadrang.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkBadrang.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkBadrang.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkChaki.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkChaki.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkChaki.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkLaal.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkLaal.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkLaal.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkShreni.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkShreni.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkShreni.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkChokar.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkChokar.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkChokar.Attributes.Add("onchange", "return chksqltxt(this)");

                txtRmkNami.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtRmkNami.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtRmkNami.Attributes.Add("onchange", "return chksqltxt(this)");

                txtToulReceiptNo.Attributes.Add("onkeypress", "return checksqlkey_gen(event,this)");
                txtToulReceiptNo.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
                txtToulReceiptNo.Attributes.Add("onchange", "return chksqltxt(this)");

                GetCropYearValues();


                GetBookNumber();
                rbCDaane.Checked = true;
                GetInspector();
                GetBagsType();


                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }

        string fromdate = Request.Form[txtDate.UniqueID];
        txtDate.Text = fromdate;
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

                    ddlCropyear.DataSource = ds.Tables[0];
                    ddlCropyear.DataTextField = "CropYear";
                    ddlCropyear.DataValueField = "CropYear";
                    ddlCropyear.DataBind();
                    ddlCropyear.Items.Insert(0, "--Select--");
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
    protected void ddlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlCMRDONo.Items.Clear();
        if (ddlCropyear.SelectedIndex > 0)
        {
            GetCropYearValuesparameters();
            GetBranch();
        }
        else
        {
            return;

        }

    }

    public void GetBagsType()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format(" select Bag_Type_ID, BagType from FIN_Bag_Type where Bag_Type_ID!='4' order by BagType desc");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();

                    ddlbagstype.DataSource = ds.Tables[0];
                    ddlbagstype.DataTextField = "BagType";
                    ddlbagstype.DataValueField = "Bag_Type_ID";
                    ddlbagstype.DataBind();
                    ddlbagstype.Items.Insert(0, "--Select--");
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
    public void GetCropYearValuesparameters()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("SELECT * FROM PaddyMilling_CropYear WHERE CropYear='2016-2017' order by CropYear");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds, "PaddyMilling_CropYear");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtYear.Text = ds.Tables[0].Rows[0]["CropYear"].ToString();
                    LblTotaGA.Text = ds.Tables[0].Rows[0]["TotaGA"].ToString();
                    LblTotaS.Text = ds.Tables[0].Rows[0]["TotaS"].ToString();
                    LblChoteToteGA.Text = ds.Tables[0].Rows[0]["ChoteToteGA"].ToString();
                    LblChoteToteS.Text = ds.Tables[0].Rows[0]["ChoteToteS"].ToString();
                    LblVijatiyeGA.Text = ds.Tables[0].Rows[0]["VijatiyeGA"].ToString();
                    LblVijatiyeS.Text = ds.Tables[0].Rows[0]["VijatiyeS"].ToString();
                    LblDamageDaaneGA.Text = ds.Tables[0].Rows[0]["DamageDaaneGA"].ToString();
                    LblDamageDaaneS.Text = ds.Tables[0].Rows[0]["DamageDaaneS"].ToString();
                    LblBadrangDaaneGA.Text = ds.Tables[0].Rows[0]["BadrangDaaneGA"].ToString();
                    LblBadrangDaaneS.Text = ds.Tables[0].Rows[0]["BadrangDaaneS"].ToString();
                    LblChaakiDaaneGA.Text = ds.Tables[0].Rows[0]["ChaakiDaaneGA"].ToString();
                    LblChaakiDaaneS.Text = ds.Tables[0].Rows[0]["ChaakiDaaneS"].ToString();
                    LblLaalDaaneGA.Text = ds.Tables[0].Rows[0]["LaalDaaneGA"].ToString();
                    LblLaalDaaneS.Text = ds.Tables[0].Rows[0]["LaalDaaneS"].ToString();
                    LblOtherGA.Text = ds.Tables[0].Rows[0]["OtherGA"].ToString();
                    LblOtherS.Text = ds.Tables[0].Rows[0]["OtherS"].ToString();
                    LblChokarDaaneGA.Text = ds.Tables[0].Rows[0]["ChokarDaaneGA"].ToString();
                    LblChokarDaaneS.Text = ds.Tables[0].Rows[0]["ChokarDaaneS"].ToString();
                    LblNamiGA.Text = ds.Tables[0].Rows[0]["NamiGA"].ToString();
                    LblNamiS.Text = ds.Tables[0].Rows[0]["NamiS"].ToString();
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

    public void GetBranch()
    {
        string Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.BranchID from MetaDataBranchWithIssueCenter inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=MetaDataBranchWithIssueCenter.BranchID where IssueCenterId='" + IC_Id + "'");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchID";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Issue Center'); </script> ");
                    }
                }
                else
                {
                    string select1 = string.Format("select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId= '23" + Dist_Id + "' order by DepotName");
                    da = new SqlDataAdapter(select1, con_MPStorage);

                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBranch.DataSource = ds.Tables[0];
                        ddlBranch.DataTextField = "DepotName";
                        ddlBranch.DataValueField = "BranchId";
                        ddlBranch.DataBind();
                        ddlBranch.Items.Insert(0, "--Select--");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();
        ddlCMRDONo.Items.Clear();

        hdfAdjustCMRDO.Value = "";

        if (ddlBranch.SelectedIndex > 0)
        {
            GetGodown();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया ब्रांच चुनें |'); </script> ");
        }
    }


    public void GetInspector()
    {

        IC_Id = Session["issue_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("select Inspector_ID,Inspector_Name from Inspector_Master_02017 where SpecialStatus='No' and GETDATE()>=Frmdate and GETDATE()<=ToDate and IssueCenter_code='" + IC_Id + "' order by Inspector_Name");
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_IC.DataSource = ds.Tables[0];
                        ddl_IC.DataTextField = "Inspector_Name";
                        ddl_IC.DataValueField = "Inspector_ID";
                        ddl_IC.DataBind();
                        ddl_IC.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select inspector name'); </script> ");
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




    public void GetGodown()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Remarks='Y' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' order by Godown_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlGodown.DataSource = ds.Tables[0];
                        ddlGodown.DataTextField = "Godown_Name";
                        ddlGodown.DataValueField = "Godown_ID";
                        ddlGodown.DataBind();
                        ddlGodown.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Branch Name'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnQuilityTested.Enabled = false;
        ddlCMRDONo.Items.Clear();
        chkCommon.Checked = false;
        CMRDODate.Text = txtLotNo.Text = txtAgrmtNo.Text = txtMillName.Text = txtAgrmtQty.Text = txtMillingType.Text = txtRemAgrmtCMRQty.Text = txtRecdQty.Text = txtBags.Text = txtTagNo.Text = txtTruckNo.Text = txtTruckNo0.Text = txtDate.Text = txtExpectedRice.Text = "";
        TxtTotaS.Text = TxtChoteToteS.Text = txtVijatiyeS.Text = txtDamageDaaneS.Text = txtBadrangDaaneS.Text = txtChaakiDaaneS.Text = txtLaalDaaneS.Text = txtOtherS.Text = txtChokarDaaneS.Text = txtNamiS.Text = txtToulReceiptNo.Text = "";
        hdfAdjustCMRDO.Value = "";
        ddlstackNumber.Items.Clear();
        if (ddlGodown.SelectedIndex > 0)
        {
           
            GetCMRDONumber();
            GetStackNumber();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Name'); </script> ");
        }
    }

    public void GetStackNumber()
    {


        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();
                string select = string.Format("select Stack_ID, Stack_Name  from tbl_MetaData_STACK where Stack_Killed='N' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' order by Stack_Name");
                da = new SqlDataAdapter(select, con_MPStorage);

                ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlstackNumber.DataSource = ds.Tables[0];
                        ddlstackNumber.DataTextField = "Stack_Name";
                        ddlstackNumber.DataValueField = "Stack_ID";
                        ddlstackNumber.DataBind();
                        ddlstackNumber.Items.Insert(0, "--Select--");

                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Stack Number Not Available'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }

            finally
            {
                if (con_MPStorage.State != ConnectionState.Closed)
                {
                    con_MPStorage.Close();
                }
            }
        }

    }


    public void GetCMRDONumber()
    {
        string Dist_Id = Session["dist_id"].ToString();
        IC_Id = Session["issue_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select CMR_DO From CMR_DepositOrder where CMR_RecdDist='" + Dist_Id + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and IssueCenter='" + IC_Id + "' and Branch='" + ddlBranch.SelectedValue.ToString() + "' and Godown_id='" + ddlGodown.SelectedValue.ToString() + "' and IsAccepted='N' and IsRejected='N'";
                da = new SqlDataAdapter(select, con);

                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCMRDONo.DataSource = ds.Tables[0];
                    ddlCMRDONo.DataTextField = "CMR_DO";
                    ddlCMRDONo.DataValueField = "CMR_DO";
                    ddlCMRDONo.DataBind();
                    ddlCMRDONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपके गोदाम में CMR Deposit Order No. उपलब्ध नहीं है|'); </script> ");
                    return;
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

    protected void ddlCMRDONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkCommon.Checked = false;
        hdfLotNO.Value = hdfMillID.Value = hdfDO_NO.Value = hdfPaddy_AgrmtDist.Value = hdfAdjustCMRDO.Value = "";
        CMRDODate.Text = txtLotNo.Text = txtAgrmtNo.Text = txtMillName.Text = txtAgrmtQty.Text = txtMillingType.Text = txtRemAgrmtCMRQty.Text = txtRecdQty.Text = txtBags.Text = txtTagNo.Text = txtTruckNo.Text = txtTruckNo0.Text = txtDate.Text = txtExpectedRice.Text = "";
        TxtTotaS.Text = TxtChoteToteS.Text = txtVijatiyeS.Text = txtDamageDaaneS.Text = txtBadrangDaaneS.Text = txtChaakiDaaneS.Text = txtLaalDaaneS.Text = txtOtherS.Text = txtChokarDaaneS.Text = txtNamiS.Text = txtToulReceiptNo.Text = "";
        btnQuilityTested.Enabled = false;

        if (ddlCMRDONo.SelectedIndex > 0)
        {
            GetCMRDOData();
            GETAdjustCMRDO();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया CMR Deposit Order No. का चुनाव करें|'); </script> ");
            return;
        }
    }

    public void GetCMRDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select CMRDO.Paddy_AgrmtDist,CMRDO.CMR_DODate,CMRDO.Lot_No,CMRDO.Agreement_ID,CMRDO.Mill_ID,CMRDO.DO_No,MR.Mill_Name,Agrmt.Total_Dhan,Agrmt.Milling_Type,Agrmt.Return_TotalRice,(Case when Agrmt.R_Ushna='0' then Agrmt.R_Arva else Agrmt.R_Ushna End) As ExpectedRice From CMR_DepositOrder As CMRDO Left Join Miller_Registration_2017 As MR ON(CMRDO.Mill_ID=MR.Registration_ID and MR.CropYear='" + ddlCropyear.SelectedValue.ToString() + "') left join PaddyMilling_Agreement_2017 As Agrmt ON(Agrmt.Agreement_ID=CMRDO.Agreement_ID)where CMR_DO='" + ddlCMRDONo.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfPaddy_AgrmtDist.Value = ds.Tables[0].Rows[0]["Paddy_AgrmtDist"].ToString();
                    hdfLotNO.Value = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    hdfMillID.Value = ds.Tables[0].Rows[0]["Mill_ID"].ToString();
                    hdfDO_NO.Value = ds.Tables[0].Rows[0]["DO_No"].ToString();

                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["CMR_DODate"].ToString());
                    CMRDODate.Text = DODate.ToString("dd/MMM/yyyy");

                    string lotNo = ds.Tables[0].Rows[0]["Lot_No"].ToString();
                    txtLotNo.Text = "Lot" + lotNo;

                    txtAgrmtNo.Text = ds.Tables[0].Rows[0]["Agreement_ID"].ToString();
                    txtMillName.Text = ds.Tables[0].Rows[0]["Mill_Name"].ToString();
                    txtAgrmtQty.Text = ds.Tables[0].Rows[0]["Total_Dhan"].ToString();
                    txtMillingType.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();
                    txtExpectedRice.Text = ds.Tables[0].Rows[0]["ExpectedRice"].ToString();

                    LblMType.Text = LblMType0.Text = LblMType1.Text = LblMType2.Text = LblMType3.Text = LblMType4.Text = LblMType5.Text = LblMType6.Text = LblMType7.Text = LblMType8.Text = ds.Tables[0].Rows[0]["Milling_Type"].ToString();

                    decimal ExpectedRice = decimal.Parse(txtExpectedRice.Text);
                    decimal RecdRice = decimal.Parse(ds.Tables[0].Rows[0]["Return_TotalRice"].ToString());

                    txtRemAgrmtCMRQty.Text = (ExpectedRice - RecdRice).ToString();

                    btnQuilityTested.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('CMR Deposit Order No. की जानकारी उपलब्ध नहीं है|'); </script> ");
                    return;
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

    public void GETAdjustCMRDO()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "Select * From Paddy_Adjust_DO where CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_ID='" + hdfMillID.Value + "' and Agreement_ID='" + txtAgrmtNo.Text + "' and DO_No='" + hdfDO_NO.Value + "' and Lot_No='" + hdfLotNO.Value + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfAdjustCMRDO.Value = "1";
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

    public void GetBookNumber()
    {
        string Dist_Id = Session["dist_id"].ToString();
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = string.Format("Select district_name From pds.districtsmp where district_code='" + Dist_Id + "'");
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDistrict.Text = ds.Tables[0].Rows[0]["district_name"].ToString();
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


    protected void btnAccept_Click(object sender, EventArgs e)
    {
        decimal CheckRecdQty = decimal.Parse(txtRecdQty.Text);
        int CheckBag = int.Parse(txtBags.Text);

        if (CheckRecdQty < 268 && hdfAdjustCMRDO.Value == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Qty की मात्रा अनुमानित मात्रा से कम होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (CheckBag < 530 && hdfAdjustCMRDO.Value == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Receiving Bags की मात्रा अनुमानित मात्रा से कम होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (CheckRecdQty >= 273)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Recd. CMR Qty की मात्रा अनुमानित मात्रा से ज्यादा होने के कारण आप CMR Receive नहीं कर सकते|'); </script> ");
            return;
        }
        else if (ddlstackNumber.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Stack Number'); </script> ");
            return;
        }
        else if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }
        else
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                using (con = new SqlConnection(strcon))
                {
                    try
                    {
                        string Dist_Id = Session["dist_id"].ToString();
                        IC_Id = Session["issue_id"].ToString();
                        string opid = Session["OperatorId"].ToString();

                        string browser = Request.Browser.Browser.ToString();
                        string version = Request.Browser.Version.ToString();
                        string useragent = browser + version;

                        ClientIP objClientIP = new ClientIP();
                        string GetIp = (objClientIP.GETIP());

                        ConvertServerDate ServerDate = new ConvertServerDate();
                        string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

                        string tags = "", BagType = "", gatepass = "", Rec_Jute_bags = "", Rec_OU_bags = "", Rec_PP_bags = "";

                        if (rbCDaane.Checked)
                        {
                            Daane = "Damage";
                        }
                        else
                        {
                            Daane = "MDamage";
                        }

                        if (rdbTagYes.Checked)
                        {
                            tags = "Y";
                        }
                        else
                        {
                            tags = "N";
                        }

                        //if (rdbNewJute.Checked)
                        //{
                        //    BagType = "9";
                        //}
                        //else if (rdbOldJute.Checked)
                        //{
                        //    BagType = "10";
                        //}
                        //else if (rdbOnceJute.Checked)
                        //{
                        //    BagType = "11";
                        //}
                        //else if (rdbNewPP.Checked)
                        //{
                        //    BagType = "4";
                        //}
                        //else if (rdbOncePP.Checked)
                        //{
                        //    BagType = "2";
                        //}
                        //else
                        //{
                        //    BagType = "12";
                        //}
                        if (ddlbagstype.SelectedValue.ToString() == "1")
                        {
                            
                             Rec_Jute_bags = "0";
                             Rec_OU_bags = "0";
                            Rec_PP_bags = txtBags.Text;
                        }

                        else if
                            (ddlbagstype.SelectedValue.ToString() == "2")
                        {

                            Rec_Jute_bags = txtBags.Text;
                            Rec_OU_bags = "0";
                            Rec_PP_bags = "0";
                        }
                        else if
                           (ddlbagstype.SelectedValue.ToString() == "3")
                        {
                            Rec_Jute_bags = "0";
                            Rec_OU_bags = txtBags.Text;
                            Rec_PP_bags = "0";
                        }


                        con.Open();

                        int IsAvailable = 0;
                        String CheckData = "";
                        CheckData = "Select * From CMR_QualityInspection where District='" + Dist_Id + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and Mill_Name='" + hdfMillID.Value + "' and Agreement_ID='" + txtAgrmtNo.Text + "' and LotNumber='" + hdfLotNO.Value + "' and Submited='Y'  and (IsStackAccepted='' OR IsStackAccepted is null) and  (IsStackRejected='' OR IsStackRejected is null)  ";
                        da = new SqlDataAdapter(CheckData, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            IsAvailable = 1;
                        }

                        if (IsAvailable == 0)
                        {
                            int month = int.Parse(DateTime.Today.Date.Month.ToString());
                            int year = int.Parse(DateTime.Today.Year.ToString());
                            long getnum = 0;

                            string qrey = "";
                            qrey = "select max(Receipt_id) as Receipt_id from dbo.tbl_Receipt_Details where Depot_id='" + IC_Id + "' and Dist_Id='" + Dist_Id + "'";
                            da = new SqlDataAdapter(qrey, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                gatepass = ds.Tables[0].Rows[0]["Receipt_id"].ToString();

                                if (gatepass == "")
                                {
                                    string issue = IC_Id.Substring(2, 5);
                                    gatepass = issue + month.ToString() + "001";

                                }
                                else
                                {
                                    getnum = Convert.ToInt64(gatepass);
                                    getnum = getnum + 1;
                                    gatepass = getnum.ToString();
                                }
                            }

                            string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                            da = new SqlDataAdapter(selectmax, con);
                            ds = new DataSet();
                            da.Fill(ds);

                            string DCDate = "", SubDCDate = "", RejNo = "", instr = "";

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                                SubDCDate = DCDate.Substring(2);
                            }

                            decimal RecdCMR = decimal.Parse(txtRecdQty.Text);
                            Decimal Variation = 270 - RecdCMR;

                            if (SubDCDate != "" && gatepass != "")
                            {
                                RejNo = "CMRA" + SubDCDate;
                                instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                            "Update PaddyMilling_Agreement_2017 set Return_CommonRice= (ISNULL(Return_CommonRice,0)+" + RecdCMR + "), Return_TotalRice= (ISNULL(Return_TotalRice,0)+" + RecdCMR + "),Rem_DhanLot=(Rem_DhanLot+1) where Agreement_ID='" + txtAgrmtNo.Text + "';";

                                instr += "Update CMR_DepositOrder set IsAccepted='Y' where CMR_DO='" + ddlCMRDONo.SelectedItem.ToString() + "';";

                                //DO_NO is CMR Deposit Order No 
                                instr += "Insert Into CMR_QualityInspection(District,issueCentre_code,CropYear,Book_Number,Date,Mill_Name,Milling_Type,DO_Number,Agreement_ID,LotNumber,Acceptance_No,Rejection_No,Truck_No,LD_No,TotaGA,TotaS,TotaRemark,ChoteToteGA,ChoteToteS,ChoteToteRemark,VijatiyeGA,VijatiyeS,VijatiyeRemark,DamageDaaneGA,DamageDaaneS,DamageDaaneRemark,BadrangDaaneGA,BadrangDaaneS,BadrangDaaneRemark,ChaakiDaaneGA,ChaakiDaaneS,ChaakiDaaneRemark,LaalDaaneGA,LaalDaaneS,LaalDaaneRemark,OtherGA,OtherS,OtherRemark,ChokarDaaneGA,ChokarDaaneS,ChokarDaaneRemark,NamiGA,NamiS,NamiRemark,IP_Address,Current_DateTime,User_Agent,Submited,Rejected,Daane,Accept_CommonRice,Accept_GradeARice,Reject_CommonRice,Reject_GradeARice,Branch_Code,Godown_Code,Bags,BagType,Tags,TagNo,TruckNo1,ToulReceiptNo, Inspector_ID, StackNumber, StackName) values('" + Dist_Id + "','" + IC_Id + "','" + ddlCropyear.SelectedValue.ToString() + "','" + RejNo + "','" + IssuedDate + "','" + hdfMillID.Value + "',N'" + txtMillingType.Text + "','" + ddlCMRDONo.SelectedItem.ToString() + "','" + txtAgrmtNo.Text + "','" + hdfLotNO.Value + "','" + RejNo + "','0','" + txtTruckNo.Text + "','0','0','" + TxtTotaS.Text + "','" + txtRmkTota.Text + "','0','" + TxtChoteToteS.Text + "','" + txtRmkCTote.Text + "','0','" + txtVijatiyeS.Text + "','" + txtRmkVijatiye.Text + "','0','" + txtDamageDaaneS.Text + "','" + txtRmkDaane.Text + "','0','" + txtBadrangDaaneS.Text + "','" + txtRmkBadrang.Text + "','0','" + txtChaakiDaaneS.Text + "','" + txtRmkChaki.Text + "','0','" + txtLaalDaaneS.Text + "','" + txtRmkLaal.Text + "','0','" + txtOtherS.Text + "','" + txtRmkShreni.Text + "','0','" + txtChokarDaaneS.Text + "','" + txtRmkChokar.Text + "','0','" + txtNamiS.Text + "','" + txtRmkNami.Text + "','" + GetIp + "',GETDATE(),'" + useragent + "','Y','0','" + Daane + "','" + RecdCMR + "','0','0','0','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "','" + txtBags.Text + "','" + ddlbagstype.SelectedValue.ToString() + "','" + tags + "','" + txtTagNo.Text + "','" + txtTruckNo0.Text + "','" + txtToulReceiptNo.Text + "','" + ddl_IC.SelectedValue.ToString() + "','" + ddlstackNumber.SelectedValue.ToString() + "','"+ddlstackNumber.SelectedItem.Text+"'); ";

                                instr += "insert into dbo.tbl_Receipt_Details(State_Id,Dist_ID,Depot_ID,Receipt_id,S_of_arrival,S_name,A_dist,A_Depo,RO_NO,TO_Number,Dispatch_Date,arrival_date,challan_no,challan_date,Qty,Commodity,Scheme,Crop_year,Category,Transporter,Vehile_no,Arrival_time,Gunny_type,No_of_Bags,Recd_Qty,Recieved_Bags,Moisture,WCM_no,Variation_qty,Month,Year,IsDeposit,IP_Address,Created_date,updated_date,Challan_Status,Godown,OperatorID,NoTransaction,Orderno,Branch,  typeofbags, Rec_PP_bags, Rec_Jute_bags, Rec_OU_bags, Sent_Jute_bags, Sent_OU_bags, Sent_PP_bags, whr, StackNumber, stackName ) values('23','" + Dist_Id + "','" + IC_Id + "','" + gatepass + "','05','" + hdfMillID.Value + "','" + hdfPaddy_AgrmtDist.Value + "','','','','" + IssuedDate + "','" + IssuedDate + "','" + RejNo + "','" + IssuedDate + "','270','3','0','" + ddlCropyear.SelectedValue.ToString() + "','1','','" + txtTruckNo.Text + "','','0'," + txtBags.Text + "," + RecdCMR + "," + txtBags.Text + ",'0','','" + Variation + "'," + month + "," + year + ",'N','" + GetIp + "',getdate(),'','N','" + ddlGodown.SelectedValue.ToString() + "','" + opid + "','N','','" + ddlBranch.SelectedValue.ToString() + "','" + ddlbagstype.SelectedValue.ToString() + "','" + Rec_PP_bags + "','" + Rec_Jute_bags + "','" + Rec_OU_bags + "','0','0','0','0','"+ddlstackNumber.SelectedValue.ToString()+"','"+ddlstackNumber.SelectedItem.Text+"') ; ";

                                instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                            }

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                Session["Book_Number"] = RejNo.ToString();
                                //  Session["CropYear"] = ddlCropyear.SelectedValue.ToString();
                                btnAccept.Enabled = btnReject.Enabled = false;
                                btnPrint.Enabled = true;

                                Label2.Visible = true;
                                Label2.Text = "Your CMR Acceptance Number Is : " + RejNo;

                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Lot Is Successfully Accepted and Your CMR Acceptance Number Is " + RejNo + "'); </script> ");
                                txtDistrict.Text = "";
                                //txtYear.Text = "";
                                ddlCropyear.ClearSelection();
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने इस लॉट नंबर के विरुद्ध CMR जमा कर दिया है, इसलिए आप फिर से इस लॉट नंबर के विरुद्ध CMR जमा नहीं कर सकते|'); </script> ");
                            btnAccept.Enabled = btnReject.Enabled = false;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnAccept.Enabled = btnReject.Enabled = false;
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (Session["update"].ToString() == ViewState["update"].ToString())
        {
            using (con = new SqlConnection(strcon))
            {
                try
                {
                    string Dist_Id = Session["dist_id"].ToString();
                    IC_Id = Session["issue_id"].ToString();
                    string opid = Session["OperatorId"].ToString();

                    string browser = Request.Browser.Browser.ToString();
                    string version = Request.Browser.Version.ToString();
                    string useragent = browser + version;

                    ClientIP objClientIP = new ClientIP();
                    string GetIp = (objClientIP.GETIP());

                    ConvertServerDate ServerDate = new ConvertServerDate();
                    string IssuedDate = ServerDate.getDate_MDY(txtDate.Text);

                    string tags = "", BagType = "";

                    if (rbCDaane.Checked)
                    {
                        Daane = "Damage";
                    }
                    else
                    {
                        Daane = "MDamage";
                    }

                    if (rdbTagYes.Checked)
                    {
                        tags = "Y";
                    }
                    else
                    {
                        tags = "N";
                    }

                    //if (rdbNewJute.Checked)
                    //{
                    //    BagType = "9";
                    //}
                    //else if (rdbOldJute.Checked)
                    //{
                    //    BagType = "10";
                    //}
                    //else if (rdbOnceJute.Checked)
                    //{
                    //    BagType = "11";
                    //}
                    //else if (rdbNewPP.Checked)
                    //{
                    //    BagType = "4";
                    //}
                    //else if (rdbOncePP.Checked)
                    //{
                    //    BagType = "2";
                    //}
                    //else
                    //{
                    //    BagType = "12";
                    //}

                    con.Open();

                    string selectmax = "select CONVERT(varchar(10),YEAR(GETDATE()))+CONVERT(varchar(10),MONTH(GETDATE()))+CONVERT(varchar(10),DAY(GETDATE()))+CONVERT(varchar(10),DATENAME(HH,GETDATE()))+CONVERT(varchar(10),DATENAME(MI,GETDATE()))+CONVERT(varchar(10),DATENAME(SS,GETDATE()))+CONVERT(varchar(10),DATENAME(MS,GETDATE())) As DCDate";
                    da = new SqlDataAdapter(selectmax, con);
                    ds = new DataSet();
                    da.Fill(ds);

                    string DCDate = "", SubDCDate = "", RejNo = "", instr = "";

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DCDate = ds.Tables[0].Rows[0]["DCDate"].ToString();
                        SubDCDate = DCDate.Substring(2);
                    }

                    if (SubDCDate != "")
                    {
                        RejNo = "CMRR" + SubDCDate;
                        instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION " +
                                    "Update PaddyMilling_Agreement_2017 set Rejected= (ISNULL(Rejected,0)+1) where Agreement_ID='" + txtAgrmtNo.Text + "';";

                        instr += "Update CMR_DepositOrder set IsRejected='Y' where CMR_DO='" + ddlCMRDONo.SelectedItem.ToString() + "';";

                        instr += "Update PaddyMilling_DO_2017 Set DispatchDhan_IC='N' where Agreement_ID='" + txtAgrmtNo.Text + "' and CropYear='" + ddlCropyear.SelectedValue.ToString() + "' and DhanLot='" + hdfLotNO.Value + "' and Check_DO='" + hdfDO_NO.Value + "' ;";

                        //DO_NO is CMR Deposit Order No 
                        instr += "Insert Into CMR_QualityInspection(District,issueCentre_code,CropYear,Book_Number,Date,Mill_Name,Milling_Type,DO_Number,Agreement_ID,LotNumber,Acceptance_No,Rejection_No,Truck_No,LD_No,TotaGA,TotaS,TotaRemark,ChoteToteGA,ChoteToteS,ChoteToteRemark,VijatiyeGA,VijatiyeS,VijatiyeRemark,DamageDaaneGA,DamageDaaneS,DamageDaaneRemark,BadrangDaaneGA,BadrangDaaneS,BadrangDaaneRemark,ChaakiDaaneGA,ChaakiDaaneS,ChaakiDaaneRemark,LaalDaaneGA,LaalDaaneS,LaalDaaneRemark,OtherGA,OtherS,OtherRemark,ChokarDaaneGA,ChokarDaaneS,ChokarDaaneRemark,NamiGA,NamiS,NamiRemark,IP_Address,Current_DateTime,User_Agent,Submited,Rejected,Daane,Accept_CommonRice,Accept_GradeARice,Reject_CommonRice,Reject_GradeARice,Branch_Code,Godown_Code,Bags,BagType,Tags,TagNo,TruckNo1,ToulReceiptNo, Inspector_ID, StackNumber, StackName) values('" + Dist_Id + "','" + IC_Id + "','" + ddlCropyear.SelectedValue.ToString() + "','" + RejNo + "','" + IssuedDate + "','" + hdfMillID.Value + "',N'" + txtMillingType.Text + "','" + ddlCMRDONo.SelectedItem.ToString() + "','" + txtAgrmtNo.Text + "','" + hdfLotNO.Value + "','0','" + RejNo + "','" + txtTruckNo.Text + "','0','0','" + TxtTotaS.Text + "','" + txtRmkTota.Text + "','0','" + TxtChoteToteS.Text + "','" + txtRmkCTote.Text + "','0','" + txtVijatiyeS.Text + "','" + txtRmkVijatiye.Text + "','0','" + txtDamageDaaneS.Text + "','" + txtRmkDaane.Text + "','0','" + txtBadrangDaaneS.Text + "','" + txtRmkBadrang.Text + "','0','" + txtChaakiDaaneS.Text + "','" + txtRmkChaki.Text + "','0','" + txtLaalDaaneS.Text + "','" + txtRmkLaal.Text + "','0','" + txtOtherS.Text + "','" + txtRmkShreni.Text + "','0','" + txtChokarDaaneS.Text + "','" + txtRmkChokar.Text + "','0','" + txtNamiS.Text + "','" + txtRmkNami.Text + "','" + GetIp + "',GETDATE(),'" + useragent + "','No','1','" + Daane + "','0','0','" + txtRecdQty.Text + "','0','" + ddlBranch.SelectedValue.ToString() + "','" + ddlGodown.SelectedValue.ToString() + "','" + txtBags.Text + "','" + ddlbagstype.SelectedValue.ToString() + "','" + tags + "','" + txtTagNo.Text + "','" + txtTruckNo0.Text + "','" + txtToulReceiptNo.Text + "','" + ddl_IC.SelectedValue.ToString() + "','0','0'); ";

                        instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";
                    }

                    cmd = new SqlCommand(instr, con);
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                    {
                        Session["Book_Number"] = RejNo.ToString();
                        //Session["CropYear"] = ddlCropyear.SelectedValue.ToString();

                        btnAccept.Enabled = btnReject.Enabled = false;
                        btnPrint.Enabled = true;

                        Label2.Visible = true;
                        Label2.Text = "Your CMR Rejection Number Is : " + RejNo;

                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Your Lot Is Rejected and Your CMR Rejection Number Is " + RejNo + "'); </script> ");
                        txtDistrict.Text = "";
                        // txtYear.Text = "";
                        ddlCropyear.ClearSelection();

                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
                catch (Exception ex)
                {
                    btnAccept.Enabled = btnReject.Enabled = false;
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

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenter/PaddyMillingHome.aspx");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected String getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }


    //protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = "";

    //    if (ddlgodown.SelectedIndex > 0)
    //    {
    //        string gname = ddlgodown.SelectedValue;
    //        mobj = new MoveChallan(ComObj);
    //        string qrygdn = "SELECT * FROM dbo.tbl_MetaData_GODOWN where DistrictId='" + Session["dist_id"].ToString() + "' and DepotId='" + Session["issue_id"].ToString() + "' and Godown_ID='" + gname + "'";

    //        DataSet ds = mobj.selectAny(qrygdn);
    //        if (ds == null)
    //        {
    //        }

    //        else
    //        {
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                txtmaxcap.Text = "";
    //            }
    //            else
    //            {
    //                DataRow dr = ds.Tables[0].Rows[0];
    //                txtmaxcap.Text = (System.Math.Round(CheckNull(dr["Godown_Capacity"].ToString()), 5)).ToString();
    //            }
    //        }
    //        //GetBalQty();
    //        GetCapGodown();
    //    }
    //    else
    //    {
    //        txtmaxcap.Text = txtcurntcap.Text = txtavalcap.Text = "";
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Godown Number'); </script> ");
    //    }
    //}

    //void GetCapGodown()
    //{
    //    try
    //    {
    //        string Godown = ddlgodown.SelectedItem.Value;

    //        Int64 comid = Convert.ToInt64(Godown);

    //        string pqry = "available_space_godown";

    //        if (con1.State == ConnectionState.Closed)
    //        {
    //            con1.Open();
    //        }

    //        SqlCommand cmdpqty = new SqlCommand(pqry, con1);
    //        cmdpqty.CommandType = CommandType.StoredProcedure;

    //        cmdpqty.Parameters.Add("@district_code", SqlDbType.Int).Value = Session["dist_id"].ToString();
    //        cmdpqty.Parameters.Add("@Depotid", SqlDbType.NVarChar).Value = Session["issue_id"].ToString();
    //        cmdpqty.Parameters.Add("@GodownId", SqlDbType.NVarChar).Value = Godown;

    //        DataSet ds = new DataSet();
    //        SqlDataAdapter dr = new SqlDataAdapter(cmdpqty);

    //        dr.Fill(ds);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            double stock = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0]["Total"].ToString()), 5);

    //            txtavalcap.Text = Convert.ToString(stock);

    //            txtcurntcap.Text = Convert.ToString(stock);
    //            txtavalcap.Text = (System.Math.Round(CheckNull(txtmaxcap.Text) - CheckNull(txtcurntcap.Text), 5)).ToString();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //    }

    //    finally
    //    {
    //        if (con1.State == ConnectionState.Open)
    //        {
    //            con1.Close();
    //        }
    //    }
    //}


    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;
    }


    protected void btnQuilityTested_Click(object sender, EventArgs e)
    {
        if (ddlCMRDONo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select CMR Deposit Order No.'); </script> ");
            return;
        }
        else if (ddlbagstype.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Bags Type'); </script> ");
            return;
        }

        else if (ddl_IC.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Inspector Name'); </script> ");
            return;
        }
        else if (rdbTagYes.Checked && txtTagNo.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Enter Number of Tags'); </script> ");
            return;
        }

        else
        {
            chkCommon.Visible = false;
            ddlBranch.Enabled = ddlGodown.Enabled = ddlCMRDONo.Enabled = false;
            txtRecdQty.Enabled = txtBags.Enabled = txtTagNo.Enabled = txtTruckNo.Enabled = txtTruckNo0.Enabled = TxtTotaS.Enabled = TxtChoteToteS.Enabled = txtVijatiyeS.Enabled = txtDamageDaaneS.Enabled = txtBadrangDaaneS.Enabled = txtChaakiDaaneS.Enabled = txtLaalDaaneS.Enabled = txtOtherS.Enabled = txtChokarDaaneS.Enabled = txtNamiS.Enabled = txtToulReceiptNo.Enabled = false;
            if (float.Parse(LblTotaS.Text) >= float.Parse(TxtTotaS.Text) && float.Parse(LblChoteToteS.Text) >= float.Parse(TxtChoteToteS.Text) && float.Parse(LblVijatiyeS.Text) >= float.Parse(txtVijatiyeS.Text) && float.Parse(LblDamageDaaneS.Text) >= float.Parse(txtDamageDaaneS.Text) && float.Parse(LblBadrangDaaneS.Text) >= float.Parse(txtBadrangDaaneS.Text) && float.Parse(LblChaakiDaaneS.Text) >= float.Parse(txtChaakiDaaneS.Text) && float.Parse(LblLaalDaaneS.Text) >= float.Parse(txtLaalDaaneS.Text) && float.Parse(LblOtherS.Text) >= float.Parse(txtOtherS.Text) && float.Parse(LblChokarDaaneS.Text) >= float.Parse(txtChokarDaaneS.Text) && float.Parse(LblNamiS.Text) >= float.Parse(txtNamiS.Text) && rdbTagYes.Checked)
            {
                btnAccept.Enabled = true;
                btnReject.Enabled = true;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
            }
            else
            {
                btnReject.Enabled = true;
                btnAccept.Enabled = false;
                btnQuilityTested.Enabled = false;
                btnQuilityTested.Text = "Submitted";
            }
        }
    }

    protected void rdbTagYes_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = true;
        btnAccept.Enabled = btnReject.Enabled = false;
    }
    protected void rdbTagNo_CheckedChanged(object sender, EventArgs e)
    {
        txtTagNo.Text = "";
        txtTagNo.Enabled = false;
        btnAccept.Enabled = btnReject.Enabled = false;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string url = "ReprintCMRAccept.aspx";
        string s = "window.open('" + url + "', 'popup_window');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void ddl_IC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
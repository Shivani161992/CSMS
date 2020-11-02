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
using System.Drawing;

public partial class PaddyMilling_PM_Challan_Delete : System.Web.UI.Page
{
    SqlConnection con, con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;

    string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;      //CSMS
    string strcon_MPStorage = ConfigurationManager.ConnectionStrings["connstorage"].ConnectionString; //Integrated_MP_Storage

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["st_id"] != null)
        if (Session["dist_id"] != null)
        {
            if (!IsPostBack)
            {
                GetCropYearValues();
                GetDistName();

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/MainLogin.aspx");
        }
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Session["st_id"].ToString() == "10")
    //    {
    //        this.MasterPageFile = "~/MasterPage/Markfed_PDY.master";
    //    }
    //}

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

    public void GetDistName()
    {
        string DistCode = Session["dist_id"].ToString();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();
                string select = "";

                //if (Session["st_id"].ToString() == "10")
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed='Y' Order By district_name";
                //}
                //else
                //{
                //    select = "SELECT district_name,district_code FROM pds.districtsmp where paddy_markfed IS NULL Order By district_name";
                //}

                select = "SELECT district_name,district_code FROM pds.districtsmp where district_code='" + DistCode + "'";

                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFrmDist.DataSource = ds.Tables[0];
                        ddlFrmDist.DataTextField = "district_name";
                        ddlFrmDist.DataValueField = "district_code";
                        ddlFrmDist.DataBind();
                        ddlFrmDist.Items.Insert(0, "--Select--");
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

    protected void btnRecptNew_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }



    protected void btnRecptClose_Click(object sender, EventArgs e)
    {
        //if (Session["st_id"].ToString() == "10")
        //{
        //    Response.Redirect("~/State/PaddyMillingHome_MFD.aspx");
        //}
        //else
        //{
        //    Response.Redirect("~/State/PaddyMillingHome.aspx");
        //}
        Response.Redirect("~/District/PaddyMillingHome.aspx");
    }

    protected void ddlFrmDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMillName.Items.Clear();
        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();
        btnDelete.Enabled = false;
        txtQty.Text = txtBags.Text = txtTruckNo.Text = txtDate.Text = txtLotNo.Text = "";
        hdfDONo.Value = hdfGatePass.Value = "";

        if (ddlFrmDist.SelectedIndex > 0)
        {
            GetMillName();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select District'); </script> ");
            return;
        }
    }

    public void GetMillName()
    {
        ddlMillName.Items.Clear();
        ddlChallanNo.Items.Clear();

        using (con = new SqlConnection(strcon))
        {
            try
            {
                string select = "";

                select = "Select distinct PM.Mill_Name As MillCode,MR.Mill_Name As MillName From PaddyMilling_Agreement_2017 As PM Left Join Miller_Registration_2017 MR ON(PM.Mill_Name=MR.Registration_ID and PM.Mill_Addr_District=MR.District_Code and PM.CropYear=MR.CropYear) where PM.District='" + ddlFrmDist.SelectedValue.ToString() + "' and PM.CropYear='" + txtYear.Text + "' and PM.IsAccepted='Y' order by MillName Asc";

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
        ddlAgtmtNumber.Items.Clear();
        ddlDONo.Items.Clear();
        ddlChallanNo.Items.Clear();
        btnDelete.Enabled = false;
        txtQty.Text = txtBags.Text = txtTruckNo.Text = txtDate.Text = txtLotNo.Text = "";
        hdfDONo.Value = hdfGatePass.Value = "";

        using (con = new SqlConnection(strcon))
        {
            try
            {
                if (ddlMillName.SelectedIndex > 0)
                {
                    con.Open();

                    string select = "";

                    select = "Select Agreement_ID From PaddyMilling_Agreement_2017 where District='" + ddlFrmDist.SelectedValue.ToString() + "' and Mill_Name='" + ddlMillName.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and IsAccepted='Y' order by Agreement_ID";

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
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Mill Name'); </script> ");
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
        ddlDONo.Items.Clear();
        ddlChallanNo.Items.Clear();
        txtQty.Text = txtBags.Text = txtTruckNo.Text = txtDate.Text = txtLotNo.Text = "";
        hdfDONo.Value = hdfGatePass.Value = "";
        btnDelete.Enabled = false;

        if (ddlAgtmtNumber.SelectedIndex > 0)
        {
            GetAgrmtData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Agreement Number'); </script> ");
        }
    }

    public void GetAgrmtData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select distinct Check_DO From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDONo.DataSource = ds.Tables[0];
                    ddlDONo.DataTextField = "Check_DO";
                    ddlDONo.DataValueField = "Check_DO";
                    ddlDONo.DataBind();
                    ddlDONo.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    protected void ddlDONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQty.Text = txtBags.Text = txtTruckNo.Text = txtDate.Text = txtLotNo.Text = "";
        ddlChallanNo.Items.Clear();
        btnDelete.Enabled = false;
        hdfDONo.Value = hdfGatePass.Value = "";

        if (ddlDONo.SelectedIndex > 0)
        {
            GetDOData();
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select DO Number'); </script> ");
        }
    }

    public void GetDOData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select DispatchDhan_IC From PaddyMilling_DO_2017 where Mill_Code='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and Check_DO='" + ddlDONo.SelectedValue.ToString() + "'";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string CMRDO = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string check = "";
                        check = ds.Tables[0].Rows[i]["DispatchDhan_IC"].ToString();

                        if (check == "Y")
                        {
                            CMRDO = "Y";
                            break;
                        }
                        else
                        {
                            CMRDO = "N";
                        }
                    }

                    if (CMRDO == "N" && CMRDO != "")
                    {
                        string ddlselect = "Select trans_id,remark From PaddyMilling_IssueAgainst_DO where Partyname='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and district_code='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and delivery_order_no='" + ddlDONo.SelectedValue.ToString() + "'";
                        da = new SqlDataAdapter(ddlselect, con);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            ddlChallanNo.DataSource = ds.Tables[0];
                            ddlChallanNo.DataTextField = "trans_id";
                            ddlChallanNo.DataValueField = "trans_id";
                            ddlChallanNo.DataBind();
                            ddlChallanNo.Items.Insert(0, "--Select--");

                            hdfDONo.Value = ds.Tables[0].Rows[0]["remark"].ToString();
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DO Number के विरुद्ध कोई भी Paddy Challan जारी नहीं किया गया हैं, इसलिए Challan No. उपलब्ध नहीं है|'); </script> ");
                            return;
                        }
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('इस DO Number के विरुद्ध CMR Deposit Order जारी किया जा चूका है, इसलिए Challan No. उपलब्ध नहीं है|'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    protected void ddlChallanNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQty.Text = txtBags.Text = txtTruckNo.Text = txtDate.Text = txtLotNo.Text = "";
        btnDelete.Enabled = false;
        hdfGatePass.Value = "";

        if (ddlChallanNo.SelectedIndex > 0)
        {
            CheckGatePass();
            if (hdfGatePass.Value != "1")
            {
                GetChallanData();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो चालान नंबर Select किया है, उसका GatePass जारी किया जा चूका है, इसलिए आप इसे डिलीट नहीं कर सकते|'); </script> ");
            }
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan Number'); </script> ");
        }
    }

    public void CheckGatePass()
    {
        using (con_MPStorage = new SqlConnection(strcon_MPStorage))
        {
            try
            {
                con_MPStorage.Open();

                string select = "Select * From tbl_RO_Details Where Trans_ID='" + ddlChallanNo.SelectedItem.ToString() + "' and State_Id='23' and Commodity_Id='13' ";
                da = new SqlDataAdapter(select, con_MPStorage);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdfGatePass.Value = "1";
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

    public void GetChallanData()
    {
        using (con = new SqlConnection(strcon))
        {
            try
            {
                con.Open();

                string select = "Select Issued_CommonDhan,bags,gate_pass,issue_date,LotNumber From PaddyMilling_IssueAgainst_DO where trans_id='" + ddlChallanNo.SelectedItem.ToString() + "' and Partyname='" + ddlMillName.SelectedValue.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedValue.ToString() + "' and district_code='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' and delivery_order_no='" + ddlDONo.SelectedValue.ToString() + "' ";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtLotNo.Text = ds.Tables[0].Rows[0]["LotNumber"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["Issued_CommonDhan"].ToString();
                    txtBags.Text = ds.Tables[0].Rows[0]["bags"].ToString();
                    txtTruckNo.Text = ds.Tables[0].Rows[0]["gate_pass"].ToString();

                    DateTime DODate = DateTime.Parse(ds.Tables[0].Rows[0]["issue_date"].ToString());
                    txtDate.Text = DODate.ToString("dd-MM-yyyy");

                    btnDelete.Enabled = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('No Data Found'); </script> ");
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlChallanNo.SelectedIndex <= 0)
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Please Select Challan No.'); </script> ");
            return;
        }
        else if (hdfGatePass.Value == "1")
        {
            Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('आपने जो चालान नंबर Select किया है, उसका GatePass जारी किया जा चूका है, इसलिए आप इसे डिलीट नहीं कर सकते|'); </script> ");
            return;
        }
        else
        {
            if (txtYear.Text != "" && hdfDONo.Value != "")
            {
                ClientIP objClientIP = new ClientIP();
                string GetIp = (objClientIP.GETIP());

                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    using (con = new SqlConnection(strcon))
                    {
                        try
                        {
                            con.Open();

                            double dblIssuedPaddy = double.Parse(txtQty.Text);

                            string instr = "";

                            instr = "BEGIN TRY; SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED BEGIN TRANSACTION ";

                            instr += "Insert Into PaddyMilling_IssueAgainst_DO_Log(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,disp_time,remark,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,Rem_CommonDhan,Rem_GradeADhan,Proximate_CommonRice,Proximate_GradeARice,Proximate_TotalRice,Agreement_ID,Branch,DeletedDate,DeletedIP,Operation, StackNumber) select trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,updated_date,swc_status,ip_add,disp_time,remark,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,LotNumber,Milling_Type,Issued_CommonDhan,Issued_GradeADhan,Rem_CommonDhan,Rem_GradeADhan,Proximate_CommonRice,Proximate_GradeARice,Proximate_TotalRice,Agreement_ID,Branch,GETDATE(),'" + GetIp + "','D', StackNumber From PaddyMilling_IssueAgainst_DO where trans_id='" + ddlChallanNo.SelectedItem.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and district_code='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ; ";
                            instr += "Insert Into PaddyMilling_DO_Log_2017(DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,DeletedDate,DeletedIP,Operation) select DO_Number,Mill_Code,Agreement_ID,District,CropYear,From_Date,To_Date,Milling_Type,Alloted_CommonDhan,Alloted_GradeADhan,R_Arva,R_Ushna,Issue_Centre,IP_Address,Current_DateTime,User_Agent,Check_DO,DispatchDhan_IC,Return_CommonRice,Return_GradeARice,Return_TotalRice,LotOnlyNumber,Rejected,DhanLot,Branch_Code,Godown_Code,LotNumber,Rem_Alloted_CommonDhan,Rem_Alloted_GradeADhan,Rem_DhanLot,LotNo,GETDATE(),'" + GetIp + "','U' From PaddyMilling_DO_2017 where DO_Number='" + hdfDONo.Value + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ; ";
                            instr += "Insert Into issue_against_OpenSale_do_Log(trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,disp_time,remark,updated_date,LotNumber,Milling_Type,Agreement_ID,Branch,DeletedDate,DeletedIP,Operation) Select trans_id,delivery_order_no,district_code,issueCentre_code,issue_to,qty_issue,bags,Source,Godown,issue_date,gate_pass,created_date,swc_status,ip_add,OperatorID,NoTransaction,Partyname,CropYear,User_Agent,disp_time,remark,updated_date,LotNumber,Milling_Type,Agreement_ID,Branch,GETDATE(),'" + GetIp + "','D' From issue_against_OpenSale_do where trans_id='" + ddlChallanNo.SelectedValue.ToString() + "' and issue_to='1' and gate_pass='" + txtTruckNo.Text + "' and CropYear='" + txtYear.Text + "' and User_Agent='" + ddlDONo.SelectedItem.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "';";
                            instr += "Insert Into OpenSale_DO_Log(delivery_order_no,district_code,issue_type,do_date,payment_mode,dd_no,dd_date,quantity,rate_per_qtls,tot_amount,amount,bank_id,commodity_id,scheme_id,created_date,IP,OperatorID,Partyname,crop_year) Select delivery_order_no,district_code,issue_type,do_date,payment_mode,dd_no,dd_date,quantity,rate_per_qtls,tot_amount,amount,bank_id,commodity_id,scheme_id,created_date,IP,OperatorID,Partyname,crop_year From OpenSale_DO Where issue_type='1' and dd_no='" + ddlChallanNo.SelectedItem.ToString() + "' and commodity_id='13' and Partyname='" + ddlMillName.SelectedValue.ToString() + "' and crop_year='" + txtYear.Text + "' ;";

                            instr += "Delete PaddyMilling_IssueAgainst_DO where trans_id='" + ddlChallanNo.SelectedItem.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "' and district_code='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ;";
                            instr += "Update PaddyMilling_DO_2017 set Return_CommonRice=(ISNULL(Return_CommonRice,0)-" + dblIssuedPaddy + ") where DO_Number='" + hdfDONo.Value + "' and District='" + ddlFrmDist.SelectedValue.ToString() + "' and CropYear='" + txtYear.Text + "' ;";
                            instr += "Delete issue_against_OpenSale_do where trans_id='" + ddlChallanNo.SelectedValue.ToString() + "' and issue_to='1' and gate_pass='" + txtTruckNo.Text + "' and CropYear='" + txtYear.Text + "' and User_Agent='" + ddlDONo.SelectedItem.ToString() + "' and Agreement_ID='" + ddlAgtmtNumber.SelectedItem.ToString() + "';";
                            instr += "Delete OpenSale_DO Where issue_type='1' and dd_no='" + ddlChallanNo.SelectedItem.ToString() + "' and commodity_id='13' and Partyname='" + ddlMillName.SelectedValue.ToString() + "' and crop_year='" + txtYear.Text + "' ;";

                            instr += "COMMIT end TRY BEGIN CATCH IF @@TRANCOUNT > 0 ROLLBACK;DECLARE @ErrorMessage NVARCHAR(4000);DECLARE @ErrorSeverity INT;DECLARE @ErrorState INT;SELECT @ErrorMessage = ERROR_MESSAGE(),  @ErrorSeverity = ERROR_SEVERITY(),   @ErrorState = ERROR_STATE();  RAISERROR (@ErrorMessage,  @ErrorSeverity,@ErrorState ); END CATCH;";

                            cmd = new SqlCommand(instr, con);
                            int count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                btnDelete.Enabled = false;
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Data Deleted Successfully'); </script> ");
                                txtYear.Text = "";
                                ddlFrmDist.Enabled = ddlMillName.Enabled = ddlAgtmtNumber.Enabled = ddlDONo.Enabled = false;
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Sorry, Deletion Not Allow'); </script> ");
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
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["update"] = Session["update"];
    }
}